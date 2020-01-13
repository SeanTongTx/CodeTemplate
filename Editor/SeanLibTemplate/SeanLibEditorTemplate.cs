
using EditorPlus;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace SeanLib.CodeTemplate
{
    public partial class CustomMenuItems
    {
        private class CreateSeanLib : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var csAsset = AssetDBHelper.LoadAsset<FileTemplateAsset>("SeanLibEditorCsTemplate t:FileTemplateAsset");
                var uxmlAsset = AssetDBHelper.LoadAsset<FileTemplateAsset>("SeanLibEditorUXMLTemplate t:FileTemplateAsset");
                var usslAsset = AssetDBHelper.LoadAsset<FileTemplateAsset>("SeanLibEditorUssTemplate t:FileTemplateAsset");
                var dir = Path.GetDirectoryName(pathName);
                var ClassName = Path.GetFileNameWithoutExtension(pathName);
                var Values = new List<KeyWordValue>() {new KeyWordValue("#CLASS_NAME#", ClassName) };
                csAsset.Generate(Values, dir);
                uxmlAsset.Generate(Values, dir);
                usslAsset.Generate(Values, dir);
                AssetDatabase.Refresh();
            }
        }
        [MenuItem("Assets/Create/CodeTemplate/SeanLibEditor", priority = 100)]
        public static void CreateSeanLibEditor()
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, ScriptableObject.CreateInstance(typeof(CreateSeanLib)) as CreateSeanLib, "NewSeanLibEditor.cs", EditorGUIUtility.FindTexture("cs Script Icon"), string.Empty);
        }
        [MenuItem("Assets/Create/CodeTemplate/SeanLibDoc", priority = 100)]
        public static void CreateSeanLibDoc()
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, ScriptableObject.CreateInstance(typeof(EndNameActions.SetFileName2FirstKeyWord)) as EndNameActions.SetFileName2FirstKeyWord , "NewSeanLibDoc.cs", EditorGUIUtility.FindTexture("cs Script Icon"), "SeanLibDocCsTemplate t:FileTemplateAsset");
        }

        [MenuItem("Assets/Create/CodeTemplate/MDDoc", priority = 100)]
        public static void CreateMDAsset()
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, ScriptableObject.CreateInstance(typeof(EndNameActions.CopyFile)) as EndNameActions.CopyFile, "NewMDDoc.md", EditorGUIUtility.FindTexture("TextAsset Icon"), "TMP_ReadMe t:textasset");
        }
    }

}