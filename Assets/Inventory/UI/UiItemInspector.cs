using UnityEngine;

namespace Inventory.UI
{
    public class UiItemInspector : MonoBehaviour
    {
        [SerializeField] private InteractionItemRotator itemRotator;
        [SerializeField] private RectTransform _itemBoundsRt;
        [SerializeField] private RectTransform _itemParent;
        private GameObject _currentInspectedItem;
        
        public void SetItemToInspect(GameObject target)
        {
            if (_currentInspectedItem!=null)
            {
                Destroy(_currentInspectedItem);
            }
            _currentInspectedItem = Instantiate(target, _itemParent, true);
            _currentInspectedItem.layer = gameObject.layer;
            FitObject(_currentInspectedItem);
            _currentInspectedItem.transform.localPosition = Vector3.zero;
            itemRotator.SetTarget(_currentInspectedItem);
        }

        public void ClearInspection()
        {
            if (_currentInspectedItem!=null)
            {
                itemRotator.Clear();
                Destroy(_currentInspectedItem);
            }
        }

        private void FitObject(GameObject target)
        {
            Vector2 rectSize = _itemBoundsRt.rect.size/2;

            MeshRenderer objectRenderer = target.GetComponent<MeshRenderer>();
            Bounds objectBounds = objectRenderer.bounds;

            float scaleX = rectSize.x / objectBounds.size.x;
            float scaleY = rectSize.y / objectBounds.size.y;
            float scaleZ = rectSize.y / objectBounds.size.z;

            float scale = Mathf.Min(scaleX, scaleY, scaleZ);

            target.transform.localScale = new Vector3(scale, scale, scale);
        }
        
    }
}
