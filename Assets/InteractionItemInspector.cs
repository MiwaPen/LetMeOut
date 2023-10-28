using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionItemInspector : MonoBehaviour, IDragHandler
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _rotatingSpeed = 18f;
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 rotation = new Vector3(eventData.delta.y, -eventData.delta.x, 0f) * _rotatingSpeed * Time.deltaTime;
        _target.transform.Rotate(rotation, Space.World);
    }
    
    // Vector3 rotation = new Vector3(Mathf.Lerp(0,Mathf.Abs(eventData.delta.y), _lerpSpeed * Time.deltaTime) * Mathf.Sign(eventData.delta.y),
    //     Mathf.Lerp(0,Mathf.Abs(eventData.delta.x), _lerpSpeed * Time.deltaTime) * Mathf.Sign(-eventData.delta.x), 0f) * _rotatingSpeed;
    // _target.transform.Rotate(rotation, Space.World);
}
