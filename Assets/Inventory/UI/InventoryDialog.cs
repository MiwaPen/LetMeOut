using Inventory.Enums;
using UnityEngine;

namespace Inventory.UI
{
    public class InventoryDialog : Dialog
    {
        [SerializeField] private Camera _itemInspectionCamera;
        [SerializeField] private UiItemInspector _uiItemInspector;
        [SerializeField] private ItemCellsController _itemCellsController;
        [SerializeField] private ItemControlButtonsController _itemControlButtonsController;
        private InventoryDialogMode _mode;
        
        public Dialog Initialize(InventoryDialogMode mode)
        {
            _mode = mode;
            
            _itemCellsController.OnSelectedCellChanged += OnSelectedCellChanged;

            _itemCellsController.Initialize();
            _itemControlButtonsController.Initialize(_mode);
            _itemControlButtonsController.SetCellView(_itemCellsController.CurrentSelectedCell);
            
            Locator.Instance.InventoryController.OnItemRemoved += OnInventoryChanged;
            Locator.Instance.InventoryController.OnItemAdded += OnInventoryChanged;
            
            Locator.Instance.CameraController.AddOverlayCameraToStack(_itemInspectionCamera);
            return this;
        }
        
        private void OnDisable()
        {
            _itemCellsController.OnSelectedCellChanged -= OnSelectedCellChanged;
            Locator.Instance.InventoryController.OnItemRemoved -= OnInventoryChanged;
            Locator.Instance.InventoryController.OnItemAdded -= OnInventoryChanged;
        }
        
        private void OnInventoryChanged(InteractableItemType itemType)
        {
            _itemCellsController.SetupCells();
            RefreshInspectorView();
            _itemControlButtonsController.SetCellView(_itemCellsController.CurrentSelectedCell);
        }

        private void OnSelectedCellChanged(ItemCell itemCell)
        {
            RefreshInspectorView();
            _itemControlButtonsController.SetCellView(_itemCellsController.CurrentSelectedCell);
        }

        private void RefreshInspectorView()
        {
            if (_itemCellsController.CurrentSelectedCell.IsEmpty==false)
            {
                _uiItemInspector.SetItemToInspect(_itemCellsController.CurrentSelectedCell.ItemData.Prefab);
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
