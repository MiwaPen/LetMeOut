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
        [field: SerializeField] public InteractableItemType InteractableItemType { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}
