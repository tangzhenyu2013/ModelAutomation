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
    public Texture Texture;

    public ModelAutoOverViewInfo(string name, string category, string description, Texture texture)
    {
        Name = name;
        Category = category;
        Description = description;
        Texture = texture;
    }

    public ModelAutoOverViewInfo()
    {
    }
}