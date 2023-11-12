using UnityEngine;

namespace Inventory
{
    public abstract class Dialog : MonoBehaviour
    {
        public virtual void Hide()
        {
            Destroy(gameObject);
        }
    }
}