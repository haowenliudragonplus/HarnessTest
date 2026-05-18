#if DEVELOPMENT_BUILD || UNITY_EDITOR

using DragonPlus.Core;
using DragonPlus.Save;
using Framework;
using TMGame;
using GameStorage;

public class OtherGM : GMBase
{
    protected override void RegisterAllCommand()
    {
        // 清空弹窗数据
        GetGMBuilder("弹窗/清空弹窗数据")
            .SetOnButtonClick((s1, s2, s3) => { SDK<IStorage>.Instance.Get<StorageClientCommon>().Popups.Clear(); })
            .Register();

        // 显示弹窗信息（进入次数）
        GetGMBuilder("弹窗/显示弹窗信息（进入次数）")
            .SetOnButtonClick((s1, s2, s3) =>
            {
                string str = string.Empty;
                foreach (var kvp in Game.GetMod<ModStorage>().GetStorage<StorageClientCommon>().Popups.EnterLevelTimePopDict)
                {
                    str += "界面id：" + kvp.Key + " 次数：" + kvp.Value + "\n";
                }
                UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
                {
                    showMidBtn = true,
                    content = str,
                    enableScroll = true,
                };
                Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
            })
            .Register();

        // 清空引导存档
        GetGMBuilder("引导/清空引导存档")
            .SetOnButtonClick((s1, s2, s3) =>
            {
                SDK<IStorage>.Instance.Get<StorageClientCommon>().Guide.Clear();
                var guide = Game.GetMod<GuideSys>();
                guide.ClearCacheFinishedGuide();
            })
            .Register();

        // 关闭全部引导
        GetGMBuilder("引导/关闭全部引导")
            .SetOnButtonClick((s1, s2, s3) => { Game.GetMod<GuideSys>().CloseAllGuide = true; })
            .Register();

        // 开启指定引导
        GetGMBuilder("引导/开启指定引导")
            .SetOnButtonClick((s1, s2, s3) =>
            {
                var guide = SDK<IStorage>.Instance.Get<StorageClientCommon>().Guide.GuideFinished;
                if (guide.ContainsKey("GUIDE_" + s1))
                {
                    guide.Remove("GUIDE_" + s1);
                }
                Game.GetMod<GuideSys>().ClearCacheFinishedGuide("GUIDE_" + s1);
            })
            .Register();

        // 完成指定引导
        GetGMBuilder("引导/完成指定引导")
            .SetOnButtonClick((s1, s2, s3) =>
            {
                var guide = SDK<IStorage>.Instance.Get<StorageClientCommon>().Guide.GuideFinished;
                if (!guide.ContainsKey("GUIDE_" + s1))
                {
                    guide.Add("GUIDE_" + s1, "GUIDE_" + s1);
                }
            })
            .Register();

        // 显示用户分层信息
        GetGMBuilder("分层/显示用户分层信息")
            .SetOnButtonClick((s1, s2, s3) =>
            {
                ulong playerId = SDK<IStorage>.Instance.Get<StorageCommon>().PlayerId;
                string infoStr = string.Empty;
                infoStr += $"当前AD总用户分组：{Game.GetMod<AdSys>().GetCurGroup()}\n";
                infoStr += $"当前RewardAD用户分组：{Game.GetMod<AdSys>().GetAdRewardCurrentGroup()}\n";
                infoStr += $"当前InterstitialAD用户分组：{Game.GetMod<AdSys>().GetAdInterstitialCurrentGroup()}\n";
                infoStr += $"当前BannerAD用户分组：{Game.GetMod<AdSys>().GetBannerCurrentGroup()}\n";
                infoStr += $"当前IAP总用户分组：{Game.GetMod<ModIAP>().GetCurGroup()}\n";
                infoStr += $"国家：{SDK<IStorage>.Instance.Get<StorageCommon>().Country}\n";
                UIView_Notice.OpenData viewData = new UIView_Notice.OpenData()
                {
                    showMidBtn = true,
                    content = infoStr,
                    enableScroll = true,
                };
                Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, viewData);
            })
            .Register();
        GetGMBuilder("成就/重置成就")
            .SetOnButtonClick((s1, s2, s3) =>
            {
               var storage =  Game.GetMod<ModStorage>().GetStorage<StorageInGame>().Achievement;
               // 清除所有成就进度
               storage.PassingExpert = 0;
               storage.DailyChallenge = 0;
               storage.DailyChallengeTime = 0;
               storage.DailyAttendanceCheckin = 0;
               storage.CheckinTime = 0;
               storage.PerfectClear = 0;
               storage.LevelLegend = 0;
               storage.ComebackPlayer = 0;
               storage.HeartPoundingMoment = 0;
               storage.UltimateMaster = 0;
               storage.WinStreakExpert = 0;
               storage.WinStreakExpertTime = 0;
               // 清除已领取的成就和可领取的成就
               storage.CollectedAchievement.Clear();
               storage.AchievementClaimable.Clear();
               storage.FirstOpen = false;
               CLog.Info("[GM] All achievements cleared successfully!");
            })
            .Register();
        GetGMBuilder("成就/清除每日挑战CD")
            .SetOnButtonClick((s1, s2, s3) =>
            {
                var storage =  Game.GetMod<ModStorage>().GetStorage<StorageInGame>().Achievement;
                storage.DailyChallengeTime = 0;
            })
            .Register();
        GetGMBuilder("成就/设置成就值")
            .SetOnButtonClick((s1, s2, s3) =>
            {
                // 验证 s1（成就类型）是否可以转换为数字
                if (!int.TryParse(s1, out int achievementType))
                {
                    CLog.Warning($"[GM] Invalid achievement type: {s1}, must be a number");
                    return;
                }

                // 验证 s2（进度值）是否可以转换为数字
                if (!int.TryParse(s2, out int progress))
                {
                    CLog.Warning($"[GM] Invalid progress value: {s2}, must be a number");
                    return;
                }
                // 调用成就模块设置进度
                Game.GetMod<ModAchievement>().SetAchievementTypeProgress(achievementType, progress);
               
            })
            .Register();
        
    }
}

#endif