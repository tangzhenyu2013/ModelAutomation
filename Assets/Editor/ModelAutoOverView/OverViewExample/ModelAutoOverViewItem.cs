using Sirenix.OdinInspector;
using UnityEngine;

namespace Editor.ModelAutoOverView.OverViewExample
{
	public class ModelAutoOverViewItem  
	{
		private AExample_Base m_Example;
        
		public AExample_Base GetExample()
		{
			return m_Example;
		}
	
		public ModelAutoOverViewItem(AExample_Base aExampleBase)
		{
			if (aExampleBase == null)
			{
				Debug.LogError("AExampleBase数据为空，请检查类型");
				return;
			}
			m_Example = aExampleBase;
		}

		[OnInspectorGUI]
		public void Draw()
		{
		}
	}
}
