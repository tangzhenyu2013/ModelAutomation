using Editor.ModelAutoOverView.OverViewExample;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor.ModelAutoOverView.Examples
{
    /// <summary>
    /// 航母
    /// </summary>
    public class Example_CV : AExample_Base
    {
        private static readonly ModelAutoOverViewInfo ModelAutoOverViewInfo = new ModelAutoOverViewInfo(
            "航母", "模型编辑", "针对航母模型的编辑调整",EditorIcons.Car.Active);

        public UnityEditor.Editor modelEditor;

        public override ModelAutoOverViewInfo GetTrickOverViewInfo()
        {
            return ModelAutoOverViewInfo;
        }

        public override void Init()
        {
            Object model =
                AssetDatabase.LoadAssetAtPath<Object>(
                    "Assets/Resources/Prefabs/Animation/3DAircraftCarrier/mx_Zhengfu/mx_Zhengfu.fbx");
            modelEditor = UnityEditor.Editor.CreateEditor(model);
        }

        public override void DrawUI(Rect rect)
        {
        }

        public override void Destroy()
        {
        }
    }
}