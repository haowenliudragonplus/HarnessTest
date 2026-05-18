using DragonPlus.Core;
using DragonPlus.Save;
using Framework;
using GameStorage;

#if DEVELOPMENT_BUILD || UNITY_EDITOR

public class ActivityGM : GMBase
{
    protected override void RegisterAllCommand()
    {
        #region 去广告礼包

        GetGMBuilder("去广告礼包/重置")
            .SetOnButtonClick((s1, s2, s3) =>
            {
                Game.GetMod<ModStorage>().GetStorage<StorageActivity>().RemoveAd.IsBuy = false;
            })
            .Register();

        #endregion 去广告礼包
    }
}

#endif