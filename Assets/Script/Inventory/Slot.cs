using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler,IDropHandler
{

    private Vector3 originPos;

    public Item item; 
    public int itemCount; 
    public Image itemImage; 


  
    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;



    void Start()
    {
        originPos = transform.position;
    }

   
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

 
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        if (item.itemType != Item.ItemType.Equipment)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }

        SetColor(1);
    }

 
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

  
    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                if (item.itemType == Item.ItemType.Consumable)
                {
                    PlayerStatManager playerStat = FindObjectOfType<PlayerStatManager>();
                    if (playerStat != null)
                    {
                        switch (item.consumableType)
                        {
                            case Item.ConsumableType.HealthPotion:
                                playerStat.HealHp(item.amount);
                                break;
                            case Item.ConsumableType.ManaPotion:
                                // 추후 Mana 관련 기능 추가 시 
                                break;
                        }

                        Debug.Log($"{item.itemName} 사용됨 → {item.amount} 회복");
                        SetSlotCount(-1);
                    }
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);

            DragSlot.instance.transform.position = eventData.position;


        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }

    }


    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {

       
        if (DragSlot.instance.dragSlot == null && DragSlot.instance.dragEquipSlot != null)
        {
         
            Item fromEquipItem = DragSlot.instance.dragEquipSlot.currentItem;

            if (fromEquipItem != null)
            {
                AddItem(fromEquipItem); 
                DragSlot.instance.dragEquipSlot.ClearEquipSlot(); 
            }
        }
        else if (DragSlot.instance.dragSlot != null)
        {
          
            ChangeSlot();
        }
    }

    private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if (_tempItem != null)
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        else
            DragSlot.instance.dragSlot.ClearSlot();
    }
}
