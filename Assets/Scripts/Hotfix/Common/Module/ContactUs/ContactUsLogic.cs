// **********************************************
// Copyright(c) 2021 by com.ustar
// All right reserved
// 
// Author : Jian.Wang
// Date : 2023/07/07/18:49
// Ver : 1.0.0
// Description : ContactUsLogic.cs
// ChangeLog :
// **********************************************

using System;
using DragonPlus.Core;
using DragonPlus.Native.Bridge;
using DragonPlus.Network;
using DragonPlus.Save;
using DragonU3DSDK;
using DragonU3DSDK.Network;
using DragonU3DSDK.Network.API;
using DragonU3DSDK.Network.API.Protocol;

using Google.Protobuf;
using UnityEngine;

namespace TMGame
{
    public class ContactUsLogic : ModuleBase
    {
        public SListUserComplainMessage MessageList { get; set; }
        Action ResultCB { get; set; }
        Action ErrorCB { get; set; }

        // 发送的消息缓存,如果成功加到队列
        public UserComplainMessage TempSendMessage { get; set; }

        /// <summary>
        /// 检测是否有邮件回复
        /// </summary>
        public void OnCheckUserComaplainMessage()
        {
            CCheckUserComplainMessage cCheckUserComaplainMessage = new CCheckUserComplainMessage();
            SDK<INetwork>.Instance.HandleRequest<CCheckUserComplainMessage, SCheckUserComplainMessage>(cCheckUserComaplainMessage,
                (resp) =>
                {
                    SCheckUserComplainMessage sCheckUserComaplainMessage = (SCheckUserComplainMessage) resp;
                    if (sCheckUserComaplainMessage.Result)
                    {
                      //  RedPointCenter.Instance.Set(RedPointCenter.ContactUsRedPointKey, 1);
                    }
                },
                (errno, errmsg, resp) => { });
        }

        /// <summary>
        /// 请求消息列表
        /// </summary>
        /// <param name="resultCB"></param>
        /// <param name="errorCB"></param>
        public void OnRequestMessageList(Action resultCB, Action errorCB)
        {
            var cListUserComplainMessage = new CListUserComplainMessage();
            ResultCB = resultCB;
            ErrorCB = errorCB;
            SDK<INetwork>.Instance.Send<CListUserComplainMessage, SListUserComplainMessage>(cListUserComplainMessage,
                OnGetMessageListResult, OnGetMessageListError);
        }

        // 获得消息列表成功
        void OnGetMessageListResult(IMessage result)
        {
            MessageList = (SListUserComplainMessage) result;
            ResultCB();
        }

        // 获得消息列表失败
        void OnGetMessageListError(ErrorCode errno, string errmsg, IMessage resp)
        {
            ErrorCB();
            Log.Error("ContactUs get messageList failed errno = {0} errmsg = {1}", errno, errmsg);
        }

        // 给客服发送消息
        public void SenMyMessage(string email, string message, Action resultCB, Action errorCB)
        {
            ResultCB = resultCB;
            ErrorCB = errorCB;

            var storageCommon = SDK<IStorage>.Instance.Get<StorageCommon>();
            var cSendUserComplainMessage = new CSendUserComplainMessage
            {
                Message = new UserComplainMessage
                {
                    Message = message,
                    MessageType = UserComplainMessage.Types.MessageType.Complain,
                    PlayerId = SDK<IStorage>.Instance.Get<StorageCommon>().PlayerId,
                    CreatedAt = (ulong) SDKUtil.TimeDate.CurrentTimeInMilliseconds(),
                    Email = email,
                    RevenueUsdCents = storageCommon.RevenueUSDCents,
                    DeviceType = SDK<INative>.Instance.GetDeviceType(),
                    DeviceModel = SDKUtil.Device.DeviceModel,
                    DeviceMemory = SDKUtil.Device.SystemMemorySize.ToString(),
                    NetworkType = Application.internetReachability.ToString(),
                    ResVersion = GameConfig.CurResVersion.ToString(),
                    NativeVersion = SDK<INative>.Instance.GetVersionCode().ToString(),
                    Platform = SDKUtil.Device.GetPlatform().Convert(),
                }
            };
            TempSendMessage = cSendUserComplainMessage.Message;
            SDK<INetwork>.Instance.Send<CSendUserComplainMessage, SSendUserComplainMessage>(cSendUserComplainMessage,
                OnSendMessageResult, OnSendMessageError);
        }

        // 给客服发送消息成功
        void OnSendMessageResult(IMessage result)
        {
            ResultCB();
        }

        // 给客服发送消息失败
        void OnSendMessageError(ErrorCode errno, string errmsg, IMessage resp)
        {
            Log.Error("ContactUs send message failed errno = {0} errmsg = {1}", errno, errmsg);
            ErrorCB();
        }

        public string GetSubscribeEmailAddress()
        {
            var storageCommon = SDK<IStorage>.Instance.Get<StorageCommon>();
            if (!string.IsNullOrEmpty(storageCommon.Email))
            {
                return storageCommon.Email;
            }

            if (!string.IsNullOrEmpty(storageCommon.FacebookEmail))
            {
                return storageCommon.FacebookEmail;
            }

            return "";
        }

        public void ForceInit()
        {
        }
    }
}