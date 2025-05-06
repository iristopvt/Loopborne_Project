using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum SlotType { Helmet, Armor, Shoes, Weapon, None }
    public SlotType slotType;

    public Image itemImage;
    public Item currentItem;

    public PlayerController playerController; 


    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("드롭 이벤트 발생");

        Slot dragSlot = DragSlot.instance.dragSlot;

  

        if (IsMatchSlotType(dragSlot.item.equipmentType))
        {
            currentItem = dragSlot.item;
            itemImage.sprite = dragSlot.item.itemImage;
            itemImage.color = Color.white;

            dragSlot.ClearSlot(); 

            Debug.Log($"장비 장착됨: {currentItem.itemName}");

            if (slotType == SlotType.Weapon && playerController != null)
            {
                playerController.rightHandWeaponObject.SetActive(true);
            }
        }
    }

    private bool IsMatchSlotType(Item.EquipmentType type)
    {
        return
            (slotType == SlotType.Helmet && type == Item.EquipmentType.Helmet) ||
            (slotType == SlotType.Armor && type == Item.EquipmentType.Armor) ||
            (slotType == SlotType.Shoes && type == Item.EquipmentType.Boots) ||
            (slotType == SlotType.Weapon && type == Item.EquipmentType.Weapon);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
      
        if (currentItem != null)
        {
            DragSlot.instance.dragSlot = null;
            DragSlot.instance.dragEquipSlot = this; 
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;


        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentItem != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      

        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragEquipSlot = null; 
    }

    public void ClearEquipSlot()
    {
        if (slotType == SlotType.Weapon && playerController != null)
        {
            playerController.rightHandWeaponObject.SetActive(false);
        }

        currentItem = null;
        itemImage.sprite = null;
        itemImage.color = new Color(1, 1, 1, 0);

     
    }
}
