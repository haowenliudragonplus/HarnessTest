/************************************************
 * Config class : Table_Common_Audio
 * This file is can not be modify !!!
 ************************************************/

using System;
using System.Collections.Generic;
using Config;

namespace DragonPlus.Config.Common
{
    public partial class Table_Common_Audio:ConfigBase
    {   
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 声音资源ID
        /// </summary>
        public int AudioResourceId { get; set; }
        

        public override int GetId()
        {
            return Id;
        }
    }
}