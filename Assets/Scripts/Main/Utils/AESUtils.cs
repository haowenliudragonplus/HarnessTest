using System;
using System.Security.Cryptography;
using System.Text;

public static class AESUtils
{
    public static string EncryptToBase64(string plaintext, string key, CipherMode cipherMode,
        byte[] iv = null, bool randomIV = false)
    {
        if (string.IsNullOrEmpty(plaintext))
            return null;

        byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
        return EncryptToBase64(plaintextBytes, key, cipherMode, iv, randomIV);
    }

    public static string EncryptToBase64(byte[] plaintextBytes, string key, CipherMode cipherMode,
        byte[] iv = null, bool randomIV = false)
    {
        if (plaintextBytes == null || string.IsNullOrEmpty(key))
            return null;

        if (cipherMode == CipherMode.CBC && randomIV)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = DeriveKeyBytes(key);
                aes.Mode = cipherMode;
                aes.Padding = PaddingMode.PKCS7;
                aes.GenerateIV();

                using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    byte[] ciphertextBytes = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
                    byte[] combined = new byte[16 + ciphertextBytes.Length];
                    Buffer.BlockCopy(aes.IV, 0, combined, 0, 16);
                    Buffer.BlockCopy(ciphertextBytes, 0, combined, 16, ciphertextBytes.Length);
                    return Convert.ToBase64String(combined);
                }
            }
        }

        byte[] keyByteArray = DeriveKeyBytes(key);
        using (Aes aes = Aes.Create())
        {
            aes.Key = keyByteArray;
            aes.Mode = cipherMode;
            aes.Padding = PaddingMode.PKCS7;

            if (cipherMode == CipherMode.CBC)
            {
                aes.IV = iv ?? DeriveIVFromKey(key);
            }

            using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            {
                byte[] ciphertextByteArray = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
                return Convert.ToBase64String(ciphertextByteArray);
            }
        }
    }

    public static byte[] DecryptFromBase64(string ciphertextBase64, string key, CipherMode cipherMode,
        byte[] iv = null, bool randomIV = false)
    {
        if (string.IsNullOrEmpty(ciphertextBase64))
            return null;

        byte[] ciphertextBytes = Convert.FromBase64String(ciphertextBase64);
        return DecryptFromBase64(ciphertextBytes, key, cipherMode, iv, randomIV);
    }

    public static byte[] DecryptFromBase64(byte[] ciphertextBytes, string key, CipherMode cipherMode,
        byte[] iv = null, bool randomIV = false)
    {
        if (ciphertextBytes == null || string.IsNullOrEmpty(key))
            return null;

        if (cipherMode == CipherMode.CBC && randomIV)
        {
            if (ciphertextBytes.Length < 16)
                return null;

            byte[] extractedIV = new byte[16];
            byte[] realCiphertext = new byte[ciphertextBytes.Length - 16];
            Buffer.BlockCopy(ciphertextBytes, 0, extractedIV, 0, 16);
            Buffer.BlockCopy(ciphertextBytes, 16, realCiphertext, 0, realCiphertext.Length);

            using (Aes aes = Aes.Create())
            {
                aes.Key = DeriveKeyBytes(key);
                aes.Mode = cipherMode;
                aes.Padding = PaddingMode.PKCS7;
                aes.IV = extractedIV;

                using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    return decryptor.TransformFinalBlock(realCiphertext, 0, realCiphertext.Length);
                }
            }
        }

        byte[] keyBytes = DeriveKeyBytes(key);
        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.Mode = cipherMode;
            aes.Padding = PaddingMode.PKCS7;

            if (cipherMode == CipherMode.CBC)
            {
                aes.IV = iv ?? DeriveIVFromKey(key);
            }

            using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            {
                return decryptor.TransformFinalBlock(ciphertextBytes, 0, ciphertextBytes.Length);
            }
        }
    }

    private static byte[] DeriveKeyBytes(string key)
    {
        using SHA256 sha256 = SHA256.Create();
        return sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
    }

    private static byte[] DeriveIVFromKey(string key)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
        byte[] iv = new byte[16];
        Buffer.BlockCopy(hash, 0, iv, 0, iv.Length);
        return iv;
    }
}
