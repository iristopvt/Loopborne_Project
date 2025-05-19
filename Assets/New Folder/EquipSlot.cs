using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum SlotType { Helmet, Armor, Pants, Weapon, None }
    public SlotType slotType;

    public Image itemImage;
    public Item currentItem;

    public PlayerController playerController; 


    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("드롭 이벤트 발생");

        Slot dragSlot = DragSlot.instance.dragSlot;


        if (dragSlot == null || dragSlot.item == null)
        {
            Debug.LogWarning("드래그 슬롯이 비어있습니다.");
            return;
        }

        if (IsMatchSlotType(dragSlot.item.equipmentType))
        {
            currentItem = dragSlot.item;
            itemImage.sprite = dragSlot.item.itemImage;
            itemImage.color = Color.white;

            dragSlot.ClearSlot(); 

            Debug.Log($"장비 장착됨: {currentItem.itemName}");

            if (playerController != null)
            {
                switch (slotType)
                {
                    case SlotType.Weapon:
                        if (playerController.rightHandWeaponObject != null)
                            playerController.rightHandWeaponObject.SetActive(true);
                        break;

                    case SlotType.Helmet:
                        if (playerController.HelmetObject != null)
                            playerController.HelmetObject.SetActive(true);
                        break;

                    case SlotType.Armor:
                        if (playerController.jacketObject != null)
                            playerController.jacketObject.SetActive(true);
                        break;

                    case SlotType.Pants:
                        if (playerController.pantsObject != null)
                            playerController.pantsObject.SetActive(true);
                        break;

                    
                }
            }
        }
    
    }

    private bool IsMatchSlotType(Item.EquipmentType type)
    {
        return
            (slotType == SlotType.Helmet && type == Item.EquipmentType.Helmet) ||
            (slotType == SlotType.Armor && type == Item.EquipmentType.Armor) ||
            (slotType == SlotType.Pants && type == Item.EquipmentType.Pants) ||
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
     

        if (playerController != null)
        {
            switch (slotType)
            {
                case SlotType.Weapon:
                    if (playerController.rightHandWeaponObject != null)
                        playerController.rightHandWeaponObject.SetActive(false);
                    break;

                case SlotType.Helmet:
                    if (playerController.HelmetObject != null)
                        playerController.HelmetObject.SetActive(false);
                    break;
                case SlotType.Armor:
                    if (playerController.jacketObject != null)
                        playerController.jacketObject.SetActive(false);
                    break;
                case SlotType.Pants:
                    if (playerController.pantsObject != null)
                        playerController.pantsObject.SetActive(false);
                    break;

                    
            }
        }

        currentItem = null;
        itemImage.sprite = null;
        itemImage.color = new Color(1, 1, 1, 0);
    }
}

