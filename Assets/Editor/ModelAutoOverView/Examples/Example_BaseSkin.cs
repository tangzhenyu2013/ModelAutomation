using UnityEngine;

/// <summary>
/// 基地皮肤
/// </summary>
public class Example_BaseSkin : AExample_Base
{
    private static readonly ModelAutoOverViewInfo ModelAutoOverViewInfo = new ModelAutoOverViewInfo(
        "基地皮肤", "模型编辑", "针对基地模型的编辑调整", Resources.Load<Texture>("Textures/CVBaseSkin/HZ_bao01"));

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