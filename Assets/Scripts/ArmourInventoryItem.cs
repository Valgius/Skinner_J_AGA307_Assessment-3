using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Inventory System/Items/Armour")]
public class ArmourInventoryItem : InventoryItem
{
    public override void AssignItemToPlayer(PlayerEquipmentController playerEquipment)
    {
        playerEquipment.AssignArmourItem(this);

    }
}
