
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SeanLib.CodeTemplate
{
    [CreateAssetMenu(fileName = "GeneralFile", menuName = "CodeTemplate/New GeneralFile Template", order = 1000)]
    public class FileTemplateAsset:TemplateAsset
    {
        [SerializeField]
        private string FileNameTemplate;
        public override void Generate(Dictionary<string, string> KeyValues, string FilePath)
        {
            FilePath = Path.Combine(FilePath, FileNameTemplate);
            base.Generate(KeyValues, FilePath);
        }
    }
}