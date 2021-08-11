using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Editor.ModelAutoOverView.OverViewExample
{
    public class ModelAutoOverViewUtilities
    {
        private static readonly Dictionary<Type, AExample_Base> AExampleBases = new Dictionary<Type, AExample_Base>();

        private static readonly CategoryComparer CategorySorter = new CategoryComparer();

        static ModelAutoOverViewUtilities()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(ModelAutoOverViewUtilities));
            Type[] types = assembly.GetTypes();

            foreach (var type in types)
            {
                object[] objects = type.GetCustomAttributes(typeof(ModelAutoAttribute), true);
                if (objects.Length == 0 || type.IsAbstract)
                {
                    continue;
                }

                AExample_Base temp = Activator.CreateInstance(type) as AExample_Base;
                AExampleBases.Add(type, temp);
            }
        }

        public static void BuildMenuTree(OdinMenuTree tree)
        {
            foreach (var aExampleBase in AExampleBases)
            {
                ModelAutoOverViewInfo trickOverViewInfo = aExampleBase.Value.GetTrickOverViewInfo();
                OdinMenuItem odinMenuItem = new OdinMenuItem(tree, trickOverViewInfo.Name, aExampleBase.Value)
                {
                    SearchString = trickOverViewInfo.Name + trickOverViewInfo.Description,
                    Icon = trickOverViewInfo.Texture
                };
                tree.AddMenuItemAtPath(trickOverViewInfo.Category, odinMenuItem);
            }
            tree.MenuItems.Sort(CategorySorter);
            tree.MarkDirty();
        }

        public static AExample_Base GetExampleByType(Type type)
        {
            AExample_Base aExampleBase;
            if (AExampleBases.TryGetValue(type, out aExampleBase))
            {
                return aExampleBase;
            }

            return null;
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
}