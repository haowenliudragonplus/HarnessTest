using System;
using System.ComponentModel;
using System.Reflection;

public static class EnumExtension
{
    public static string ToDes(this Enum e)
    {
        FieldInfo fi = e.GetType().GetField(e.ToString());
        DescriptionAttribute attribute = Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute)) as DescriptionAttribute;
        return attribute == null
            ? e.ToString()
            : attribute.Description;
    }
}