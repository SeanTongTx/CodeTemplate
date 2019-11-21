using SeanLib.Core;
using System.Collections.Generic;
using System.Text;
namespace SeanLib.CodeTemplate
{
    public abstract class CodeTemplate:ITemplate
    {
        public string TemplateName => templateName;

        public CodeTemplate()
        {

        }
        public CodeTemplate(string TemplateName)
        {
            this.templateName = TemplateName;
        }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string templateName;
        /// <summary>
        /// 文件生成路径(子路径)
        /// </summary>
        public abstract string FilePath { get; }
        /// <summary>
        /// 模板本体
        /// </summary>
        public abstract string Template { get; }
        /// <summary>
        /// 模板中的关键字
        /// </summary>
        public abstract KeyWord[] KeyWords { get; }

        /// <summary>
        /// 生成字符
        /// </summary>
        /// <param name="KeyValues"></param>
        /// <returns></returns>
        public virtual string Generate(string template, Dictionary<string, string> KeyValues)
        {
            StringBuilder sb = new StringBuilder(template);
            for (int i = 0; i < KeyWords.Length; i++)
            {
                if (KeyValues.ContainsKey(KeyWords[i].key))
                    sb.Replace(KeyWords[i].key, KeyValues[KeyWords[i].key]);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 生成字符
        /// </summary>
        /// <param name="KeyValues"></param>
        /// <returns></returns>
        public virtual string Generate(Dictionary<string, string> KeyValues)
        {
            return Generate(this.Template, KeyValues);
        }
        /// <summary>
        /// 生成文件
        /// </summary>
        /// <param name="KeyValues"></param>
        /// <param name="DirRoot"></param>
        public virtual void GenerateFile(Dictionary<string, string> KeyValues, string DirRoot)
        {
            var codeStr = Generate(KeyValues);
            StringBuilder sb = new StringBuilder(FilePath);
            for (int i = 0; i < KeyWords.Length; i++)
            {
                sb.Replace(KeyWords[i].key, KeyValues[KeyWords[i].key]);
            }
            var filePath = sb.ToString();
            FileTools.WriteAllText(DirRoot + "/" + filePath, codeStr);
        }

        public void Generate(Dictionary<string, string> KeyValues, string FilePath)
        {
            GenerateFile(KeyValues, FilePath);
        }
    }
}