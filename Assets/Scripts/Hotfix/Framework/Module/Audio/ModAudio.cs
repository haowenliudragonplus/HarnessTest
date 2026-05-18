using System.Collections.Generic;
using DragonPlus.Config.Common;
using DragonPlus.Core;
using DragonPlus.Haptics;
using DragonPlus.Save;
using Framework;
using GameStorage;
using TMGame;
using UnityEngine;

/// <summary>
/// 音效数据
/// </summary>
public class SoundData
{
    public int audioId;
    public AudioSource audioSource;
    public TimerTask timerTask;
}

/// <summary>
/// 声音管理器
/// </summary>
public class ModAudio : ModuleBase
{
    private AudioSource bgmAudioSource; //BMG的音效组件（全局只有一个）

    private const string SoundAudioSourcePoolName = "SoundAudioSource"; //音效组件的池子key
    private Dictionary<int, List<SoundData>> soundId2SoundDataDict = new Dictionary<int, List<SoundData>>(); //音效id - 正在播放的音效数据列表

    /// <summary>
    /// BGM是否开启
    /// </summary>
    public bool EnableBGM
    {
        get
        {
            var StorageClientCommon = SDK<IStorage>.Instance.Get<StorageClientCommon>();
            return !StorageClientCommon.UserData.MusicClose;
        }
        set
        {
            var StorageClientCommon = SDK<IStorage>.Instance.Get<StorageClientCommon>();
            StorageClientCommon.UserData.MusicClose = !value;
            if (!value)
            {
                PauseBGM();
            }
            else
            {
                UnPauseBGM();
            }
        }
    }

    /// <summary>
    /// 音效是否开启
    /// </summary>
    public bool EnableSound
    {
        get
        {
            var StorageClientCommon = SDK<IStorage>.Instance.Get<StorageClientCommon>();
            return !StorageClientCommon.UserData.SoundEffectClose;
        }
        set
        {
            var StorageClientCommon = SDK<IStorage>.Instance.Get<StorageClientCommon>();
            StorageClientCommon.UserData.SoundEffectClose = !value;
            if (!value)
            {
                StopAllSound();
            }
        }
    }

    /// <summary>
    /// 震动是否开启
    /// </summary>
    public bool VibrateClose
    {
        get
        {
            var StorageClientCommon = SDK<IStorage>.Instance.Get<StorageClientCommon>();
            return StorageClientCommon.UserData.VibrateClose;
        }
        set
        {
            var StorageClientCommon = SDK<IStorage>.Instance.Get<StorageClientCommon>();
            StorageClientCommon.UserData.VibrateClose = value;
            GameUtils.SetVibrateEnable(value);
        }
    }

    #region BGM相关

    /// <summary>
    /// 播放BGM
    /// </summary>
    public void PlayBGM(int audioId, float startClipTime = 0, float volume = 1)
    {
        // 2026-01-09 15:10:00 屏蔽BGM
        return;
        CreateBGMAudioSource();

        var clip = LoadAudioClip(audioId);
        if (clip == null)
            return;
        if (bgmAudioSource.clip != null && bgmAudioSource.clip.name == clip.name)
        {
            CLog.Error($"BGM正在播放，BGM：{clip.name}");
            return;
        }

        StopBGM();

        bgmAudioSource.loop = true;
        bgmAudioSource.clip = clip;
        bgmAudioSource.time = startClipTime;
        bgmAudioSource.volume = volume;
        bgmAudioSource.Play();

        if (!EnableBGM)
            PauseBGM();
    }

    /// <summary>
    /// 暂停BGM
    /// </summary>
    public void PauseBGM()
    {
        // 2026-01-09 15:10:00 屏蔽BGM
        if (bgmAudioSource == null || bgmAudioSource.clip == null || !bgmAudioSource.isPlaying)
            return;

        bgmAudioSource.Pause();
    }

    /// <summary>
    /// 恢复BGM
    /// </summary>
    public void UnPauseBGM()
    {
        if (!EnableBGM)
            return;
        // 2026-01-09 15:10:00 屏蔽BGM
        if (bgmAudioSource == null || bgmAudioSource.clip == null || bgmAudioSource.isPlaying)
            return;

        bgmAudioSource.UnPause();
    }

    /// <summary>
    /// 停止BGM
    /// </summary>
    public void StopBGM()
    {
        // 2026-01-09 15:10:00 屏蔽BGM
        if (bgmAudioSource == null || bgmAudioSource.clip == null || !bgmAudioSource.isPlaying)
            return;

        bgmAudioSource.clip = null;
        bgmAudioSource.Stop();
    }

    /// <summary>
    /// 获取当前正在播放的BGM片段
    /// </summary>
    public AudioClip GetBGMClip()
    {
        if (bgmAudioSource == null)
            return null;
        var clip = bgmAudioSource.clip;
        return clip;
    }

    private void CreateBGMAudioSource()
    {
        if (bgmAudioSource != null)
            return;

        GameObject bgmGo = new GameObject("BGM_AudioSource");
        bgmGo.transform.SetParent(Game.DontDestoryRoot.transform);
        bgmGo.transform.localPosition = Vector3.zero;
        bgmAudioSource = bgmGo.AddComponent<AudioSource>();
        bgmAudioSource.loop = true;
        bgmAudioSource.playOnAwake = false;
    }

    #endregion BGM相关

    #region 音效相关

    /// <summary>
    /// 播放音效
    /// </summary>
    public AudioSource PlaySound(int audioId, bool loop = false, float volume = 1)
    {
        if (!EnableSound)
            return null;

        var clip = LoadAudioClip(audioId);
        if (clip == null)
            return null;

        var audioSource = GameObjectPool.Get(SoundAudioSourcePoolName).GetComponent<AudioSource>();

        if (!soundId2SoundDataDict.TryGetValue(audioId, out var _list))
        {
            _list = new List<SoundData>();
            soundId2SoundDataDict.Add(audioId, _list);
        }
        SoundData soundData = new SoundData();

        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.volume = volume;
        audioSource.Play();
        if (!loop)
        {
            soundData.timerTask = Game.GetMod<ModTimer>().Register(clip.length, true, onComplete: (v) =>
            {
                //
                InternalStopSound(soundData);
            }, belongType: ETimerBelongType.Audio);
        }
        soundData.audioId = audioId;
        soundData.audioSource = audioSource;
        _list.Add(soundData);
        return audioSource;
    }

    /// <summary>
    /// 停止某一个音效
    /// </summary>
    public void StopSound(AudioSource audioSource)
    {
        if (audioSource == null)
            return;

        bool b = false;
        foreach (var soundDataList in soundId2SoundDataDict.Values)
        {
            var tempList = new List<SoundData>(soundDataList);
            foreach (var soundData in tempList)
            {
                if (soundData.audioSource != audioSource)
                    continue;
                InternalStopSound(soundData);
                b = true;
                break;
            }
            if (b)
                break;
        }
    }

    /// <summary>
    /// 停止某个id的全部音效
    /// </summary>
    public void StopSound(int audioId)
    {
        if (!soundId2SoundDataDict.TryGetValue(audioId, out var _soundDataList))
            return;

        var tempList = new List<SoundData>(_soundDataList);
        foreach (var soundData in tempList)
        {
            InternalStopSound(soundData);
        }
    }

    private void InternalStopSound(SoundData soundData)
    {
        if (soundData == null)
            return;
        soundData.timerTask?.Dispose();
        PutSoundAudioSourceToPool(soundData.audioSource);
        var soundDataList = soundId2SoundDataDict[soundData.audioId];
        soundDataList.Remove(soundData);
        if (soundDataList.Count <= 0)
        {
            soundId2SoundDataDict.Remove(soundData.audioId);
        }
    }

    /// <summary>
    /// 停止全部音效
    /// </summary>
    public void StopAllSound()
    {
        if (soundId2SoundDataDict.Count <= 0)
            return;

        Game.GetMod<ModTimer>().DisposeAll(ETimerBelongType.Audio);

        foreach (var soundDataList in soundId2SoundDataDict.Values)
        {
            foreach (var soundData in soundDataList)
            {
                if (soundData == null)
                    continue;
                soundData.timerTask?.Dispose();
                PutSoundAudioSourceToPool(soundData.audioSource);
            }
        }
        soundId2SoundDataDict.Clear();
    }

    /// <summary>
    /// 将音效组件放回池子
    /// </summary>
    private void PutSoundAudioSourceToPool(AudioSource audioSource)
    {
        if (audioSource == null)
            return;

        audioSource.clip = null;
        audioSource.Stop();
        GameObjectPool.Put(audioSource.gameObject, SoundAudioSourcePoolName);
    }

    #endregion 音效相关

    private AudioClip LoadAudioClip(int audioId)
    {
        var audioCfg = Game.GetMod<ModConfig>().GetConfig<Table_Common_Audio>(audioId);
        if (audioCfg == null)
            return null;
        var resKey = CoreUtils.GetResKey(audioCfg.AudioResourceId);
        var audioClip = Game.GetMod<ModAsset>().GetRes<AudioClip>(resKey).GetInstance(Game.DontDestoryRoot);
        return audioClip;
    }

    /// <summary>
    /// 初始化震动
    /// </summary>
    private void InitVirbrate()
    {
        SDK<IHaptics>.Instance.Init();
        GameUtils.SetVibrateEnable(VibrateClose);
    }

    #region 生命周期

    public override void OnInit()
    {
        var pool = GameObjectPool.GetOrCreatePool(SoundAudioSourcePoolName, forceGet: true);
        pool.SetCustomInstantiateFun(() =>
        {
            GameObject go = new GameObject();
            var audioSource = go.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            return go;
        });

        InitVirbrate();
    }

    public void Start()
    {
    }

    public void Update()
    {
    }

    public void Dispose()
    {
        SDK<IHaptics>.Instance.Release();
    }

    #endregion 生命周期
}