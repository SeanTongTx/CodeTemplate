using SeanLib.Core;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
namespace SeanLib.CodeTemplate
{
    //[CreateAssetMenu(fileName = "Template", menuName = "CodeTemplate/New Unity Template", order = 1000)]
    public class UnityCodeTemplate : TemplateAsset
    {
        private class ClassEndNameAction : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var csTemplate = AssetDatabase.LoadAssetAtPath<UnityCodeTemplate>(resourceFile);
                var ClassName = Path.GetFileNameWithoutExtension(pathName);
                var code = csTemplate.Generate(new Dictionary<string, string>() { { "#CS_CLASS_NAME#", ClassName } });
                FileTools.WriteAllText(pathName, code);
                AssetDatabase.Refresh();
            }
        }
        [MenuItem("Assets/Create/CodeTemplate/ScriptableObject", priority = 0)]
        public static void CreateInterface()
        {
            var templatePath = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("ScriptableObject t:UnityCodeTemplate")[0]);

            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, new ClassEndNameAction(), "NewScriptableObject.cs", EditorGUIUtility.FindTexture("cs Script Icon"), templatePath);
        }
    }
}