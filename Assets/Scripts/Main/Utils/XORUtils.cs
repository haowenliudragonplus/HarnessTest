using System;

/// <summary>
/// 异或加密
/// </summary>
public static class XORUtils
{
    /// <summary>
    /// 使用异或加密/解密字节数组
    /// </summary>
    public static byte[] Crypto(byte[] data, byte[] key)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));

        if (key == null || key.Length == 0)
            throw new ArgumentException("Key cannot be null or empty");

        byte[] result = new byte[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            // 循环使用密钥中的字节
            result[i] = (byte)(data[i] ^ key[i % key.Length]);
        }

        return result;
    }

    public static byte[] Crypto(byte[] data, string key)
    {
        byte[] keyBytes = System.Text.Encoding.UTF8.GetBytes(key);
        return Crypto(data, keyBytes);
    }
}