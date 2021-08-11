using System;
using System.Collections.Generic;
using System.IO;
using Editor.ModelAutoOverView.OverViewExample;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editor.ModelAutoOverView.Examples
{
    /// <summary>
    /// 航母
    /// </summary>
    public class Example_CV : AExample_Base
    {
        private static readonly ModelAutoOverViewInfo ModelAutoOverViewInfo = new ModelAutoOverViewInfo(
            "航母", "模型编辑", "针对航母模型的编辑调整", EditorIcons.Car.Active);

        private static string assetPath = "Assets/Resources/Prefabs/Animation/3DAircraftCarrier";

        /// <summary>
        /// 模型Editor
        /// </summary>
        private UnityEditor.Editor modelEditor;

        /// <summary>
        /// 模型路径
        /// </summary>
        private List<string> path;

        /// <summary>
        /// 模型名称
        /// </summary>
        private List<string> name;

        /// <summary>
        /// 有问题的模型集合
        /// </summary>
        private Dictionary<string, ModelImporter> modelImporters;

        private List<Material> materials;

        /// <summary>
        /// 是否显示所有的模型
        /// </summary>
        [LabelText("所有模型/问题模型")] [ShowInInspector] [LabelWidth(150f)] [HorizontalGroup("0")]
        private bool isShowAllModel;

        [Button("一键设置")]
        [HorizontalGroup("0", 100f, 0, 500, 1)]
        public void SetModel()
        {
            List<string> materialPaths = new List<string>();
            string tempPath;
            foreach (var modelImporter in modelImporters)
            {
                modelImporter.Value.materialLocation = ModelImporterMaterialLocation.External;
                EditorUtility.SetDirty(modelImporter.Value);
                AssetDatabase.ImportAsset(modelImporter.Key);
                tempPath = Path.GetDirectoryName(modelImporter.Key);
                if (!materialPaths.Contains(tempPath))
                {
                    materialPaths.Add(tempPath);
                }
            }

            AssetDatabase.Refresh();

            string[] allGuids = AssetDatabase.FindAssets("t:Material",
                new[] {assetPath});

            for (int i = 0; i < materialPaths.Count; i++)
            {
                tempPath = AssetDatabase.GUIDToAssetPath(allGuids[i]);
                if (materialPaths.Contains(tempPath))
                {
                    Debug.Log(tempPath);
                }
            }
        }

        public override ModelAutoOverViewInfo GetTrickOverViewInfo()
        {
            return ModelAutoOverViewInfo;
        }

        public override void Init()
        {
            InitList();

            string[] allGuids = AssetDatabase.FindAssets("t:Model",
                new[] {assetPath});

            for (int i = 0; i < allGuids.Length; i++)
            {
                path.Add(AssetDatabase.GUIDToAssetPath(allGuids[i]));
                name.Add(Path.GetFileName(path[i]));
            }
        }

        private void InitList()
        {
            if (null == path) path = new List<string>();
            if (null == name) name = new List<string>();
            if (null == modelImporters) modelImporters = new Dictionary<string, ModelImporter>();
            if (null == materials) materials = new List<Material>();
        }

        public override void DrawUI()
        {
            for (int i = 0; i < path.Count; i++)
            {
                ModelImporter modelImporter = AssetImporter.GetAtPath(path[i]) as ModelImporter;

                if (null == modelImporter) continue;

                if (isShowAllModel && modelImporter.materialLocation == ModelImporterMaterialLocation.External)
                {
                    continue;
                }

                if (!modelImporters.ContainsKey(path[i]) &&
                    modelImporter.materialLocation == ModelImporterMaterialLocation.InPrefab)
                {
                    modelImporters.Add(path[i], modelImporter);
                }

                if (GUILayout.Button(name[i], GUILayout.Width(200)))
                {
                    Object model = AssetDatabase.LoadAssetAtPath<Object>(path[i]);
                    modelEditor = UnityEditor.Editor.CreateEditor(model);
                }
            }

            if (null == modelEditor) return;
            modelEditor.DrawPreview(new Rect(470, 0, 200, 200));
        }

        public override void Destroy()
        {
            path = null;
            modelEditor = null;
            name = null;
            modelImporters = null;
            materials = null;
        }
    }
}