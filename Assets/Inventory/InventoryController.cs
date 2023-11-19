using System;
using System.IO;
using System.Linq;
using Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private const string CURRENT_ITEMS_PATH = "Assets/" + "InventoryInfo.json";

    public event Action<InteractableItemType> OnItemAdded;
    public event Action<InteractableItemType> OnItemRemoved;
    
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
            int emptyIndex = Array.IndexOf(_data.CurrentItems, InteractableItemType.NONE); 

            if (emptyIndex != -1)  
            {
                _data.CurrentItems[emptyIndex] = itemType; 
                OnItemAdded?.Invoke(itemType);
            }
            else
            {
                //inventory is full
            }
            SaveData();
        }
    }

    public void SwapItemsIndexes(int indexA, int indexB)
    {
        
        if (_data.CurrentItems.Length <= indexA || _data.CurrentItems.Length <= indexB)
        {
            Debug.LogError("One item in swap items are incorrect!");
            return;
        }
        (_data.CurrentItems[indexA], _data.CurrentItems[indexB]) = (_data.CurrentItems[indexB], _data.CurrentItems[indexA]);
        SaveData();
    }

    public void RemoveItemFromInventory(InteractableItemType itemType)
    {
        if (_data.CurrentItems.Contains(itemType))
        {
            int targetIndex = Array.IndexOf(_data.CurrentItems, itemType);
            _data.CurrentItems[targetIndex] = InteractableItemType.NONE;
            OnItemRemoved?.Invoke(itemType);

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
        public InteractableItemType[] CurrentItems;

        public InventoryData()
        {
            CurrentItems = new InteractableItemType[9];
            Array.Fill(CurrentItems, InteractableItemType.NONE);
        }
    }
}
