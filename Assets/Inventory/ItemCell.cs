using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class ItemCell : MonoBehaviour, IPointerClickHandler
    {
        public static event Action<ItemCell> OnSelectedCellChanged;
        
        private static ItemCell SelectedItemCell;
        
        public ItemData ItemData { get; private set; }
        public bool IsEmpty => ItemData == null;
        
        [SerializeField] private Image _icon;
        [SerializeField] private Image _selectionBorder;


        public void Init(ItemData itemData = null)
        {
            if (itemData==null)
            {
                _icon.enabled = false;
            }
            else
            {
                ItemData = itemData;
                _icon.sprite = ItemData.Icon;
                _icon.enabled = true;
            }
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
    }
}