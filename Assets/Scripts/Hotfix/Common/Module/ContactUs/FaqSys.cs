// **********************************************
// Copyright(c) 2021 by com.ustar
// All right reserved
// 
// Author : Jian.Wang
// Date : 2023/07/10/09:59
// Ver : 1.0.0
// Description : FaqSys.cs
// ChangeLog :
// **********************************************

using System;
using System.Collections;
using System.Collections.Generic;
using DragonPlus;
using DragonPlus.Core;
using DragonPlus.Network;
using DragonU3DSDK;
using DragonU3DSDK.Network.API;
using DragonU3DSDK.Network.API.Protocol;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace TMGame
{
    public class FaqSys : ModuleBase
    {
        public List<ContactUsFaqConfig> FaqList;
        public Dictionary<int, ContactUsFaqConfig> FaqDic;

        public List<ContactUsI18nConfig> I18nList;
        public Dictionary<string, ContactUsI18nConfig> I18nDic;

        public override void OnStart()
        {
            InitConfigs();

            base.OnStart();
        }

        public void InitConfigs()
        {
            var configPath = "faq";
            var ta = Game.GetMod<ModAsset>().GetRes<TextAsset>(configPath).GetInstance(Game.DontDestoryRoot);
            var jsStr = ta.text;

            if (String.IsNullOrEmpty(jsStr))
            {
                Log.Error("Load contactus config error!");
                return;
            }

            InitConfigForString(jsStr);
            GetServerConfig();
        }

        public void GetServerConfig()
        {
            CGetConfig cGetConfig = new CGetConfig
            {
                Route = "config_faq",
            };

            SDK<IRemoteRequest>.Instance.HandleRequest(cGetConfig, (SGetConfig sGetConfig) =>
            {
                if (string.IsNullOrEmpty(sGetConfig.Config.Json))
                {
                    Log.Warning("config_faq 服务器配置为空！");
                    return;
                }

                try
                {
                    JObject obj = JObject.Parse(sGetConfig.Config.Json);
                    LoadRemoteConfig(sGetConfig.Config.Json);
                }
                catch (Exception e)
                {
                }
            }, (errno, errmsg, resp) => { });
        }

        public void LoadRemoteConfig(string jsStr)
        {
            if (String.IsNullOrEmpty(jsStr))
            {
                Log.Error("Load remote contactus config error!");
                return;
            }

            InitConfigForString(jsStr);
        }

        public void InitConfigForString(string jsStr)
        {
            Hashtable table = JsonConvert.DeserializeObject<Hashtable>(jsStr);

            object tempObj = table["faq"];
            String tempObjStr = JsonConvert.SerializeObject(tempObj);
            FaqList = JsonConvert.DeserializeObject<List<ContactUsFaqConfig>>(tempObjStr);
            FaqDic = new Dictionary<int, ContactUsFaqConfig>();

            for (int i = 0; i < FaqList.Count; i++)
            {
                ContactUsFaqConfig config = FaqList[i];

                FaqDic.Add(config.Id, config);
            }

            tempObj = table["i18n"];
            tempObjStr = JsonConvert.SerializeObject(tempObj);
            I18nList = JsonConvert.DeserializeObject<List<ContactUsI18nConfig>>(tempObjStr);
            I18nDic = new Dictionary<string, ContactUsI18nConfig>();

            for (int i = 0; i < I18nList.Count; i++)
            {
                ContactUsI18nConfig config = I18nList[i];
                config.InitI18n();

                I18nDic.Add(config.Key, config);
            }
        }

        public string GetAnswer(int id)
        {
            ContactUsFaqConfig config;
            if (FaqDic.TryGetValue(id, out config))
            {
                ContactUsI18nConfig i18nConfig;
                if (I18nDic.TryGetValue(config.Answer, out i18nConfig))
                {
                    string locale = Game.GetMod<ModLanguage>().CurLanguage;
                    return i18nConfig.I18nDic[locale];
                }
            }

            return "";
        }

        public string GetQuestion(int id)
        {
            ContactUsFaqConfig config;
            if (FaqDic.TryGetValue(id, out config))
            {
                ContactUsI18nConfig i18nConfig;
                if (I18nDic.TryGetValue(config.Question, out i18nConfig))
                {
                    string locale = Game.GetMod<ModLanguage>().CurLanguage;
                    return i18nConfig.I18nDic[locale];
                }
            }

            return "";
        }

        public List<ContactUsFaqConfig> GetQuestions(int id)
        {
            List<ContactUsFaqConfig> questions = new List<ContactUsFaqConfig>();
            ContactUsFaqConfig config;
            if (FaqDic.TryGetValue(id, out config))
            {
#if UNITY_ANDROID
                if (config.AndroidQuestionList != null)
                {
                    for (int i = 0; i < config.AndroidQuestionList.Count; i++)
                    {
                        int key = config.AndroidQuestionList[i];
                        ContactUsFaqConfig qc = FaqDic[key];
                        questions.Add(qc);
                    }
                }
#elif UNITY_IOS
                if (config.IosQuestionList != null)
                {
                    for (int i = 0; i < config.IosQuestionList.Count; i++)
                    {
                        int key = config.IosQuestionList[i];
                        ContactUsFaqConfig qc = FaqDic[key];
                        questions.Add(qc);
                    }
                }
#endif
            }

            return questions;
        }
    }
}