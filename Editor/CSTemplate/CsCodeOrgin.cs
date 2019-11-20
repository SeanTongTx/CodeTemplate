using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class CsCodeOrgin
{
    private class EndNameAction : EndNameEditAction
    {
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            Debug.Log(instanceId+":"+ pathName+":"+ resourceFile);
        }
    }
    [MenuItem("Assets/Create/CodeTemplate/C# Class",priority =0)]
    public static void Create()
    {  
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,new EndNameAction(), "Code.cs", EditorGUIUtility.FindTexture("cs Script Icon"), "");
    }
}
