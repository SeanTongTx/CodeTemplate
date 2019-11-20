using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
namespace SeanLib.CodeTemplate
{
    //[CreateAssetMenu(fileName = "Template", menuName = "CodeTemplate/NewTemplate",order =1000)]
    public abstract class TemplateAsset : ScriptableObject, ITemplate
    {
        [SerializeField]
        [Multiline]
        private string template;
        [SerializeField]
        private string[] keyWords;

        public string TemplateName => name;

        public string Template => template;

        public string[] KeyWords => keyWords;

        public virtual string Generate(Dictionary<string, string> KeyValues)
        {
            return Generate(this.Template, KeyValues);
        }

        public virtual void Generate(Dictionary<string, string> KeyValues, string FilePath)
        {
            throw new System.Exception("Un handled");
        }

        /// <summary>
        /// 生成字符
        /// </summary>
        /// <param name="KeyValues"></param>
        /// <returns></returns>
        protected virtual string Generate(string template, Dictionary<string, string> KeyValues)
        {
            StringBuilder sb = new StringBuilder(template);
            for (int i = 0; i < KeyWords.Length; i++)
            {
                if (KeyValues.ContainsKey(KeyWords[i]))
                    sb.Replace(KeyWords[i], KeyValues[KeyWords[i]]);
            }
            return sb.ToString();
        }

    }
}
