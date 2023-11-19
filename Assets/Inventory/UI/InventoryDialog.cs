using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Enums;
using UnityEngine;

namespace Inventory.UI
{
    public class InventoryDialog : Dialog
    {
        public event Action <ItemCell> OnSelectedCellChanged;
        
        [SerializeField] private Camera _itemInspectionCamera;
        [SerializeField] private List<ItemCell> _itemCells;
        [SerializeField] private UiItemInspector _uiItemInspector;
        [SerializeField] private ItemControlButtonsController _itemControlButtonsController;
        private ItemCell _currentSelectedCell;
        private InventoryDialogMode _mode;
        
        public Dialog Initialize(InventoryDialogMode mode)
        {
            _mode = mode;
            ItemCell.OnCellClicked += SelectCell;
            
            Locator.Instance.InventoryController.OnItemRemoved += OnInventoryChanged;
            Locator.Instance.InventoryController.OnItemAdded += OnInventoryChanged;
            
            _itemControlButtonsController.Initialize(_mode);
            Locator.Instance.CameraController.AddOverlayCameraToStack(_itemInspectionCamera);

            SetupCells();
            
            var firstCell = _itemCells.FirstOrDefault(x => x.IsEmpty == false);
            SelectCell(firstCell? firstCell : _itemCells[0]);

            return this;
        }
        
        private void OnDisable()
        {
            ItemCell.OnCellClicked -= SelectCell;
            Locator.Instance.InventoryController.OnItemRemoved -= OnInventoryChanged;
            Locator.Instance.InventoryController.OnItemAdded -= OnInventoryChanged;
        }

        private void SetupCells()
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

        private void OnInventoryChanged(InteractableItemType itemType)
        {
            SetupCells();
            RefreshInspectorView();
            _itemControlButtonsController.SetCellView(_currentSelectedCell);
        }

        private void SelectCell(ItemCell itemCell)
        {
            if (_currentSelectedCell != itemCell)
            {
                _currentSelectedCell = itemCell;
                OnSelectedCellChanged?.Invoke(itemCell);
                RefreshInspectorView();
                _itemControlButtonsController.SetCellView(_currentSelectedCell);
            }
        }

        private void RefreshInspectorView()
        {
            if (_currentSelectedCell.IsEmpty==false)
            {
                _uiItemInspector.SetItemToInspect(_currentSelectedCell.ItemData.Prefab);
            }
            else
            {
                _uiItemInspector.ClearInspection();
            }
        }
        
        public override void Hide()
        {
            Locator.Instance.CameraController.RemoveOverlayCameraFromStack(_itemInspectionCamera);
            base.Hide();
        }
    }
}
