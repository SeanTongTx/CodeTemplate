using System;
using System.Collections.Generic;

namespace SeanLib.CodeTemplate
{
    public interface ITemplate
    {
        string TemplateName { get; }
        string Template { get; }

        KeyWord[] KeyWords { get; }

        string Generate(Dictionary<string, string> KeyValues);

        void Generate(Dictionary<string, string> KeyValues,string FilePath);
    }
    [Serializable]
    public struct KeyWord
    {
        public string key;
        public string comment;
    }

}