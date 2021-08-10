using UnityEngine;

namespace Editor.ModelAutoOverView.OverViewExample
{
    /// <summary>
    /// Example的基类
    /// </summary>
    [ModelAuto]
    public abstract class AExample_Base
    {
        public abstract ModelAutoOverViewInfo GetTrickOverViewInfo();

        public abstract void Init();

        public abstract void DrawUI(Rect rect);

        public abstract void Destroy();
    }
}