using System;
using Editor.ModelAutoOverView.Examples;
using Editor.ModelAutoOverView.OverViewExample;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor.ModelAutoOverView
{
    public class ModelAutoOverViewEditorWindow : OdinMenuEditorWindow
    {
        private AExample_Base aExampleBase;

        [MenuItem("Tools/模型编辑窗口")]
        private static void OpenWindow()
        {
            bool flag = Resources.FindObjectsOfTypeAll<AttributesExampleWindow>().Length == 0;
            var window = GetWindow<ModelAutoOverViewEditorWindow>();
            if (flag)
            {
                window.MenuWidth = 250f;
                window.position = GUIHelper.GetEditorWindowRect().AlignCenterXY(1000, 800f);
            }
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree odinMenuTre = new OdinMenuTree();
            odinMenuTre.Selection.SupportsMultiSelect = false;
            odinMenuTre.Selection.SelectionChanged += SelectionChanged;
            odinMenuTre.Config.DrawSearchToolbar = true;
            odinMenuTre.Config.DefaultMenuStyle.Height = 22;
            ModelAutoOverViewUtilities.BuildMenuTree(odinMenuTre);
            return odinMenuTre;
        }

        private void SelectionChanged(SelectionChangedType selectionChangedType)
        {
            if (aExampleBase != null)
            {
                aExampleBase.Destroy();
            }

            aExampleBase = (AExample_Base) MenuTree.Selection.SelectedValue;

            if (aExampleBase != null) aExampleBase.Init();
        }

        protected override void DrawEditors()
        {
            base.DrawEditors();
            if (aExampleBase != null)
                aExampleBase.DrawUI();
        }

        protected override void DrawMenu()
        {
            base.DrawMenu();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (aExampleBase != null)
                aExampleBase.Destroy();
            aExampleBase = null;
        }
    }
}