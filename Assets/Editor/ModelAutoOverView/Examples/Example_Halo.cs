using UnityEngine;

/// <summary>
/// 光环
/// </summary>
public class Example_Halo : AExample_Base
{
    private static readonly ModelAutoOverViewInfo ModelAutoOverViewInfo = new ModelAutoOverViewInfo(
        "光环", "模型编辑", "针对光环模型的编辑调整", Resources.Load<Texture>("Textures/Item/ICON_guanghuan3_yellowBase"));

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