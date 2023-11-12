using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class ItemCell : MonoBehaviour, IPointerClickHandler, IDropHandler
    {
        public static event Action<ItemCell> OnSelectedCellChanged;
        private static ItemCell SelectedItemCell;
        
        public ItemData ItemData { get; private set; }
        public bool IsEmpty => ItemData == null;
        public int IndexInCells { get; private set; }

        [SerializeField] private ItemView _itemView;
        [SerializeField] private Transform _itemViewParent;
        [SerializeField] private Image _selectionBorder;

        public void Initialize(int indexInCells, ItemData itemData=null)
        {
            IndexInCells = indexInCells;
            SetItemData(itemData);
        }
        
        private void SetItemData(ItemData itemData = null)
        {
            ItemData = itemData;
            UpdateView();
        }

        private void UpdateView()
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

        private void OnEnable()
        {
            OnSelectedCellChanged += OnSelectedCell;
        }

        private void OnDisable()
        {
            OnSelectedCellChanged -= OnSelectedCell;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SelectCell();
        }

        public void SelectCell()
        {
            if (SelectedItemCell!=this)
            {
                SelectedItemCell = this;
                OnSelectedCellChanged?.Invoke(this);
            }
        }

        private void OnSelectedCell(ItemCell selectedCell)
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

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag!=null && eventData.pointerDrag != _itemView.gameObject)
            {
                SwapItemDatas(eventData.pointerDrag.GetComponent<ItemView>().ItemCell, this);
                SelectCell();
            }
        }

        private void SwapItemDatas(ItemCell cellA, ItemCell cellB)
        {
            Locator.Instance.InventoryController.SwapItemsIndexes(cellA.IndexInCells, cellB.IndexInCells);

            var cellAView = cellA._itemView;
            var cellAData = cellA.ItemData;
            
            cellA.SetNewCellData(cellB._itemView, cellB.ItemData);
            cellB.SetNewCellData(cellAView, cellAData);
        }
    }
}