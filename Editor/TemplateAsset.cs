using SeanLib.Core;
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
        [SerializeField]
        [Multiline(30)]
        private string template;
        [SerializeField]
        private KeyWord[] keyWords;

        public string TemplateName => name;

        public string Template => template;

        public KeyWord[] KeyWords => keyWords;

        public virtual string Generate(Dictionary<string, string> KeyValues)
        {
            StringBuilder sb = new StringBuilder(Template);
            for (int i = 0; i < KeyWords.Length; i++)
            {
                if (KeyValues.ContainsKey(KeyWords[i].key))
                    sb.Replace(KeyWords[i].key, KeyValues[KeyWords[i].key]);
            }
            return sb.ToString();
        }

        public virtual void Generate(Dictionary<string, string> KeyValues, string FilePath)
        {
            var codeStr = Generate(KeyValues);
            StringBuilder sb = new StringBuilder(FilePath);
            for (int i = 0; i < KeyWords.Length; i++)
            {
                sb.Replace(KeyWords[i].key, KeyValues[KeyWords[i].key]);
            }
            var filePath = sb.ToString();
            FileTools.WriteAllText(filePath, codeStr);
            //AssetDatabase.Refresh();
            //自行刷新
        }

        public static T LoadTemplateAsset<T>(string search)where T: TemplateAsset
        {
            var templatePath = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(search)[0]);
            var TemplateAsset = AssetDatabase.LoadAssetAtPath<T>(templatePath);
            return TemplateAsset;
        }
    }
}
