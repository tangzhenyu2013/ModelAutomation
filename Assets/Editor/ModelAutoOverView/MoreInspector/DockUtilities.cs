using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public static class DockUtilities
{
    public enum DockPosition
    {
        Left,
        Top,
        Right,
        Bottom
    }

    private static Vector2 GetFakeMousePosition(EditorWindow wnd, DockPosition position)
    {
        Vector2 mousePosition = Vector2.zero;

        // The 20 is required to make the docking work.
        // Smaller values might not work when faking the mouse position.
        switch (position)
        {
            case DockPosition.Left:
                mousePosition = new Vector2(20, wnd.position.size.y / 2);
                break;
            case DockPosition.Top:
                mousePosition = new Vector2(wnd.position.size.x / 2, 20);
                break;
            case DockPosition.Right:
                mousePosition = new Vector2(wnd.position.size.x - 20, wnd.position.size.y / 2);
                break;
            case DockPosition.Bottom:
                mousePosition = new Vector2(wnd.position.size.x / 2, wnd.position.size.y - 20);
                break;
        }

        return new Vector2(wnd.position.x + mousePosition.x, wnd.position.y + mousePosition.y);
    }

    /// <summary>
    /// Docks the "docked" window to the "anchor" window at the given position
    /// </summary>
    public static void DockWindow(this EditorWindow anchor, EditorWindow docked, DockPosition position)
    {
        var anchorParent = GetParentOf(anchor);

        SetDragSource(anchorParent, GetParentOf(docked));
        PerformDrop(GetWindowOf(anchorParent), docked, GetFakeMousePosition(anchor, position));
    }

    static object GetParentOf(object target)
    {
        var field = target.GetType().GetField("m_Parent", BindingFlags.Instance | BindingFlags.NonPublic);
        return field.GetValue(target);
    }

    static object GetWindowOf(object target)
    {
        var property = target.GetType().GetProperty("window", BindingFlags.Instance | BindingFlags.Public);
        return property.GetValue(target, null);
    }

    static void SetDragSource(object target, object source)
    {
        var field = target.GetType().GetField("s_OriginalDragSource", BindingFlags.Static | BindingFlags.NonPublic);
        if (field != null) field.SetValue(null, source);
    }

    static void PerformDrop(object window, EditorWindow child, Vector2 screenPoint)
    {
        var rootSplitViewProperty =
            window.GetType().GetProperty("rootSplitView", BindingFlags.Instance | BindingFlags.Public);
        if (rootSplitViewProperty != null)
        {
            object rootSplitView = rootSplitViewProperty.GetValue(window, null);

            var dragMethod = rootSplitView.GetType().GetMethod("DragOver", BindingFlags.Instance | BindingFlags.Public);
            var dropMethod = rootSplitView.GetType()
                .GetMethod("PerformDrop", BindingFlags.Instance | BindingFlags.Public);

            if (dragMethod != null)
            {
                var dropInfo = dragMethod.Invoke(rootSplitView, new object[] {child, screenPoint});
                if (dropMethod != null && dropInfo != null)
                    dropMethod.Invoke(rootSplitView, new object[] {child, dropInfo, screenPoint});
            }
        }
    }

    /// <summary>
    /// 显示额外Inspector面板
    /// </summary>
    /// <param name="targetGO"></param>
    /// <returns></returns>
    public static EditorWindow GetInspectTarget(Object targetGO)
    {
        Type inspectorType = typeof(Editor).Assembly.GetType("UnityEditor.InspectorWindow");
        EditorWindow inspectorInstance = ScriptableObject.CreateInstance(inspectorType) as EditorWindow;
        Object prevSelection = Selection.activeObject;
        Selection.activeObject = targetGO;
        var isLocked = inspectorType.GetProperty("isLocked", BindingFlags.Instance | BindingFlags.Public);
        if (isLocked != null) isLocked.GetSetMethod().Invoke(inspectorInstance, new object[] {true});
        Selection.activeObject = prevSelection;
        if (inspectorInstance != null)
        {
            inspectorInstance.Show();
        }

        return inspectorInstance;
    }

    public static void SetInspectTarget(this EditorWindow editorWindow, Object targetGO)
    {
        Type inspectorType = typeof(Editor).Assembly.GetType("UnityEditor.InspectorWindow");
        Object prevSelection = Selection.activeObject;
        var isLocked = inspectorType.GetProperty("isLocked", BindingFlags.Instance | BindingFlags.Public);
        if (isLocked != null) isLocked.GetSetMethod().Invoke(editorWindow, new object[] {false});
        Selection.activeObject = targetGO;
        if (isLocked != null) isLocked.GetSetMethod().Invoke(editorWindow, new object[] {true});
        Selection.activeObject = prevSelection;
    }
}