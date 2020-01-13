using EditorPlus;
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
    }
    public partial class CustomMenuItems
    {
        [MenuItem("Assets/Create/CodeTemplate/Unity/ScriptableObject", priority = 0)]
        public static void CreateScriptableObject()
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, new EndNameActions.SetFileName2FirstKeyWord(), "NewScriptableObject.cs", EditorGUIUtility.FindTexture("cs Script Icon"), "ScriptableObject t:UnityCodeTemplate");
        }
        [MenuItem("Assets/Create/CodeTemplate/Unity/CustomEditor", priority = 0)]
        public static void CreateCustomEditor()
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, new EndNameActions.SetFileName2FirstKeyWord(), "NewCustomEditor.cs", EditorGUIUtility.FindTexture("cs Script Icon"), "CustomEditor t:UnityCodeTemplate");
        }
        [MenuItem("Assets/Create/CodeTemplate/Unity/pacakgeConfig", priority = 0)]
        public static void CreatePackageJson()
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, new EndNameActions.FileName(), "package.json", EditorGUIUtility.FindTexture("TextAsset Icon"), "package t: templateasset");
        }
    }
}