using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class ItemView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public ItemCell ItemCell { get; private set; }

        [SerializeField] private Image _icon;
        [SerializeField] private LayoutElement _layoutElement;
        
        private RectTransform _selfRectTr;
        private Transform _draggingParent;
        private Transform _defaultParent;

        public void Init(ItemCell itemCell, Transform parent)
        {
            _selfRectTr = transform as RectTransform;
            ItemCell = itemCell;
            _draggingParent = itemCell.transform.parent;

            _defaultParent = parent;
            transform.SetParent(_defaultParent);
            
            _layoutElement.ignoreLayout = false;
            _icon.raycastTarget = true;
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
        
        public void SetIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_icon.raycastTarget)
            {
                return;
            }
            RectTransformUtility.ScreenPointToLocalPointInRectangle(ItemCell.ItemCellsController.CellsBounds, 
                eventData.position, eventData.pressEventCamera, out Vector2 boundsPoint);
            
            if (ItemCell.ItemCellsController.CellsBounds.rect.Contains(boundsPoint))
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(_selfRectTr.parent as RectTransform, 
                    eventData.position, eventData.pressEventCamera, out Vector2 localPoint);
                _selfRectTr.localPosition = localPoint;
            }
            else
            {
                OnEndDrag(null);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _layoutElement.ignoreLayout = true;
            transform.SetParent(_draggingParent);
            transform.SetAsLastSibling();
            _icon.raycastTarget = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_icon.raycastTarget)
            {
                return;
            }
            transform.SetParent(_defaultParent);
            
            _layoutElement.ignoreLayout = false;
            _icon.raycastTarget = true;
        }
    }
}
