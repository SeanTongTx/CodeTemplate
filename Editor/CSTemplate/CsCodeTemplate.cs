using SeanLib.Core;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
namespace SeanLib.CodeTemplate
{
    [CreateAssetMenu(fileName = "Template", menuName = "CodeTemplate/New C# Template", order = 1000)]
    public class CsCodeTemplate : TemplateAsset
    {
        private class ClassEndNameAction : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
            }
        }
        private class InterfaceEndNameAction : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var csTemplate = AssetDatabase.LoadAssetAtPath<CsCodeTemplate>(resourceFile);
                var ClassName = Path.GetFileNameWithoutExtension(pathName);
                var code = csTemplate.Generate(new Dictionary<string, string>() { { "#CS_INTERFACE_NAME#", ClassName } });
                FileTools.WriteAllText(pathName, code);
                AssetDatabase.Refresh();
            }
        }

        [MenuItem("Assets/Create/CodeTemplate/C# Class", priority = 0)]
        public static void CreateClass()
        {
            var templatePath= AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("CsClass t:CsCodeTemplate")[0]);

            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, new ClassEndNameAction(), "NewClass.cs", EditorGUIUtility.FindTexture("cs Script Icon"), templatePath);
        }
        [MenuItem("Assets/Create/CodeTemplate/C# Interface", priority = 0)]
        public static void CreateInterface()
        {
            var templatePath = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("CsInterface t:CsCodeTemplate")[0]);

            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, new InterfaceEndNameAction(), "NewInterface.cs", EditorGUIUtility.FindTexture("cs Script Icon"), templatePath);
        }
    }
}