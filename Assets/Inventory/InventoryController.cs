using System;
using System.Collections.Generic;
using System.IO;
using Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private const string CURRENT_ITEMS_PATH = "Assets/" + "InventoryInfo.json";
    
    [SerializeField, ReadOnly] private InventoryData _data;
    public InventoryData Data => _data;
    
    private void Awake()
    {
        GetData();
        _data ??= new InventoryData();
    }

    public void AddItemToInventory(InteractableItemType itemType)
    {
        if (_data.CurrentItems.Contains(itemType)==false) //prevent duplicates for now??
        {
            _data.CurrentItems.Add(itemType);
            SaveData();
        }
    }

    public void RemoveItemFromInventory(InteractableItemType itemType)
    {
        if (_data.CurrentItems.Contains(itemType))
        {
            _data.CurrentItems.Remove(itemType);
            SaveData();
        }
    }
    
    [Button]
    private void SaveData()
    { 
        File.WriteAllText(CURRENT_ITEMS_PATH, JsonUtility.ToJson(_data));
    }

    [Button]
    private void GetData()
    {
        var data =File.ReadAllText(CURRENT_ITEMS_PATH);
        _data = JsonUtility.FromJson<InventoryData>(data);
    }

    [Button]
    private void ClearData()
    {
        File.WriteAllText(CURRENT_ITEMS_PATH, string.Empty);
    }

    [Serializable]
    public class InventoryData
    {
        public List<InteractableItemType> CurrentItems = new();
    }
}
