using System;

/// <summary>
/// 使用此特性标记的类会被收集到TreeView
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ModelAutoAttribute : Attribute
{
}