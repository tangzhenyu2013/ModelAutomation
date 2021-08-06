using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.Examples;
using UnityEngine;

public class ModelAutoOverViewUtilities
{
    private static readonly Dictionary<Type, AExample_Base> AExampleBases = new Dictionary<Type, AExample_Base>();
    private static readonly CategoryComparer CategorySorter = new CategoryComparer();

    static ModelAutoOverViewUtilities()
    {
        AExampleBases.Add(typeof(Example_CV),new Example_CV());
    }

    public static void BuildMenuTree(OdinMenuTree tree)
    {
        foreach (var aExampleBase in AExampleBases)
        {
            ModelAutoOverViewInfo trickOverViewInfo = aExampleBase.Value.GetTrickOverViewInfo();
            OdinMenuItem odinMenuItem = new OdinMenuItem(tree, trickOverViewInfo.Name, aExampleBase.Key)
            {
                Value = aExampleBase.Key,
                SearchString = trickOverViewInfo.Name + trickOverViewInfo.Description
            };
            tree.Add(trickOverViewInfo.Category, odinMenuItem, trickOverViewInfo.EditorIcon);
        }

        tree.MenuItems.Sort(CategorySorter);
        tree.MarkDirty();
    }

    private class CategoryComparer : IComparer<OdinMenuItem>
    {
        private static readonly Dictionary<string, int> Order = new Dictionary<string, int>()
        {
            {
                "Essentials",
                -10
            },
            {
                "Misc",
                8
            },
            {
                "Meta",
                9
            },
            {
                "Unity",
                10
            },
            {
                "Debug",
                50
            }
        };

        public int Compare(OdinMenuItem x, OdinMenuItem y)
        {
            int num1;
            if (!Order.TryGetValue(x.Name, out num1))
                num1 = 0;
            int num2;
            if (!Order.TryGetValue(y.Name, out num2))
                num2 = 0;
            return num1 == num2 ? x.Name.CompareTo(y.Name) : num1.CompareTo(num2);
        }
    }
}