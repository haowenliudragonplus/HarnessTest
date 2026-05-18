/************************************************
 * Storage class : StorageExtensionData
 * This file is can not be modify !!!
 * If there is some problem, ask hong.zhou.
 ************************************************/

using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using DragonPlus.Save;
using System.Collections.Generic;

namespace GameStorage
{
    [System.Serializable]
    public class StorageExtensionData : StorageBase
    {
        
        // 参数1
        [JsonProperty]
        string parm1 = "";
        [JsonIgnore]
        public string Parm1
        {
            get
            {
                return parm1;
            }
            set
            {
                if(parm1 != value)
                {
                    parm1 = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
        // 参数2
        [JsonProperty]
        string parm2 = "";
        [JsonIgnore]
        public string Parm2
        {
            get
            {
                return parm2;
            }
            set
            {
                if(parm2 != value)
                {
                    parm2 = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
        // 参数3
        [JsonProperty]
        string parm3 = "";
        [JsonIgnore]
        public string Parm3
        {
            get
            {
                return parm3;
            }
            set
            {
                if(parm3 != value)
                {
                    parm3 = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
        // 参数4
        [JsonProperty]
        string parm4 = "";
        [JsonIgnore]
        public string Parm4
        {
            get
            {
                return parm4;
            }
            set
            {
                if(parm4 != value)
                {
                    parm4 = value;
                    Profile.Instance.UpdateLocalVersion();
                    
                    
                }
            }
        }
        // ---------------------------------//
        
    }
}