using SeanLib.Core;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace SeanLib.CodeTemplate
{
    //[CreateAssetMenu(fileName = "Template", menuName = "CodeTemplate/New C# Template", order = 1000)]
    public class CsCodeTemplate : TemplateAsset
    {
    }

    public partial class CustomMenuItems
    {
        [MenuItem("Assets/Create/CodeTemplate/C# Script/Class", priority = 0)]
        public static void CreateClass()
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, new EndNameActions.SetFileName2FirstKeyWord(), "NewClass.cs", EditorGUIUtility.FindTexture("cs Script Icon"), "CsClass t:CsCodeTemplate");
        }
        [MenuItem("Assets/Create/CodeTemplate/C# Script/Interface", priority = 0)]
        public static void CreateInterface()
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, new EndNameActions.SetFileName2FirstKeyWord(), "NewInterface.cs", EditorGUIUtility.FindTexture("cs Script Icon"), "CsInterface t:CsCodeTemplate");
        }
        [MenuItem("Assets/Create/CodeTemplate/C# Script/Struct", priority = 0)]
        public static void CreateStruct()
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, new EndNameActions.SetFileName2FirstKeyWord(), "NewStruct.cs", EditorGUIUtility.FindTexture("cs Script Icon"), "CsSturct t:CsCodeTemplate");
        }
    }

}