using System.Collections.Generic;
using System.Linq;
using Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

public class DialogsController : MonoBehaviour
{
    [SerializeField] private InventoryDialog _inventoryDialog;
    private Stack<Dialog> _activeDialogs = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideFrontDialog();
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventoryDialog();
        }
    }

    private void HideFrontDialog()
    {
        if (_activeDialogs.Count>0)
        {
            _activeDialogs.Pop().Hide();
        }
    }

    [Button]
    private void ShowInventoryDialog()
    {
        if (_activeDialogs.FirstOrDefault(x=>x.GetType()==typeof(InventoryDialog)))
        {
            return;
        }
        _activeDialogs.Push(Instantiate(_inventoryDialog, transform));
    }
}
