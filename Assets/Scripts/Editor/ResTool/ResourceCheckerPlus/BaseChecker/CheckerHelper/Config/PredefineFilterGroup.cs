using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceCheckerPlus
{
    public class PredefineFilterGroup : ScriptableObject
    {
        public string filterGroupName;
        public FilterItemCfg[] filterCfgGroup;
    }

    [System.Serializable]
    public class FilterItemCfg
    {
        public string checkItemName;
        public string filterString;
        public bool positive;
    }
}
