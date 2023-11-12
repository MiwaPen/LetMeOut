using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

public class Locator : MonoBehaviour
{
    public static Locator Instance;
    
    [field: SerializeField] public CameraController CameraController { get; private set; }
    [field: SerializeField] public InventoryController InventoryController { get; private set; }
    [field: SerializeField] public InteractableItemsConfig InteractableItemsConfig { get; private set; }
    
    private void Awake()
    {
        if (Instance!=null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
