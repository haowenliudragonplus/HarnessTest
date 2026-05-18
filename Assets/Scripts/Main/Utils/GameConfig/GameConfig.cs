using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public partial class GameConfig
{
    private const string GAME_CONFIG_DIR = "GameConfig";

    private static Dictionary<string, string> configDict = new Dictionary<string, string>();

    public static void AddConfig(string key, string value)
    {
        if (configDict.ContainsKey(key))
            configDict[key] = value;
        else
            configDict.Add(key, value);
    }

    public static string GetConfig(string key)
    {
        if (!configDict.TryGetValue(key, out var _value))
            return String.Empty;
        return _value;
    }
}