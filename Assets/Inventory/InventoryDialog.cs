using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Inventory
{
    public class InventoryDialog : Dialog
    {
        [SerializeField] private Camera _itemInspectionCamera;
        [SerializeField] private List<ItemCell> _itemCells;
        [SerializeField] private UiItemInspector _uiItemInspector;
        
        private void Start()
        {
            Locator.Instance.CameraController.AddOverlayCameraToStack(_itemInspectionCamera);
            var currentItems = Locator.Instance.InventoryController.Data.CurrentItems;

            for (int i = 0; i < _itemCells.Count; i++)
            {
                if (i<currentItems.Count)
                {
                    _itemCells[i].Init(Locator.Instance.InteractableItemsConfig.GetItemData(currentItems[i]));
                }
                else
                {
                    _itemCells[i].Init();
                }
            }
            _itemCells[0].SelectCell();
        }

        private void OnEnable()
        {
            ItemCell.OnSelectedCellChanged += OnSelectedCellChanged;
        }

        private void OnDisable()
        {
            ItemCell.OnSelectedCellChanged -= OnSelectedCellChanged;
        }

        private void OnSelectedCellChanged(ItemCell itemCell)
        {
            if (itemCell.IsEmpty==false)
            {
                _uiItemInspector.SetItemToInspect(itemCell.ItemData.Prefab);
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
