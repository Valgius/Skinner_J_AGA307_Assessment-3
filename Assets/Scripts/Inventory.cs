using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Inventory System/Inventory")] 
public class Inventory : ScriptableObject
{
    [SerializeField] private List<InventoryItemWrapper> items = new List<InventoryItemWrapper>(); //Set inventory content from inspector
    [SerializeField] private InventoryUI inventoryUIPrefab;
    private InventoryUI _inventoryUI;
    private InventoryUI InventoryUI
    {
        get
       {
           if(!_inventoryUI)
            {
                _inventoryUI = Instantiate(inventoryUIPrefab, playerEquipment.GetUIParent());
            }
            return _inventoryUI;
        }
    }

    private Dictionary<InventoryItem, int> itemToCountMap = new Dictionary<InventoryItem, int>(); //Map item from wrapper to their number
    private PlayerEquipmentController playerEquipment;
    
    /// <summary>
    /// Fill dictionary with values from the inventory item wrapper list
    /// </summary>
    public void InitInventory(PlayerEquipmentController playerEquipment)
    {
        this.playerEquipment = playerEquipment;
        for (int i = 0; i < items.Count; i++)
        {
            itemToCountMap.Add(items[i].GetItem(), items[i].GetItemCount());
        }
    }

    public void OpenInventoryUI()
    {
        InventoryUI.gameObject.SetActive(true);
        InventoryUI.InitInventoryUI(this);
    }

    /// <summary>
    /// Assigns item to the player
    /// </summary>
    public void AssignItem(InventoryItem item)
    {
        Debug.Log(string.Format("Player assigned (0) item", item.GetName()));
    }

    public Dictionary<InventoryItem, int> GetAllItemsMap()
    {
        return itemToCountMap;
    }

    /// <summary>
    /// Adds Items to inventory
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(InventoryItem item, int count)
    {
        int currentItemCount;
        if(itemToCountMap.TryGetValue(item, out currentItemCount))
        {
            itemToCountMap[item] = currentItemCount + count;
        }
        else
        {
            itemToCountMap.Add(item, count);
        }
        InventoryUI.CreateOrUpdateSlot(this, item, count);
    }

    /// <summary>
    /// Removes items from the inventory
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"></param>
    public void RemoveItem(InventoryItem item, int count)
    {
        int currentItemCount;
        if (itemToCountMap.TryGetValue(item, out currentItemCount))
        {
            itemToCountMap[item] = currentItemCount - count;
            if (currentItemCount - count <= 0)
            {
                InventoryUI.DestroySlot(item);
            }
            else
            {
                InventoryUI.UpdateSlot(item, currentItemCount - count);
            }
        }
        else
        {
            Debug.Log(string.Format("Cant remove {0}. This item is not in the inventory"));
        }
    }
}
