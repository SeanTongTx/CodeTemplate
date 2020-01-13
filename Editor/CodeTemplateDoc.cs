using EditorPlus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SeanLib.CodeTemplate
{

    [CustomSeanLibEditor("CodeTemplate",IsDoc =true)]
    public class CodeTemplateDoc : EditorMarkDownWindow
    {
        public override string RelativePath=>"../../Doc";
        public override bool EditScript => false;
    }
}