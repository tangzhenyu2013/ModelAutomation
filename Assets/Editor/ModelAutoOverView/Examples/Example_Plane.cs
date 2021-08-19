using UnityEngine;

/// <summary>
/// 飞机
/// </summary>
public class Example_Plane : AExample_Base
{
    private static readonly ModelAutoOverViewInfo ModelAutoOverViewInfo = new ModelAutoOverViewInfo(
        "飞机", "模型编辑", "针对飞机模型的编辑调整", Resources.Load<Texture>("Textures/Planes/Zhandouji/ZD_CT_A6M_Zero"));

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