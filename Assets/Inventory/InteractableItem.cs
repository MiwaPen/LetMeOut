using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Inventory
{
    public class InteractableItem : MonoBehaviour
    {
        [SerializeField] private InteractableItemType _interactableItemType;
        [SerializeField, ReadOnly] private ItemData _data;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_data == null ||  _data.InteractableItemType!=_interactableItemType)
            {
                var allConfigs = AssetDatabase.FindAssets("t:ScriptableObject");

                InteractableItemsConfig config = null;
                foreach (var path in allConfigs)
                {
                    var finalPath =AssetDatabase.GUIDToAssetPath(path);
                    var obj = AssetDatabase.LoadAssetAtPath(finalPath, typeof(InteractableItemsConfig));
                    if (obj != null)
                    {
                        config = obj as InteractableItemsConfig;
                    }
                }

                if (config!=null)
                {
                    _data = config.GetItemData(_interactableItemType);
                }
            }
        }
#endif
        
        [Button]
        private void GetItemInInventory()
        {
            Locator.Instance.InventoryController.AddItemToInventory(_interactableItemType);
            Destroy(gameObject);
        }
    }
}
