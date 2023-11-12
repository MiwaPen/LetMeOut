using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionItemRotator : MonoBehaviour, IDragHandler, IPointerExitHandler
{
    [SerializeField] private float _rotatingSpeed = 18f;
    [SerializeField] private bool _stopWhenOffBounds = true;
    [SerializeField] private Vector3 _defaultRotationAngles = new (-20f,45f,-18f);
    
    private GameObject _target;
    private bool _canDrag;

    public void SetTarget(GameObject target)
    {
        _target = target;
        _target.transform.eulerAngles = _defaultRotationAngles;
        _canDrag = true;
    }

    public void Clear()
    {
        _target = null;
        _canDrag = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_canDrag)
        {
            Vector3 rotation = new Vector3(eventData.delta.y, -eventData.delta.x, 0f) * _rotatingSpeed * Time.deltaTime;
            _target.transform.Rotate(rotation, Space.World);
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (_stopWhenOffBounds)
        {
            eventData.pointerDrag = null;
        }
    }
}
