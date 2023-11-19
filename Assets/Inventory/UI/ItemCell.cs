using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class ItemCell : MonoBehaviour, IPointerClickHandler, IDropHandler
    {
        public static event Action<ItemCell> OnCellClicked;
        
        public ItemData ItemData { get; private set; }
        public bool IsEmpty => ItemData == null;

        [SerializeField] private ItemView _itemView;
        [SerializeField] private Transform _itemViewParent;
        [SerializeField] private Image _selectionBorder;
        private ItemCellsController _itemCellsController;
        private int _indexInCells;

        public void Initialize(ItemCellsController itemCellsController, int indexInCells, ItemData itemData=null)
        {
            _itemCellsController = itemCellsController;
            _itemCellsController.OnSelectedCellChanged += RefreshView;
            _indexInCells = indexInCells;
            SetItemData(itemData);
        }

        private void OnDisable()
        {
            _itemCellsController.OnSelectedCellChanged -= RefreshView;
        }
        
        private void RefreshView(ItemCell selectedCell)
        {
            if (selectedCell==this)
            {
                _selectionBorder.color = Color.white;
            }
            else
            {
                _selectionBorder.color = Color.black;
            }
        }
        
        private void SetItemData(ItemData itemData = null)
        {
            ItemData = itemData;
            RefreshItemView();
        }

        private void RefreshItemView()
        {
            if (ItemData==null)
            {
                _itemView.SetActive(false);
            }
            else
            {
                _itemView.SetIcon(ItemData.Icon);
                _itemView.SetActive(true);
            }
            
            _itemView.Init(this, _itemViewParent);
        }

        private void SetNewCellData(ItemView itemView, ItemData itemData)
        {
            _itemView = itemView;
            SetItemData(itemData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnCellClicked?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag!=null && eventData.pointerDrag != _itemView.gameObject)
            {
                SwapItemDatas(eventData.pointerDrag.GetComponent<ItemView>().ItemCell, this);
                OnCellClicked?.Invoke(this);
            }
        }

        private void SwapItemDatas(ItemCell cellA, ItemCell cellB)
        {
            Locator.Instance.InventoryController.SwapItemsIndexes(cellA._indexInCells, cellB._indexInCells);

            var cellAView = cellA._itemView;
            var cellAData = cellA.ItemData;
            
            cellA.SetNewCellData(cellB._itemView, cellB.ItemData);
            cellB.SetNewCellData(cellAView, cellAData);
        }
    }
}