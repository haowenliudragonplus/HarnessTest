using System.Collections;
using System.Collections.Generic;
using DragonU3DSDK.Network.API.Protocol;
using Framework;
using GameStorage;
using TMPro;
using UnityEngine;

#if DEVELOPMENT_BUILD || UNITY_EDITOR

public class InGameGM : GMBase
{
    protected override void RegisterAllCommand()
    {
        GetGMBuilder("显示信息")
          .SetOnButtonClick((str1, str2, str3) =>
          {
              if (!Game.GetMod<ModFsm>().CheckState<FsmState_InGame>())
                  return;
              var mode = (Game.GetMod<ModFsm>().CurState as FsmState_InGame).Mode;

              string infoStr = string.Empty;
              infoStr += $"id：{mode.Data.LevelIndex + 1}\n";
              infoStr += $"序号：{mode.Data.LevelIndex + 1}\n";
              infoStr += $"模式：{mode.Data.ModeType}\n";
              infoStr += $"json文件名：{mode.Data.JsonFileName}\n";
              infoStr += $"连赢次数：{mode.Data.TempWinStreakCount}\n";
              infoStr += $"连败次数：{mode.Data.TempLoseStreakCount}\n";
              infoStr += $"箭头数量：{mode.Data.LevelData.level.Count}\n";
              UIView_Notice.OpenData openData = new UIView_Notice.OpenData()
              {
                  showMidBtn = true,
                  content = infoStr,
                  enableScroll = true,
                  useText = true,
              };
              Game.GetMod<ModUI>().OpenSync(UIViewName.UIView_Notice, openData);
          })
          .Register();

        GetGMBuilder("设置关卡")
            .SetOnButtonClick((str1, str2, str3) =>
            {
                if (string.IsNullOrEmpty(str2))
                {
                    str2 = ((int)EInGameModeType.Main).ToString();
                }
                if (!int.TryParse(str1, out int _levelIndex))
                    return;
                if (!int.TryParse(str2, out int _levelMode))
                    return;
                Game.GetMod<ModStorage>().GetStorage<StorageMahjongScrew>().LevelIndexDict[_levelMode] = _levelIndex - 1;
            })
            .SetTipStr("参数1：关卡ID\n参数2：模式类型（1：主线）")
            .Register();

        GetGMBuilder("直接进入关卡")
            .SetOnButtonClick((str1, str2, str3) =>
            {
                if (string.IsNullOrEmpty(str1))
                    return;
                EInGameModeType modeType = EInGameModeType.Main;
                if (int.TryParse(str2, out var _mode))
                {
                    modeType = (EInGameModeType)_mode;
                }
                else
                {
                    modeType = EInGameModeType.Main;
                }
                Game.GetMod<ModInGame>().EnterGame_GM(str1, modeType);
            })
            .SetTipStr("参数1：输入关卡json文件名\n参数2：模式类型（1：主玩法（默认））")
            .SetCloseViewAfterExecute()
            .Register();

        GetGMBuilder("本关胜利")
            .SetOnButtonClick((str1, str2, str3) =>
            {
                if (Game.GetMod<ModFsm>().CurState is not FsmState_InGame)
                    return;
                var mode = ((FsmState_InGame)Game.GetMod<ModFsm>().CurState)?.Mode;
                if (mode == null)
                    return;
                foreach (var arrowEntity in mode.Data.GetArrowEntityList())
                {
                    arrowEntity.Holder.gameObject.SetActive(false);
                }
                mode.HandleGameWin();
            })
            .SetCloseViewAfterExecute()
            //.SetTipStr("参数1：最终得分（可选）")
            .Register();

        GetGMBuilder("本关失败")
            .SetOnButtonClick((str1, str2, str3) =>
            {
                if (Game.GetMod<ModFsm>().CurState is not FsmState_InGame)
                    return;
                var mode = ((FsmState_InGame)Game.GetMod<ModFsm>().CurState)?.Mode;
                if (mode == null)
                    return;
                switch (mode.Data.Group_HpOrStep)
                {
                    case EABTestGroup.Group2:
                        mode.HandleGameFail(EInGameFailType.NoStep);
                        break;
                    default:
                        mode.HandleGameFail(EInGameFailType.NoHp);
                        break;
                }

            })
            .SetCloseViewAfterExecute()
            .Register();

        GetGMBuilder("设置步数")
        .SetOnButtonClick((str1, str2, str3) =>
        {
            if (Game.GetMod<ModFsm>().CurState is not FsmState_InGame)
                return;
            var mode = ((FsmState_InGame)Game.GetMod<ModFsm>().CurState)?.Mode;
            if (mode == null)
                return;
            if (mode.Data.Group_HpOrStep != EABTestGroup.Group2)
                return;
            if (!int.TryParse(str1, out int _step))
                return;
            mode.Data.SetStep(_step);

        })
        .SetTipStr("参数1：步数")
        .SetCloseViewAfterExecute()
        .Register();


        // GetGMBuilder("添加道具99999")
        //     .SetOnButtonClick((str1, str2, str3) =>
        //     {
        //         if (Game.GetMod<ModFsm>().CurState is not FsmState_InGame)
        //             return;
        //         AddItem(EItemType.Shuffle);
        //         AddItem(EItemType.Hint);
        //         AddItem(EItemType.Clear);
        //         AddItem(EItemType.AddSlot);
        //     })
        //     .SetCloseViewAfterExecute()
        //     .Register();
        // void AddItem(EItemType itemType)
        // {
        //     Game.GetMod<ModBag>().AddItem(itemType, 99999,
        //             new BIHelper.ItemChangeReasonArgs(BiEventArrowPuzzle1.Types.ItemChangeReason.Debug)
        //             {
        //                 data1 = ((int)itemType).ToString(),
        //                 data2 = 99999.ToString(),
        //             });
        // }

        // GetGMBuilder("道具清空")
        //     .SetOnButtonClick((str1, str2, str3) =>
        //     {
        //         if (Game.GetMod<ModFsm>().CurState is not FsmState_InGame)
        //             return;
        //         RemoveItem(EItemType.Shuffle);
        //         RemoveItem(EItemType.Hint);
        //         RemoveItem(EItemType.Clear);
        //         RemoveItem(EItemType.AddSlot);
        //     })
        //     .SetCloseViewAfterExecute()
        //     .Register();
        // void RemoveItem(EItemType itemType)
        // {
        //     int curCount = Game.GetMod<ModBag>().GetItemCount(itemType);
        //     Game.GetMod<ModBag>().AddItem(itemType, -curCount,
        //             new BIHelper.ItemChangeReasonArgs(BiEventArrowPuzzle1.Types.ItemChangeReason.Debug)
        //             {
        //                 data1 = ((int)itemType).ToString(),
        //                 data2 = (-curCount).ToString(),
        //             });
        // }

        GetGMBuilder("线条动画")
            .SetOnButtonClick((str1, str2, str3) =>
            {
                if (Game.GetMod<ModFsm>().CurState is not FsmState_InGame)
                    return;

                LineRendererUtils.LineAnimSpeed = float.TryParse(str1, out float value1) ? value1 : 100f;
                LineRendererUtils.PointsPerSegment = int.TryParse(str2, out int value2) ? value2 : 2;

                var data = ((FsmState_InGame)Game.GetMod<ModFsm>().CurState)?.Mode?.Data;
                if (data == null)
                    data.DisplayLineEnterAnim();
            })
            .SetTipStr("参数1：速度,默认100f \n 参数2：插值整数,默认2")
            .SetCloseViewAfterExecute()
            .Register();

        #region 局内相机移动

        // GetGMBuilder("相机移动/拖拽边界")
        //     .SetOnButtonClick((str1, str2, str3) =>
        //     {
        //         if (!float.TryParse(str1, out float value1))
        //             return;
        //         if (!float.TryParse(str2, out float value2))
        //             return;
        //
        //         if (Game.GetMod<ModFsm>().CurState is not FsmState_InGame)
        //             return;
        //         var camera = ((FsmState_InGame)Game.GetMod<ModFsm>().CurState)?.Mode?.CameraInput;
        //         if (camera == null)
        //             return;
        //         var settings = camera.GetSettings();
        //         settings.maxBounds = new Vector2(value1, value2);
        //         settings.minBounds = new Vector2(-value1, -value2);
        //     })
        //     .SetTipStr("参数1：X轴默认15 \n 参数2：Y轴默认30")
        //     .Register();

        GetGMBuilder("相机移动/拖拽速度")
            .SetOnButtonClick((str1, str2, str3) =>
            {
                if (Game.GetMod<ModFsm>().CurState is not FsmState_InGame)
                    return;
                var camera = ((FsmState_InGame)Game.GetMod<ModFsm>().CurState)?.Mode?.CameraInput;
                if (camera == null)
                    return;
                var settings = camera.GetSettings();
                settings.dragSpeed = float.TryParse(str1, out float value1) ? value1 : 1.5f;
            })
            .SetTipStr("参数1：速度默认1.5")
            .Register();

        GetGMBuilder("相机移动/拖拽生效距离")
            .SetOnButtonClick((str1, str2, str3) =>
            {
                if (Game.GetMod<ModFsm>().CurState is not FsmState_InGame)
                    return;
                var camera = ((FsmState_InGame)Game.GetMod<ModFsm>().CurState)?.Mode?.CameraInput;
                if (camera == null)
                    return;
                var settings = camera.GetSettings();
                settings.dragEffectDistance = float.TryParse(str1, out float value1) ? value1 : 3f;
            })
            .SetTipStr("参数1：默认3像素长度")
            .Register();

        GetGMBuilder("相机移动/拖拽生效时长")
            .SetOnButtonClick((str1, str2, str3) =>
            {
                if (Game.GetMod<ModFsm>().CurState is not FsmState_InGame)
                    return;
                var camera = ((FsmState_InGame)Game.GetMod<ModFsm>().CurState)?.Mode?.CameraInput;
                if (camera == null)
                    return;
                var settings = camera.GetSettings();
                settings.dragEffectTime = float.TryParse(str1, out float value1) ? value1 : 0.06f;
            })
            .SetTipStr("参数1：默认0.06")
            .Register();

        GetGMBuilder("相机移动/拖拽缓动参数")
            .SetOnButtonClick((str1, str2, str3) =>
            {
                if (Game.GetMod<ModFsm>().CurState is not FsmState_InGame)
                    return;
                var camera = ((FsmState_InGame)Game.GetMod<ModFsm>().CurState)?.Mode?.CameraInput;
                if (camera == null)
                    return;
                var settings = camera.GetSettings();
                settings.dragSmoothTime = float.TryParse(str1, out float value1) ? value1 : 0.03f;
            })
            .SetTipStr("参数1：默认0.03f 平滑时间值越小越跟手")
            .Register();

        GetGMBuilder("相机移动/拖拽惯性")
            .SetOnButtonClick((str1, str2, str3) =>
            {
                if (Game.GetMod<ModFsm>().CurState is not FsmState_InGame)
                    return;
                var camera = ((FsmState_InGame)Game.GetMod<ModFsm>().CurState)?.Mode?.CameraInput;
                if (camera == null)
                    return;
                var settings = camera.GetSettings();
                settings.dragInertiaDuration = float.TryParse(str1, out float value1) ? value1 : 0.5f;
                settings.dragInertiaSpeed = float.TryParse(str1, out float value2) ? value2 : 0.8f;
            })
            .SetTipStr("参数1：惯性时间0.5")
            .Register();

        GetGMBuilder("相机移动/防连点间隔")
            .SetOnButtonClick((str1, str2, str3) =>
            {
                if (Game.GetMod<ModFsm>().CurState is not FsmState_InGame)
                    return;
                var camera = ((FsmState_InGame)Game.GetMod<ModFsm>().CurState)?.Mode?.CameraInput;
                if (camera == null)
                    return;
                var settings = camera.GetSettings();
                settings.doubleTapTimeThreshold = float.TryParse(str1, out float value1) ? value1 : 0.3f;
            })
            .SetTipStr("参数1：默认0.3秒")
            .Register();

        GetGMBuilder("相机移动/缩放大小")
            .SetOnButtonClick((str1, str2, str3) =>
            {
                if (Game.GetMod<ModFsm>().CurState is not FsmState_InGame)
                    return;
                var camera = ((FsmState_InGame)Game.GetMod<ModFsm>().CurState)?.Mode?.CameraInput;
                if (camera == null)
                    return;
                var settings = camera.GetSettings();
                settings.maxSize = float.TryParse(str1, out float value1) ? value1 : 16;
                settings.minSize = float.TryParse(str2, out float value2) ? value2 : 10;
            })
            .SetTipStr("参数1：最大值默认16 \n 参数2：最小值默认10")
            .Register();

        GetGMBuilder("相机移动/缩放速度")
            .SetOnButtonClick((str1, str2, str3) =>
            {
                if (Game.GetMod<ModFsm>().CurState is not FsmState_InGame)
                    return;
                var camera = ((FsmState_InGame)Game.GetMod<ModFsm>().CurState)?.Mode?.CameraInput;
                if (camera == null)
                    return;
                var settings = camera.GetSettings();
                settings.zoomSpeed = float.TryParse(str1, out float value1) ? value1 : 0.3f;
                settings.zoomSmoothTime = float.TryParse(str2, out float value2) ? value2 : 0.08f;
            })
            .SetTipStr("参数1：速度默认0.1 \n 参数2：平滑参数默认0.08")
            .Register();

        GetGMBuilder("相机移动/聚焦")
            .SetOnButtonClick((str1, str2, str3) =>
            {
                if (Game.GetMod<ModFsm>().CurState is not FsmState_InGame)
                    return;
                var mode = ((FsmState_InGame)Game.GetMod<ModFsm>().CurState)?.Mode;
                if (mode == null)
                    return;

                var arrowIndex = int.TryParse(str1, out int value1) ? value1 : 1;
                var pos = mode.Data.GetArrowEntityList()[arrowIndex].Go_Arrow.transform.position;
                mode.CameraInput.Focus(pos);

            })
            .SetTipStr("参数1：速度默认0.3 \n 参数2：平滑参数默认0.08")
            .SetCloseViewAfterExecute()
            .Register();

        #endregion

        GetGMBuilder("工具/开启无敌模式", EGMCommandType.Toggle)
                   .SetOnToggleChanged(b =>
                   {
                       Game.GetMod<ModInGame>().EnableInvincibleMode = b;
                   })
                   .SetGetToggleInitStateEvent(() =>
                   {
                       return Game.GetMod<ModInGame>().EnableInvincibleMode;
                   })
                   .SetTipStr("开启后不掉血")
                   .Register();
    }
}

#endif
