using TabsMenu.Enums;
using UnityEngine;

namespace TabsMenu
{
    public abstract class BaseMenuTab : MonoBehaviour
    {
        public abstract BaseMenuTab Initialize(TabsMenuScreenMode mode);
        
        public abstract void Show();
        public abstract void Hide();
    }
}
