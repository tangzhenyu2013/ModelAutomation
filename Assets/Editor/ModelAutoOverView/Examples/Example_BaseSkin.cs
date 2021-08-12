﻿using Sirenix.Utilities.Editor;

/// <summary>
/// 基地皮肤
/// </summary>
public class Example_BaseSkin : AExample_Base
{
    private static readonly ModelAutoOverViewInfo ModelAutoOverViewInfo = new ModelAutoOverViewInfo(
        "基地皮肤", "模型编辑", "针对基地模型的编辑调整", EditorIcons.Car.Active);

    public override ModelAutoOverViewInfo GetTrickOverViewInfo()
    {
        return ModelAutoOverViewInfo;
    }

    public override void Init()
    {
    }

    public override void DrawUI()
    {
    }

    public override void Destroy()
    {
    }
}