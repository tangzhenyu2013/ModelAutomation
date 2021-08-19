using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// 航母
/// </summary>
public class Example_CV : AExample_Base
{
    private static readonly ModelAutoOverViewInfo ModelAutoOverViewInfo = new ModelAutoOverViewInfo(
        "航母", "模型编辑", "针对航母模型的编辑调整", Resources.Load<Texture>("Textures/HeadIcons/HeadImage01"));

    private static string assetPath = "Assets/Resources/Prefabs/Animation/3DAircraftCarrier";

    /// <summary>
    /// 模型Editor
    /// </summary>
    // private Editor modelEditor;
    private EditorWindow _editorWindow;

    /// <summary>
    /// 模型路径
    /// </summary>
    private List<string> path = new List<string>();

    /// <summary>
    /// 模型名称
    /// </summary>
    private List<string> name = new List<string>();

    /// <summary>
    /// 有问题的模型集合
    /// </summary>
    private Dictionary<string, ModelImporter> modelImporters = new Dictionary<string, ModelImporter>();

    private List<Material> materials = new List<Material>();

    /// <summary>
    /// 是否显示所有的模型
    /// </summary>
    [LabelText("所有模型/问题模型")] [ShowInInspector] [LabelWidth(150f)] [HorizontalGroup("0")]
    private bool isShowAllModel = true;


    public override ModelAutoOverViewInfo GetTrickOverViewInfo()
    {
        return ModelAutoOverViewInfo;
    }

    public override void Init()
    {
        string[] allGuids = AssetDatabase.FindAssets("t:Model",
            new[] {assetPath});

        for (int i = 0; i < allGuids.Length; i++)
        {
            path.Add(AssetDatabase.GUIDToAssetPath(allGuids[i]));
            name.Add(Path.GetFileName(path[i]));
        }
    }

    public override void DrawUI()
    {
        for (int i = 0; i < path.Count; i++)
        {
            ModelImporter modelImporter = AssetImporter.GetAtPath(path[i]) as ModelImporter;

            if (null == modelImporter) continue;

            if (!isShowAllModel && modelImporter.materialLocation == ModelImporterMaterialLocation.External)
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
                if (null == _editorWindow)
                {
                    _editorWindow = DockUtilities.GetInspectTarget(model);
                    ModelAutoOverViewEditorWindow.Instance.DockWindow(_editorWindow, DockUtilities.DockPosition.Right);
                }
                else
                {
                    _editorWindow.SetInspectTarget(model);
                }
            }
        }
    }

    [Button("一键设置")]
    [HorizontalGroup("0", 100f, 0, 500, 1)]
    public void SetModel()
    {
        List<string> modelName = new List<string>();
        foreach (var modelImporter in modelImporters)
        {
            modelImporter.Value.materialLocation = ModelImporterMaterialLocation.External;
            EditorUtility.SetDirty(modelImporter.Value);
            AssetDatabase.ImportAsset(modelImporter.Key);
            modelName.Add(Path.GetFileName(modelImporter.Key).Replace(".fbx", ""));
        }

        AssetDatabase.Refresh();

        string[] allGuids = AssetDatabase.FindAssets("t:Material",
            new[] {assetPath});

        for (int i = 0; i < allGuids.Length; i++)
        {
            string tempPath = AssetDatabase.GUIDToAssetPath(allGuids[i]);
            Material material = AssetDatabase.LoadAssetAtPath<Material>(tempPath);
            string str = modelName.Find(x => x.Contains(Path.GetFileName(tempPath).Replace(".mat", "")));
            if (null == material || string.IsNullOrEmpty(str)) continue;
            material.shader = Shader.Find("Legacy Shaders/Transparent/Cutout/Soft Edge Unlit");
            material.color = Color.white;
            AssetDatabase.ImportAsset(tempPath);
        }
    }

    public override void Destroy()
    {
        path.Clear();
        name.Clear();
        modelImporters.Clear();
        materials.Clear();
        if (_editorWindow != null)
        {
            _editorWindow.Close();
            _editorWindow = null;
        }
    }
}