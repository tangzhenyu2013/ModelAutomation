using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

public class ModelAutoOverViewEditorWindow : OdinMenuEditorWindow 
{
    [MenuItem("Tools/模型编辑窗口")]
    private static void OpenWindow()
    {
        bool flag = Resources.FindObjectsOfTypeAll<AttributesExampleWindow>().Length == 0;
        var window = GetWindow<ModelAutoOverViewEditorWindow>();
        if (flag)
        {
            window.MenuWidth = 250f;
            window.position = GUIHelper.GetEditorWindowRect().AlignCenterXY(850f, 700f);
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
        
    }
}
