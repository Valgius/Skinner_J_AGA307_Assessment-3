using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEquipmentController : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Transform inventoryUIParent;
    [SerializeField] private Transform leftHandAnchor;
    [SerializeField] private Transform rightHandAnchor;
    [SerializeField] private Transform armourAnchor;

    [Header ("Anchors")]
    private GameObject currentLeftHandObj;
    private GameObject currentRightHandObj;
    private GameObject currentArmourObj;

    private void Start()
    {
        inventory.InitInventory(this);
        inventory.OpenInventoryUI();
    }

    public void AssignArmourItem (ArmourInventoryItem item)
    {
        DestroyIfNotNull(currentArmourObj);
        currentArmourObj = CreateNewItemInstance(item, armourAnchor);
    }

    public void AssignHandItem(HandInventoryItem item)
    {
        switch (item.hand)
        {
            case Hand.LEFT:
                DestroyIfNotNull(currentLeftHandObj);
                currentLeftHandObj = CreateNewItemInstance(item, leftHandAnchor);
                break;
            case Hand.RIGHT:
                DestroyIfNotNull(currentRightHandObj);
                currentRightHandObj = CreateNewItemInstance(item, rightHandAnchor);
                break;
            case Hand.TWOHAND:
                DestroyIfNotNull(currentRightHandObj);
                DestroyIfNotNull(currentLeftHandObj);
                currentRightHandObj = CreateNewItemInstance(item, rightHandAnchor);
                break;
            default:
                break;
        }
    }

    private GameObject CreateNewItemInstance(InventoryItem item, Transform anchor)
    {
        var itemInstance = Instantiate(item.GetPrefab(), anchor);
        itemInstance.transform.localPosition = item.GetLocalPosition();
        itemInstance.transform.localRotation = item.GetLocalRotation();
        return itemInstance;
    }
    private void DestroyIfNotNull(GameObject obj)
    {
        if (obj)
        {
            Destroy(obj);
        }
    }

    public Transform GetUIParent()
    {
        return inventoryUIParent;
    }
}
