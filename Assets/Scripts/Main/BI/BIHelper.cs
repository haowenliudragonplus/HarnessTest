using System;
using System.Reflection;
using DragonPlus.Core;
using DragonPlus.Tracking;
using DragonU3DSDK;
using DragonU3DSDK.Network.API.Protocol;
using DragonU3DSDK.Network.BI;
using Framework;
using Google.Protobuf;

// 主工程的BIHelper
namespace Launcher
{
    public static class BIHelper
    {
        private static void SendEvent(IMessage message)
        {
            try
            {
                var biEvent = new BiEventArrowPuzzle1() { };
                var messageName = message.GetType().Name;
                biEvent.GetType().InvokeMember(messageName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, Type.DefaultBinder, biEvent,
                    new object[] { message });
                SDK<ITracking>.Instance.SendBiTracking(biEvent);
            }
            catch (Exception e)
            {
                CLog.Error(e.ToString());
            }
        }

        public static void SendGameEvent(BiEventArrowPuzzle1.Types.GameEventType gameEventType, bool isAuto = false)
        {
            var gameEvent = new BiEventArrowPuzzle1.Types.GameEvent
            {
                GameEventType = gameEventType,
            };

            SendEvent(gameEvent);
        }
    }
}