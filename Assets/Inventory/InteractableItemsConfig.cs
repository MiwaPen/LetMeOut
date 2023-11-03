using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(menuName = "InteractableItems/GeneralConfig", fileName = "InteractableItemsConfig")]
    public class InteractableItemsConfig : ScriptableObject
    {
        [SerializeField] private List<ItemData> _itemsDatas;
        
        public ItemData GetItemData(InteractableItemType type)
        {
           return _itemsDatas.FirstOrDefault(x => x.InteractableItemType == type);
        }
    }
    
    [Serializable]
    public class ItemData
    {
        public InteractableItemType InteractableItemType;
        public string Name;
        public Sprite Icon;
        public GameObject Prefab;
    }
}
