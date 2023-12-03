using System.Collections.Generic;
using System.Linq;
using Inventory;
using Inventory.UI;
using Sirenix.OdinInspector;
using TabsMenu;
using TabsMenu.Enums;
using UnityEngine;

public class ScreensController : MonoBehaviour
{
    [SerializeField] private TabsMenuScreen _tabsMenuScreen;
    private Stack<BaseScreen> _activeScreens = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideFrontDialog();
            return;
        }
    }

    private void HideFrontDialog()
    {
        if (_activeScreens.Count>0)
        {
            _activeScreens.Pop().Hide();
        }
    }

    [Button]
    private void ShowTabsScreen()
    {
        if (_activeScreens.FirstOrDefault(x=>x.GetType()==typeof(TabsMenuScreen)))
        {
            return;
        }
        _activeScreens.Push(Instantiate(_tabsMenuScreen, transform).Initialize(TabsMenuScreenMode.DEFAULT));
    }
    
    [Button]
    private void ShowTabsScreenInteractMode()
    {
        if (_activeScreens.FirstOrDefault(x=>x.GetType()==typeof(TabsMenuScreen)))
        {
            return;
        }
        _activeScreens.Push(Instantiate(_tabsMenuScreen, transform).Initialize(TabsMenuScreenMode.ITEM_INTERACTION));
    }
}
