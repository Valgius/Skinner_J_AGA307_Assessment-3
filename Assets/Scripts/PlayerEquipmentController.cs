using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEquipmentController : Singleton<PlayerEquipmentController>
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
    public string weaponType;
   

    private void Start()
    {
        inventory.InitInventory(this);
        inventory.OpenInventoryUI();
        weaponType = "Unarmed";
    }

    public void AssignArmourItem (ArmourInventoryItem item)
    {
        DestroyIfNotNull(currentArmourObj);
        currentArmourObj = CreateNewItemInstance(item, armourAnchor);
    }

    /// <summary>
    /// Destroys any exixitng weapon at hand anchor and replaces it with a new one.
    /// </summary>
    /// <param name="item">weapon pulled from inventory item script</param>
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
                weaponType = "1H";
                break;
            case Hand.TWOHAND:
                DestroyIfNotNull(currentRightHandObj);
                DestroyIfNotNull(currentLeftHandObj);
                currentRightHandObj = CreateNewItemInstance(item, rightHandAnchor);
                weaponType = "2H";
                break;
            default:
                break;
        }
    }

    ///Creates a prefab at specified postition and rotation.
    private GameObject CreateNewItemInstance(InventoryItem item, Transform anchor)
    {
        var itemInstance = Instantiate(item.GetPrefab(), anchor);
        itemInstance.transform.localPosition = item.GetLocalPosition();
        itemInstance.transform.localRotation = item.GetLocalRotation();
        return itemInstance;
    }

    ///Destroys item already present in slot
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
