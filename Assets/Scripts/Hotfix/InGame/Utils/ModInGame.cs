using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using DragonPlus.Config.InGame;
using Framework;
using GameStorage;
using Newtonsoft.Json;
using UnityEngine;

public class ModInGame : ModuleBase
{
    private StorageMahjongScrew storageMahjongScrew;

    public override void OnLoginSuccess()
    {
        base.OnLoginSuccess();
        storageMahjongScrew = Game.GetMod<ModStorage>().GetStorage<StorageMahjongScrew>();
        if (!storageMahjongScrew.LevelIndexDict.ContainsKey((int)EInGameModeType.Main))
        {
            storageMahjongScrew.LevelIndexDict.Add((int)EInGameModeType.Main, 0);
        }
    }

    /// <summary>
    /// 进入游戏
    /// </summary>
    public void EnterGame(int levelIndex, EInGameModeType modeType)
    {
        storageMahjongScrew.LevelIndexDict[(int)modeType] = levelIndex;
        var enterParam = new FsmState_InGame.EnterParam();
        enterParam.levelIndex = levelIndex;
        enterParam.modeType = modeType;
        Game.GetMod<ModFsm>().ChangeState<FsmState_InGame>(true, enterParam);
    }

    /// <summary>
    /// GM进入关卡
    /// </summary>
    public void EnterGame_GM(string levelJsonFileName, EInGameModeType modeType)
    {
        var enterParam = new FsmState_InGame.EnterParam();
        enterParam.levelIndex = 0;
        enterParam.modeType = modeType;
        enterParam.forceJsonFileName = levelJsonFileName;
        Game.GetMod<ModFsm>().ChangeState<FsmState_InGame>(true, enterParam);
    }

    /// <summary>
    /// 下一关
    /// </summary>
    public void NextLevel(EInGameModeType modeType)
    {
        int levelIndex = storageMahjongScrew.LevelIndexDict[(int)modeType];
        int nextLevelIndex = levelIndex + 1;
        EnterGame(nextLevelIndex, modeType);
    }

    /// <summary>
    /// 上一关
    /// </summary>
    public void LastLevel(EInGameModeType modeType)
    {
        int levelIndex = storageMahjongScrew.LevelIndexDict[(int)modeType];
        if (levelIndex - 1 < 0)
            return;
        int lastLevelIndex = levelIndex - 1;
        EnterGame(lastLevelIndex, modeType);
    }

    /// <summary>
    /// 获取某个模式的关卡下标
    /// </summary>
    public int GetLevelIndex(EInGameModeType modeType)
    {
        if (storageMahjongScrew.LevelIndexDict.TryGetValue((int)modeType, out var _levelIndex))
            return _levelIndex;
        return 0;
    }

    /// <summary>
    /// 增加某个模式的关卡下标
    /// </summary>
    public void AddLevelIndex(EInGameModeType modeType)
    {
        if (storageMahjongScrew.LevelIndexDict.ContainsKey((int)modeType))
        {
            storageMahjongScrew.LevelIndexDict[(int)modeType]++;
            if (modeType == EInGameModeType.Main)
            {
                BIHelper.SendTrackingEvent_PassLevel(storageMahjongScrew.LevelIndexDict[(int)modeType]);
            }
        }

    }

    /// <summary>
    /// 获取关卡配置
    /// </summary>
    public Table_InGame_Level GetLevelCfg(int levelIndex, EInGameModeType modeType, bool forceGet = true)
    {
        var cfgList = InGameConfigUtils.GetLevelCfgList(modeType, forceGet);
        if (levelIndex < cfgList.Count)
        {
            var cfg = cfgList[levelIndex];
            return cfg;
        }
        // 超出则循环
        else
        {
            int loopStart = Mathf.Min(100, cfgList.Count);
            int loopEnd = cfgList.Count;
            int loopCount = loopEnd - loopStart + 1;
            int n = loopStart + ((levelIndex - cfgList.Count) % loopCount) - 1;
            if (n > cfgList.Count)
            {
                CLog.Error("索引超出，异常！！！！！");
                return cfgList[loopStart - 1];
            }
            if (n >= cfgList.Count)
            {
                CLog.Error("越界，异常！！！！！");
                return cfgList[^1];
            }
            return cfgList[n];
        }
    }

    /// <summary>
    /// 获取关卡数据
    /// </summary>
    private Dictionary<string, LevelData> levelDataDict = new Dictionary<string, LevelData>();
    public LevelData GetLevelLayoutData(string jsonFileName)
    {
        if (levelDataDict.TryGetValue(jsonFileName, out var _levelData))
            return _levelData;

        GameObject holder = Game.GetMod<ModFsm>().CheckState<FsmState_InGame>()
            ? (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode.Root
            : Game.DontDestoryRoot;
        string text = Game.GetMod<ModAsset>().GetRes<TextAsset>(jsonFileName)?.GetInstance(holder)?.text;
        if (text == null)
        {
            CLog.Error("找不到关卡数据文件：" + jsonFileName);
            return null;
        }

        var json = "";
#if UNITY_EDITOR
        json = text;
#else
        // 解密
        var compressedBytes = AESUtils.DecryptFromBase64(text, GameSafeCenter.EnhancementEncryptKey, CipherMode.CBC);
        // 解压
        using (var ms = new MemoryStream(compressedBytes))
        using (var gzip = new GZipStream(ms, CompressionMode.Decompress))
        using (var outMS = new MemoryStream())
        {
            gzip.CopyTo(outMS);
            json = Encoding.UTF8.GetString(outMS.ToArray());
        }

#endif
        var levelData = JsonConvert.DeserializeObject<LevelData>(json,
            new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
            }); //防止派生类关系丢失
        if (levelData == null)
        {
            CLog.Error($"解析关卡数据失败：{jsonFileName}");
            return null;
        }
        levelDataDict.TryAdd(jsonFileName, levelData);
        return levelData;
    }

    #region GM

    // 开启无敌模式
    private bool enableInvincibleMode;
    public bool EnableInvincibleMode
    {
        get
        {
            if (Main.GameUtils.IsDevelopmentEnv())
            {
                return enableInvincibleMode;
            }
            else
            {
                return false;
            }
        }
        set
        {
            enableInvincibleMode = value;
        }
    }

    #endregion GM
}

/// <summary>
/// 局内模式类型
/// </summary>
public enum EInGameModeType : byte
{
    Main = 1, //主玩法
}

/// <summary>
/// 局内难度类型
/// </summary>
public enum EIngameDifficultType : byte
{
    Easy = 1,
    Hard,
    SuperHard,
}

/// <summary>
/// 局内失败类型
/// </summary>
public enum EInGameFailType : byte
{
    NoHp = 1,//血量为0
    NoStep,//步数没了
}