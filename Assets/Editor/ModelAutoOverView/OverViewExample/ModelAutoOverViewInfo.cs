using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities.Editor;
using UnityEngine;

/// <summary>
/// 示例条目类
/// </summary>
public class ModelAutoOverViewInfo
{
    /// <summary>
    /// 显示在TreeView的条目名称
    /// </summary>
    public string Name;

    /// <summary>
    /// 分组
    /// </summary>
    public string Category;

    /// <summary>
    /// 描述
    /// </summary>
    public string Description;

    /// <summary>
    /// 图片
    /// </summary>
    public EditorIcon EditorIcon;

    public ModelAutoOverViewInfo(string name, string category, string description, EditorIcon editorIcon)
    {
        Name = name;
        Category = category;
        Description = description;
        EditorIcon = editorIcon;
    }

    public ModelAutoOverViewInfo()
    {
    }
}