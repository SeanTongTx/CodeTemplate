
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
namespace SeanLib.CodeTemplate
{
    public class SeanLibEditorTemplate
    {
        private class CreateSeanLib : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var csAsset = TemplateAsset.LoadTemplateAsset<FileTemplateAsset>("SeanLibEditorCsTemplate t:FileTemplateAsset");
                var uxmlAsset = TemplateAsset.LoadTemplateAsset<FileTemplateAsset>("SeanLibEditorUXMLTemplate t:FileTemplateAsset");
                var usslAsset = TemplateAsset.LoadTemplateAsset<FileTemplateAsset>("SeanLibEditorUssTemplate t:FileTemplateAsset");
                var dir = Path.GetDirectoryName(pathName);
                var ClassName = Path.GetFileNameWithoutExtension(pathName);
                Dictionary<string, string> Values = new Dictionary<string, string>() { { "#CLASS_NAME#", ClassName } };
                csAsset.Generate(Values, dir);
                uxmlAsset.Generate(Values, dir);
                usslAsset.Generate(Values, dir);
                AssetDatabase.Refresh();
            }
        }

        [MenuItem("Assets/Create/CodeTemplate/SeanLibEditor", priority = 100)]
        public static void CreateSeanLibEditor()
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, new CreateSeanLib(), "NewSeanLibEditor.cs", EditorGUIUtility.FindTexture("cs Script Icon"), string.Empty);
        }
    }
}