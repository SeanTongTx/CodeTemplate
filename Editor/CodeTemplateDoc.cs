using EditorPlus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SeanLib.CodeTemplate
{

    [CustomSeanLibEditor("CodeTemplate")]
    public class CodeTemplateDoc : EditorMarkDownWindow
    {
        public override string RelativePath => "../../Doc";
    }
}