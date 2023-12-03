using System;
using Inventory.UI;
using Journal;
using TabsMenu.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace TabsMenu
{
    public class TabsMenuScreen : BaseScreen
    {
        [SerializeField] private Camera _itemInspectionCamera;
        [SerializeField] private Button _inventoryButton;
        [SerializeField] private Button _journalButton;
        [SerializeField] private InventoryTab _inventoryTab;
        [SerializeField] private JournalTab _journalTab;
        private BaseMenuTab _currentActiveTab;

        private void Awake()
        {
            _inventoryTab.gameObject.SetActive(false);
            _journalTab.gameObject.SetActive(false);
        }

        public BaseScreen Initialize(TabsMenuScreenMode mode)
        {
            Locator.Instance.CameraController.AddOverlayCameraToStack(_itemInspectionCamera);

            _inventoryTab.Initialize(mode);
            _journalTab.Initialize(mode);
            
            ShowTab(_inventoryTab);

            AddListeners();
            return this;
        }

        public override void Hide()
        {
            RemoveListeners();
            
            Locator.Instance.CameraController.RemoveOverlayCameraFromStack(_itemInspectionCamera);
            base.Hide();
        }

        private void ShowTab(BaseMenuTab tab)
        {
            if (_currentActiveTab!=null)
            {
                if (_currentActiveTab==tab)
                {
                    return;
                }
                _currentActiveTab.Hide();
            }
            
            tab.Show();
            _currentActiveTab = tab;
        }

        private void AddListeners()
        {
            _inventoryButton.onClick.AddListener(()=>ShowTab(_inventoryTab));
            _journalButton.onClick.AddListener(()=>ShowTab(_journalTab));
        }

        private void RemoveListeners()
        {
            _inventoryButton.onClick.RemoveAllListeners();
            _journalButton.onClick.RemoveAllListeners();
        }
        
    }
}
