// using Sirenix.OdinInspector;
// using Sirenix.OdinInspector.Editor;
// using Sirenix.Utilities;
// using Sirenix.Utilities.Editor;
// using System;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEngine;
//
// public class GetItemEditorWindowNew : OdinMenuEditorWindow
// {
//     private List<GetItemEditorData> _tempList = new List<GetItemEditorData>();
//     private Dictionary<ItemType, ItemData> _dicBaseData = new Dictionary<ItemType, ItemData>();
//     private ItemData datas = new ItemData();
//     [MenuItem("TEST/获取道具 = GM命令getitem")]
//     private static void OpenWindow()
//     {
//         var window = GetWindow<GetItemEditorWindowNew>();
//         window.Init();
//         window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1024, 768);
//         window.titleContent = new GUIContent("获取道具");
//     }
//
//     protected override OdinMenuTree BuildMenuTree()
//     {
//         OdinMenuTree odinMenuTree = new OdinMenuTree(true);
//         odinMenuTree.DefaultMenuStyle.SetIconSize(28f);
//         odinMenuTree.Config.DrawSearchToolbar = true;
//         RunGameInit();
//         odinMenuTree.Add("全部道具", datas, Resources.Load("Textures/HeadIcons/HeadImage01") as Texture);
//         for (int i = 0; i < _tempList.Count; i++)
//         {
//             odinMenuTree.Add(_tempList[i].ToString(), _dicBaseData[_tempList[i].ItemType], Resources.Load(_tempList[i].GetMenuIconPath()) as Texture);
//         }
//         return odinMenuTree;
//     }
//
//     protected override void DrawMenu()
//     {
//         base.DrawMenu();
//         RunGameInit();
//         if (null == MenuTree.Selection.SelectedValue) return;
//         ItemData itemData = MenuTree.Selection.SelectedValue as ItemData;
//         itemData.items.Clear();
//         for (int i = 0; i < itemData.allItem.Count; i++)
//         {
//             if (itemData.allItem[i].IsShow(itemData.searchSign)
//                 && itemData.allItem[i].IsShow(itemData.qualityItemEditor))
//             {
//                 itemData.items.Add(itemData.allItem[i]);
//             }
//         }
//     }
//
//     private void RunGameInit()
//     {
//         if (_tempList.Count == 0 || _dicBaseData.Count == 0)
//             Init();
//     }
//     public void Init()
//     {
//         ExlRow info = null;
//         EditorItem itemInfo = null;
//         int count = ExlDataMgr.Item.GetRowCount();
//         _dicBaseData.Clear();
//         if (_dicBaseData == null || _dicBaseData.Count == 0)
//         {
//             for (int i = 0; i < count; i++)
//             {
//                 info = ExlDataMgr.Item.GetRowByIndex(i);
//                 if (info == null)
//                 {
//                     continue;
//                 }
//                 ItemType _type = (ItemType)ExlField_Item.GetItemType(info.GetRowFieldInt(ExlField_Item.ItemID));
//                 if (!IsShow(_type))
//                 {
//                     itemInfo = new EditorItem(info);
//                     datas.allItem.Add(itemInfo);
//                     continue;
//                 }
//                 if (!_dicBaseData.ContainsKey(_type))
//                 {
//                     _dicBaseData.Add(_type, new ItemData());
//                 }
//                 if (_dicBaseData[_type] == null)
//                 {
//                     _dicBaseData[_type] = new ItemData();
//                 }
//                 itemInfo = new EditorItem(info);
//                 datas.allItem.Add(itemInfo);
//                 _dicBaseData[_type].allItem.Add(itemInfo);
//                 if (_tempList.FindIndex(x => x.ItemType == _type) == -1)
//                 {
//                     _tempList.Add(new GetItemEditorData()
//                     {
//                         ItemType = _type,
//                         IsFold = false,
//                     });
//                 }
//                 info = null;
//             }
//         }
//     }
//     private bool IsShow(ItemType _type)
//     {
//         if (_type == ItemType.Equipment
//             || _type == ItemType.AircraftCarrier
//             || _type == ItemType.Submarine
//             || _type == ItemType.StationSkin
//             || _type == ItemType.GloryCarrierAura
//             || _type == ItemType.TalentTypeFixedQualityTalent
//             || _type == ItemType.TalentTypeQualityAndAbove
//             || _type == ItemType.Aircraft
//             || _type == ItemType.SkinProps
//             )
//         {
//             return true;
//         }
//         return false;
//
//     }
//     [Serializable]
//     public class GetItemEditorData
//     {
//         public ItemType ItemType;
//         public string menuIconPath;
//         public bool IsFold = false;
//
//         public string GetMenuIconPath()
//         {
//             switch (ItemType)
//             {
//                 case ItemType.Equipment:
//                     return "Textures/item/ICON-DJ-hmzb-dd1";
//                 case ItemType.AircraftCarrier:
//                     return "Textures/HeadIcons/HeadImage01";
//                 case ItemType.Submarine:
//                     return "Textures/Item/Submarine/QT_XG_sunyuanyi";
//                 case ItemType.StationSkin:
//                     return "Textures/item/HZ_GM_zhuzhadian01";
//                 case ItemType.GloryCarrierAura:
//                     return "Textures/Item/ICON-DJ-xingjun03";
//                 case ItemType.TalentTypeFixedQualityTalent:
//                     return "Textures/HeadIcons/yinengqu/tiancai_201_niudun";
//                 case ItemType.TalentTypeQualityAndAbove:
//                     return "Textures/HeadIcons/yinengqu/tiancai_201_niudun";
//                 case ItemType.Aircraft:
//                     return "Textures/Item/PlanesUI/Hongzhaji/HZ_B-2YL";
//                 case ItemType.SkinProps:
//                     return "Textures/CVBaseSkin/HZ_zhangyuchuanzhang";
//                 default:
//                     return string.Empty;
//             }
//         }
//         public override string ToString()
//         {
//             string str = "";
//             if (ItemType == ItemType.Equipment)
//             {
//                 str = "装备";
//             }
//             else if (ItemType == ItemType.AircraftCarrier)
//             {
//                 str = "航母";
//             }
//             else if (ItemType == ItemType.Submarine)
//             {
//                 str = "潜艇";
//             }
//             else if (ItemType == ItemType.StationSkin)
//             {
//                 str = "驻扎皮肤";
//             }
//             else if (ItemType == ItemType.GloryCarrierAura)
//             {
//                 str = "荣耀:航母光环";
//             }
//             else if (ItemType == ItemType.TalentTypeFixedQualityTalent)
//             {
//                 str = "天才类型-品质固定天才";
//             }
//             else if (ItemType == ItemType.TalentTypeQualityAndAbove)
//             {
//                 str = "天才类型-品质及以上天才";
//             }
//             else if (ItemType == ItemType.Aircraft)
//             {
//                 str = "飞机";
//             }
//             else if (ItemType == ItemType.SkinProps)
//             {
//                 str = "基地皮肤";
//             }
//             return str;
//         }
//     }
//     public class ItemData
//     {
//         [Title("直接获取"),]
//         [ShowInInspector, LabelText("道具ID")]
//         public static int itemId;
//         [ShowInInspector, InlineButton("GetItem", "道具ID获取")]
//         public static int num = 1;
//         [PropertySpace(20f)]
//         [Title("搜索筛选")]
//         [LabelText("搜索(Name|ID):")]
//         public string searchSign;
//         [LabelText("选择品质")]
//         public QualityItemEditor qualityItemEditor;
//         public List<EditorItem> items = new List<EditorItem>();
//         [HideInInspector]
//         public List<EditorItem> allItem = new List<EditorItem>();
//
//         private void GetItem()
//         {
//             string order = string.Format("getitem {0} {1}", itemId, num);
//             GMCommon.Execute(order);
//         }
//     }
//
//     [Serializable]
//     public class EditorItem
//     {
//         public int Num = 1;
//         [Button("获取道具")]
//         private void GetItem()
//         {
//             string order = string.Format("getitem {0} {1}", ItemID, Num);
//             if (ItemType == ItemType.Aircraft)
//             {
//                 order = string.Format("addplane {0} {1}", ItemID, 70);
//             }
//             GMCommon.Execute(order);
//         }
//
//         [ShowInInspector, EnableGUI, InlineButton("CopyItemID", "Copy")]
//         public int ItemID
//         {
//             get { return itemId; }
//         }
//         private int itemId;
//         private void CopyItemID()
//         {
//             Copy(ItemID.ToString());
//         }
//         [ShowInInspector, EnableGUI, InlineButton("CopyItemName", "Copy")]
//         public string ItemName {
//             get
//             {
//                 return itemName;
//             }
//         } 
//
//         private string itemName;
//         private void CopyItemName()
//         {
//             Copy(ItemName);
//         }
//         [HideInInspector]
//         public ItemType ItemType {
//             get
//             {
//                 return itemType;
//             } 
//         }
//         private ItemType itemType;
//         [ShowInInspector]
//         public int Quality {
//             get
//             {
//                 return quality;
//             }
//         }
//
//         private int quality;
//         [PreviewField(Sirenix.OdinInspector.ObjectFieldAlignment.Left), ReadOnly]
//         public Texture Icon;
//         public EditorItem(ExlRow info)
//         {
//             if (info != null)
//             {
//                 itemId = info.GetRowFieldInt(ExlField_Item.ItemID);
//                 itemName = ExlField_Item.GetItemName(ItemID);
//                 quality = ExlField_Item.GetItemQua(ItemID);
//                 itemType = (ItemType)ExlField_Item.GetItemType(ItemID);
//                 Icon = Resources.Load(ExlField_Item.GetItemRes(ItemID)) as Texture;
//             }
//         }
//
//         public bool IsShow(string filter)
//         {
//             if (Utility.IsEmptyStr(filter))
//             {
//                 return true;
//             }
//             return ItemName.Contains(filter) || ItemID.ToString().Contains(filter);
//         }
//         public bool IsShow(QualityItemEditor filter)
//         {
//             if (filter == QualityItemEditor.None)
//             {
//                 return true;
//             }
//             return Quality == (int)filter;
//         }
//         private void Copy(string str)
//         {
//             TextEditor textEditor = new TextEditor();
//             textEditor.text = str;
//             textEditor.SelectAll();
//             textEditor.Copy();
//         }
//     }
//
//     public enum QualityItemEditor
//     {
//         None = 0,
//         Qua1,
//         Qua2,
//         Qua3,
//         Qua4,
//         Qua5,
//         Qua6,
//     }
// }
