using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DragonPlus
{
    public abstract class AbstractLocalize : MonoBehaviour
    {
        [SerializeField]
        protected string Term;

        protected string CurrLocalize = string.Empty;

        [SerializeField]
        protected bool UseGlobalSetting = true;

        public string Key
        {
            get
            {
                return Term;
            }
            set
            {
                Term = value;
            }
        }

        public string GetTrrm()
        {
            return Term;
        }

        protected abstract void Localize();

        private void OnEnable()
        {
            if (NeedChangeLocalize())
                Localize();

            //LocalizationManager.OnLocalization += Localize;
        }

        //private void OnDisable()
        //{
        //    //LocalizationManager.OnLocalization -= Localize;
        //}

        //private void OnDestroy()
        //{
        //    //LocalizationManager.OnLocalization -= Localize;
        //}

        private void Start()
        {
            Localize();
        }

        public void SetTerm(string term)
        {
            this.Term = term;
            Localize();
        }

        public string GetTerm()
        {
            return this.Term;
        }

        private bool NeedChangeLocalize()
        {
            if (!Game.GameInited)
                return false;
            return CurrLocalize != Game.GetMod<ModLanguage>().CurLanguage;
        }
    }
}