using System;
using UnityEngine;
using TMPro;
using System.Collections;
using DragonPlus.Core;

namespace DragonPlus
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TextMeshPro))]
    public class LocalizeTextMeshPro : AbstractLocalize
    {
        [SerializeField]
        private string m_Matrial;

        public bool isAutoChangeMatrial = true;
        public TextMeshPro m_TmpText;

        private void Awake()
        {
            m_TmpText = transform.GetComponent<TextMeshPro>();
            m_Matrial = GetMaterialSuffix2(m_TmpText.fontSharedMaterial.name);
        }

        protected override void Localize()
        {
            SetText();
        }

#if UNITY_EDITOR
        private void LateUpdate()
        {
            if (!Application.isPlaying)
                m_Matrial = GetMaterialSuffix2(m_TmpText.fontSharedMaterial.name);
        }
#endif

        public void SetMaterialName(string matName)
        {
            if (string.Equals(m_Matrial, matName))
                return;
            m_Matrial = matName;
            Localize();
        }

        public void SetMaterialSuffix(string matSuffix)
        {
            m_Matrial = matSuffix;
        }

        public TextMeshPro GetTmpText()
        {
            return m_TmpText;
        }

        private void SetText()
        {
            if (string.IsNullOrEmpty(Term))
            {
                SetFont();
                return;
            }

            var translation = CoreUtils.GetLocalization(Term);
            if (!String.IsNullOrEmpty(translation))
            {
                SetText(translation, false);
            }
            else
            {
                SetText("", false);
                //                Log.Error(transform.name + " ### LocalizeTextMeshProUGUI Term error: " + Term + " ###");
            }
        }

        public void SetText(string str, bool clearTerm = true)
        {
            if (str == null)
                return;

            if (m_TmpText == null)
                m_TmpText = transform.GetComponent<TextMeshPro>();

            if (m_TmpText != null)
            {
                m_TmpText.SetText(str);
                //单语言没必要一直加载字体 
                SetFont();
                CurrLocalize = Game.GetMod<ModLanguage>().CurLanguage;
            }
            else
                Log.Error("### LocalizeTextMeshPro Component error: " + str + " ###");

            if (clearTerm)
            {
                Term = null;
            }
        }

        public string GetText()
        {
            if (m_TmpText == null)
                Awake();
            if (m_TmpText != null)
                return m_TmpText.text;
            else
            {
                Log.Error("### LocalizeTextMeshPro Component error: " + gameObject.name + " ###");
                return string.Empty;
            }
        }

        public void SetColor(Color color)
        {
            if (m_TmpText == null)
                Awake();
            if (m_TmpText != null)
                m_TmpText.color = color;
            else
                Log.Error("### LocalizeTextMeshProUGUI Component error: " + gameObject.name + " ###");
        }

        public Color GetColor()
        {
            if (m_TmpText == null)
                Awake();
            if (m_TmpText != null)
                return m_TmpText.color;

            return Color.white;
        }

        public void HideAlpha()
        {
            var c = GetColor();
            c.a = 0;
            SetColor(c);
        }

        public void ShowAlpha()
        {
            var c = GetColor();
            c.a = 1;
            SetColor(c);
        }

        public void SetEffectColor(Color color)
        {
            //GetComponent<TextMeshProUGUI>().e = color;
        }

        public void DoFade(float endvalue, float duration, Action cb = null)
        {
            StartCoroutine(dofade(endvalue, duration, cb));
        }

        private IEnumerator dofade(float endvalue, float time, Action cb)
        {
            int count = (int)(30 * time); // 1秒变化30次
            float timeDelta = time / count;
            float valueDelta = (float)(endvalue - m_TmpText.color.a) / count;
            var c = GetColor();
            float startvalue = endvalue > 0 ? 0 : 1f;
            for (int i = 0; i <= count; i++)
            {
                if (i == count)
                {
                    c.a = endvalue;
                    SetColor(c);
                }
                else
                {
                    c.a = startvalue + i * valueDelta;
                    SetColor(c);
                }

                //yield return new WaitForSeconds(timeDelta);
                yield return new WaitForEndOfFrame();
            }

            cb?.Invoke();
        }

        private void SetFont()
        {
            if (!Application.isPlaying)
                return;

            if (!String.IsNullOrEmpty(m_Matrial))
            {
                LocalizeFont();
            }

            LocalizeMaterial();

#if UNITY_EDITOR
            LocalizeTMPSetting();
#endif
        }

        private void LocalizeFont()
        {
            TMP_FontAsset localizeFont = Game.GetMod<ModLanguage>().GetTMPFontAsset(Game.GetMod<ModLanguage>().CurLanguage);
            if (localizeFont != null)
            {
                m_TmpText.font = localizeFont;
            }
        }

        private void LocalizeMaterial()
        {
            if (!isAutoChangeMatrial)
                return;

            if (!string.IsNullOrEmpty(m_Matrial) && !m_Matrial.Equals("Material"))
            {
                Material material = Game.GetMod<ModLanguage>().GetTMPMat(Game.GetMod<ModLanguage>().CurLanguage, m_Matrial);
                if (material != null)
                    m_TmpText.fontMaterial = material;
                else
                {
                    Log.Error(transform.name + "  ######  " + m_Matrial + "\t" + gameObject.name);
                }
            }
        }

        private void LocalizeTMPSetting()
        {
            if (!string.IsNullOrEmpty(m_Matrial))
            {
                TMPSettingParam localizeParam = Game.GetMod<ModLanguage>().GetTMPSetting(m_Matrial);
                if (m_TmpText != null)
                {
                    m_TmpText.characterSpacing = localizeParam.CharacterSpacing;
                    m_TmpText.lineSpacing = localizeParam.LineSpacing;
                    m_TmpText.wordSpacing = localizeParam.WordSpacing;
                    m_TmpText.paragraphSpacing = localizeParam.ParagraphSpacing;
                }
            }
        }

        private static string GetMaterialSuffix2(string materialName)
        {
            // LocaleFont_En SDF combo Shadow 2
            string mName = materialName;
            if (mName.EndsWith("(Instance)"))
            {
                mName = mName.Substring(0, mName.LastIndexOf("(Instance)"));
            }
            if (mName.LastIndexOf("SDF") > -1)
            {
                mName = mName.Substring(mName.LastIndexOf("SDF") + 3);
            }
            if (mName.EndsWith(")"))
            {
                mName = mName.Substring(0, mName.LastIndexOf(")"));
            }
            mName = mName.Trim();

            return mName;
        }
    }
}