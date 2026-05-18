
public static class StringUtils
{
    /// <summary>
    /// 首字母大写
    /// </summary>
    public static string FirstCharToUpper(string str)
    {
        char[] charArray = str.ToCharArray();
        charArray[0] = char.ToUpper(charArray[0]);
        return new string(charArray);
    }
}