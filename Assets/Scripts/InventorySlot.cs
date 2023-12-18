using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemCountText;
    [SerializeField] private Button slotButton;

    public void InitSlotVisualisation(Sprite itemSprite, string itemName, int itemCount)
    {
        itemImage.sprite = itemSprite;
        itemNameText.text = itemName;
        UpdateSlotCount(itemCount);
    }

    public void UpdateSlotCount(int itemCount)
    {
        itemCountText.text = itemCount.ToString();
    }

    public void AssignSlotButtonCallback(System.Action onClickCallback)
    {
        slotButton.onClick.AddListener(() => onClickCallback());
    }
}
