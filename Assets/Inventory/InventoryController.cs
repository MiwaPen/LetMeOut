using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;
    
    private const string CURRENT_ITEMS_PATH = "Assets/" + "InventoryInfo.json";
    
    [SerializeField] private InteractableItemsConfig _config;
    [SerializeField] private InventoryData _data;
    
    
    private void Awake()
    {
        if (Instance!=null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        GetData();
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

    [Serializable]
    public class InventoryData
    {
        public List<InteractableItemType> CurrentItems;
    }
}
