using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentController : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Transform inventoryUIParent;

    private void Start()
    {
        inventory.InitInventory(this);
        inventory.OpenInventoryUI();
    }

    public Transform GetUIParent()
    {
        return inventoryUIParent;
    }
}
