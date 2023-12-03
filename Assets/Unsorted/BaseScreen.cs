using UnityEngine;

public abstract class BaseScreen : MonoBehaviour 
{
    public virtual void Hide()
    { 
        Destroy(gameObject);
    }
}