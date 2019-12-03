using EditorPlus;
using SeanLib.Core;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SeanLib.CodeTemplate
{
    public abstract class CodeGenerator : SeanLibEditor
    {
        public string Dir;
        public List<ITemplate> templates = new List<ITemplate>();
        public Dictionary<string, string> UserInputKV = new Dictionary<string, string>();
        public TextField Preview;
        protected override bool UseIMGUI => false;
        protected virtual bool DefaultLayout => true;
        public override void SetupUIElements()
        {
            if (DefaultLayout)
            {
                var editorContent = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(PathTools.RelativeAssetPath(typeof(CodeGenerator), "../CodeGenerator.uxml"));
                editorContent_styles = AssetDatabase.LoadAssetAtPath<StyleSheet>(PathTools.RelativeAssetPath(typeof(CodeGenerator), "../CodeGenerator.uss"));
                window.EditorContent.styleSheets.Add(editorContent_styles);
                editorContent.CloneTree(window.EditorContent);
                //SetupElements
                var SaveDirGUI = this.EditorContent_Elements.Q<IMGUIContainer>("savedir-view");
                SaveDirGUI.onGUIHandler = () =>
                {
                    Dir = OnGUIUtility.SaveFolderPanel("SaveDirectory");
                };
                var LegcyGUI = this.EditorContent_Elements.Q<IMGUIContainer>("GUIContainer");
                LegcyGUI.onGUIHandler = OnGUI;
                var genrateButton = this.EditorContent_Elements.Q<Button>("btn-generate");
                genrateButton.clickable.clicked += OnGenerate;

                Preview = this.EditorContent_Elements.Q<TextField>("Preview-text");

                foreach (var template in templates)
                {
                    InitTemplate(template);
                }
            }
            else
            {
                base.SetupUIElements();
            }
        }

        protected virtual void InitTemplate(ITemplate template)
        {
            foreach (var key in template.KeyWords)
            {
                UserInputKV[key.key] = string.Empty;
            }
        }
        public override void OnGUI()
        {
            base.OnGUI();
            foreach (var template in templates)
            {
                GUILayout.BeginVertical(SeanLibEditor.styles.Box);
                {
                    EditorGUILayout.LabelField(template.TemplateName, EditorStyles.boldLabel);
                    foreach (var keyword in template.KeyWords)
                    {
                        UserInputKV[keyword.key] = EditorGUILayout.TextField(keyword.comment, UserInputKV[keyword.key]);
                    }
                    PreviewButton(template);
                }
                GUILayout.EndVertical();
                EditorGUILayout.Space();
            }
        }
        protected void PreviewButton(ITemplate template)
        {
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Preview"))
                {
                    Preview.value = template.Generate(UserInputKV);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        public virtual void OnGenerate()
        {
            foreach (var item in UserInputKV)
            {
                if (item.Value.IsNullOrEmpty())
                {
                    throw new System.Exception("Values error");
                }
            }
            foreach (var template in templates)
            {
                template.Generate(UserInputKV, Dir);
            }
            AssetDatabase.Refresh();
        }

        #region User Input
        public virtual void SetInput(string key, string value)
        {
            UserInputKV[key] = value;
        }
        public virtual string GetInput(string key)
        {
            if (UserInputKV.TryGetValue(key, out string s))
            {
                return s;
            }
            return string.Empty;
        }
        #endregion
    }
}