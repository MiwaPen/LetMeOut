using TabsMenu;
using TabsMenu.Enums;
using UnityEngine;

namespace Inventory.UI
{
    public class InventoryTab : BaseMenuTab
    {
        [SerializeField] private UiItemInspector _uiItemInspector;
        [SerializeField] private ItemCellsController _itemCellsController;
        [SerializeField] private ItemControlButtonsController _itemControlButtonsController;
        private TabsMenuScreenMode _mode;
        
        public override BaseMenuTab Initialize(TabsMenuScreenMode mode)
        {
            _mode = mode;
            return this;
        }

        public override void Show()
        {
            AddListeners();
            
            _itemCellsController.Initialize();
            _itemControlButtonsController.Initialize(_mode);
            _itemControlButtonsController.SetCellView(_itemCellsController.CurrentSelectedCell);
            
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            RemoveListeners();
            gameObject.SetActive(false);
        }
     
        private void AddListeners()
        {
            _itemCellsController.OnSelectedCellChanged += OnSelectedCellChanged;
            Locator.Instance.InventoryController.OnItemRemoved += OnInventoryChanged;
            Locator.Instance.InventoryController.OnItemAdded += OnInventoryChanged;
        }
        
        private void RemoveListeners()
        {
            _itemCellsController.OnSelectedCellChanged -= OnSelectedCellChanged;
            Locator.Instance.InventoryController.OnItemRemoved -= OnInventoryChanged;
            Locator.Instance.InventoryController.OnItemAdded -= OnInventoryChanged;
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
    }
}
