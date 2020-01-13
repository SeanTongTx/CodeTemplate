
using EditorPlus;
using SeanLib.CodeTemplate;
using SeanLib.Core;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace SeanLib.CodeTemplate
{
    public class EndNameActions
    {

        public class CopyFile : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var template = AssetDBHelper.LoadAsset<UnityEngine.Object>(resourceFile);
                FileTools.Copy(PathTools.Asset2File(AssetDatabase.GetAssetPath(template)),pathName,true);
                AssetDatabase.Refresh();
            }
        }
        public class SetFileName2FirstKeyWord : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var template = Object.Instantiate(AssetDBHelper.LoadAsset<TemplateAsset>(resourceFile));
                var input = Path.GetFileNameWithoutExtension(pathName);
                string code;
                if (template.KeyWords.Length > 0)
                {
                    code = template.Generate(new List<KeyWordValue>(){ new KeyWordValue(template.KeyWords[0].key, input) });
                }
                else
                {
                    code = template.Template;
                }
                DestroyImmediate(template);
                FileTools.WriteAllText(pathName, code);
                AssetDatabase.Refresh();
            }
        }
        public class FileName : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var template = Object.Instantiate(AssetDBHelper.LoadAsset<TemplateAsset>(resourceFile));
                DestroyImmediate(template);
                FileTools.WriteAllText(pathName, template.Template);
                AssetDatabase.Refresh();
            }
        }
    }

    public partial class CustomMenuItems
    {
    }
}