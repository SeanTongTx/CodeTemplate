using EditorPlus;
using SeanLib.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
namespace SeanLib.CodeTemplate
{

    [CreateAssetMenu(fileName = "General", menuName = "CodeTemplate/New General Template", order = 1000)]
    public class TemplateAsset : ScriptableObject, ITemplate
    {
        [Obsolete("Use AssetDBHelper.LoadAsset instead ")]
        public static T LoadTemplateAsset<T>(string search) where T : TemplateAsset
        {
            return AssetDBHelper.LoadAsset<T>(search);
        }

        [SerializeField]
        [Multiline(30)]
        protected string template;
        [SerializeField]
        protected KeyWord[] keyWords;

        public string TemplateName => name;

        public string Template => template;

        public KeyWord[] KeyWords => keyWords;


        /// <summary>
        /// 将用户输入合并到生成器中
        /// </summary>
        /// <param name="UserInputs"></param>
        protected virtual void MergeValues(List<KeyWordValue> UserInputs)
        {
            var enumerator = UserInputs.GetEnumerator();
            while (enumerator.MoveNext())
            {
                SetValue(enumerator.Current.key, enumerator.Current.value);
            }
        }

        public virtual string Generate(List<KeyWordValue> UserInputs)
        {
            MergeValues(UserInputs);
            return Generater.Generate(Template, KeyWords, Values);
        }

        public virtual void Generate(List<KeyWordValue> UserInputs, string FilePath)
        {
            MergeValues(UserInputs);
            var codeStr = Generater.Generate(Template, KeyWords, Values);

            var filePath = Generater.Generate(FilePath, KeyWords, Values);

            FileTools.WriteAllText(filePath, codeStr);
            //AssetDatabase.Refresh();
            //自行刷新
        }

        #region 输入参数缓存
        /// <summary>
        /// 生成用缓存
        /// </summary>
        [HideInInspector]
        [SerializeField]
        public KeyWordDic ValueDic = new KeyWordDic();
        public List<KeyWordValue> Values => ValueDic.Values;

        public string GetValue(string key)
        {
            return ValueDic.GetValue(key);
        }

        public void SetValue(string key, string value)
        {
            ValueDic.SetValue(key, value);
        }
        #endregion

    }
}
