using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{


    [SerializeField]
    private float range;

    private bool pickupActivated = false; 

    private RaycastHit hitInfo; 


    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Text actionText;
    [SerializeField]
    private Inventory theInventory;

    private Collider targetItem;

    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CanPickUp()
    {
     

        if (pickupActivated && targetItem != null)
        {
            ItemPickUp itemPickup = targetItem.GetComponent<ItemPickUp>();
            Debug.Log(itemPickup.item.itemName + " 획득했습니다");
            theInventory.AcquireItem(itemPickup.item);
            Destroy(targetItem.gameObject);
            InfoDisappear();
        }
    }

    private void CheckItem()
    {
        

        Collider[] hits = Physics.OverlapSphere(transform.position, range, layerMask);

        foreach (Collider col in hits)
        {
            if (col.CompareTag("Item"))
            {
                targetItem = col;
                ItemInfoAppear();
                return;
            }
        }

        targetItem = null;
        InfoDisappear();

      
    }

    private void ItemInfoAppear()
    {
       
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = targetItem.GetComponent<ItemPickUp>().item.itemName + " 획득 <color=yellow>(E)</color>";
    }
    private void InfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
}
