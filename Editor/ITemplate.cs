using SeanLib.Core;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace SeanLib.CodeTemplate
{

    public interface ITemplate
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        string TemplateName { get; }
        /// <summary>
        /// 模板内容
        /// </summary>
        string Template { get; }
        /// <summary>
        /// 关键字
        /// </summary>
        KeyWord[] KeyWords { get; }
        /// <summary>
        /// 生成字符
        /// </summary>
        /// <param name="KeyValues">外部输入数据</param>
        /// <returns></returns>
        string Generate(List<KeyWordValue>  KeyValues);

        /// <summary>
        /// 生成文件
        /// </summary>
        /// <param name="KeyValues"></param>
        /// <param name="FilePath"></param>
        void Generate(List<KeyWordValue> KeyValues, string FilePath);


        /// <summary>
        /// 最终生成时的数据
        /// </summary>
        List<KeyWordValue> Values { get; }
        string GetValue(string key);
        void SetValue(string key, string value);
    }
    [Serializable]
    public class KeyWord
    {
        [Flags]
        public enum Flag
        {
            /// <summary>
            /// 不在界面中显示
            /// </summary>
            HideInInspector = 1 << 1,
            /// <summary>
            /// 公共的 用来控制不同模板中相同的Key
            /// </summary>
            Public = 1 << 2,
            /// <summary>
            /// 必须的 如果没有设置 将无法生成
            /// </summary>
            Required = 1 << 3,
        }
        public string key;
        public string comment;
        /// <summary>
        /// 检查标签
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool _(Flag flag)
        {
            return (Flags & flag)>0;

        }
        public Flag Flags;
        public KeyWord Clone()
        {
           return this.MemberwiseClone() as KeyWord;
        }
    }
    /// <summary>
    /// this is for unity serialize
    /// </summary>
    [Serializable]
    public class KeyWordValue
    {
        public string key;
        public string value;
        public KeyWordValue() { }
        public KeyWordValue(string k,string v)
        {
            this.key = k;
            this.value = v;
        }
    }

    [Serializable]
    public class KeyWordDic
    {
        /// <summary>
        /// 支持unity序列化
        /// </summary>
        [SerializeField]
        protected List<KeyWordValue> values = new List<KeyWordValue>();
        private Dictionary<string, int> caches = new Dictionary<string, int>();
        public List<KeyWordValue> Values => values;
        private void CheckInit()
        {
            if (values.Count != caches.Count)
            {
                caches.Clear();
                for (int i = 0; i < values.Count; i++)
                {
                    caches[values[i].key] = i;
                }
            }
        }
        public void SetValue(string key, string value)
        {
            CheckInit();
            int index = 0;
            if (caches.TryGetValue(key, out index))
            {
                values[index].value = value;
            }
            else
            {
                index = values.Count;
                caches[key] = index;
                values.Add(new KeyWordValue(key, value));
            }
        }
        public string GetValue(string key)
        {
            CheckInit();
            int index = 0;
            if (caches.TryGetValue(key, out index))
            {
                return values[index].value;
            }
            return string.Empty;
        }
    }

}