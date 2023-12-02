using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory.UI
{
    public class ItemCellsController : MonoBehaviour
    {
        public event Action <ItemCell> OnSelectedCellChanged;
        public ItemCell CurrentSelectedCell { get; private set; }

        [field: SerializeField] public RectTransform CellsBounds { get;private set; }

        [SerializeField] private List<ItemCell> _itemCells;

        public void Initialize()
        {
            ItemCell.OnCellClicked += SelectCell;
            
            SetupCells();

            var firstCell = _itemCells.FirstOrDefault(x => x.IsEmpty == false);
            SelectCell(firstCell? firstCell : _itemCells[0]);
        }
        
        public void SetupCells()
        {
            var currentItems = Locator.Instance.InventoryController.Data.CurrentItems;
            
            for (int i = 0; i < _itemCells.Count; i++)
            {
                if (i<currentItems.Length)
                {
                    var existingItemType = currentItems[i];
                    if (existingItemType!=InteractableItemType.NONE)
                    {
                        _itemCells[i].Initialize(this,i,Locator.Instance.InteractableItemsConfig.GetItemData(existingItemType)); //not empty cell
                        continue;
                    }
                    _itemCells[i].Initialize(this,i); //empty cell
                }
            }
        }
        
        private void SelectCell(ItemCell itemCell)
        {
            if (CurrentSelectedCell != itemCell)
            {
                CurrentSelectedCell = itemCell;
                OnSelectedCellChanged?.Invoke(itemCell);
            }
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            _itemCells = GetComponentsInChildren<ItemCell>().ToList();
        }

#endif
    }
}
