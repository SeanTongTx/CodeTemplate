using System.Collections.Generic;
using System.Text;

namespace SeanLib.CodeTemplate
{
    public class Generater
    {
        /// <summary>
        /// 通过Keyword数据类型控制替换项
        /// </summary>
        /// <param name="template"></param>
        /// <param name="KeyWords"></param>
        /// <param name="Values"></param>
        /// <returns></returns>
        public static string Generate(string template, KeyWord[] KeyWords, List<KeyWordValue> Values)
        {
            StringBuilder sb = new StringBuilder(template);
            for (int i = 0; i < KeyWords.Length; i++)
            {
                var value = Values.Find(e => e.key == KeyWords[i].key);
                if (value!=null)
                    sb.Replace(value.key, value.value);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 替换模板中所有特殊字符
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="Values">替换字符列表</param>
        /// <returns></returns>
        public static string Generate(string template, List<KeyWordValue> Values)
        {
            StringBuilder sb = new StringBuilder(template);
            var enumerator = Values.GetEnumerator();
            while(enumerator.MoveNext())
            {
                var kv = enumerator.Current;
                sb.Replace(kv.key, kv.value);
            }
            return sb.ToString();
        }
    }
}