using Editor.ModelAutoOverView.OverViewExample;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace Editor.ModelAutoOverView.Examples
{
	/// <summary>
	/// 光环
	/// </summary>
	public class Example_Halo : AExample_Base {
		private static readonly ModelAutoOverViewInfo ModelAutoOverViewInfo = new ModelAutoOverViewInfo(
			"光环", "模型编辑", "针对光环模型的编辑调整",EditorIcons.Car.Active);
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
}
