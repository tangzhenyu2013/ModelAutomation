using Sirenix.Utilities.Editor;


/// <summary>
/// 驻扎皮肤
/// </summary>
public class Example_StationedSkin : AExample_Base
{
    private static readonly ModelAutoOverViewInfo ModelAutoOverViewInfo = new ModelAutoOverViewInfo(
        "驻扎皮肤", "模型编辑", "针对驻扎皮肤模型的编辑调整", EditorIcons.Car.Active);

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