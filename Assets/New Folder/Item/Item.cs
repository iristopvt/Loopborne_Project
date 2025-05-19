using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;             
    [TextArea]
    public string itemDesc;             
    public Sprite itemImage;            
    public GameObject itemPrefab;      
    public ItemType itemType;           
    public EquipmentType equipmentType; 


    public int damage;
    public float attackSpeed;

  
    public ConsumableType consumableType; 
    public int amount;                   

    public enum ItemType
    {
        Equipment,
        Consumable,
        Ingredient,
        ETC
    }

    public enum EquipmentType
    {
        None,
        Weapon,
        Armor,
        Helmet,
        Pants
    }

    public enum ConsumableType
    {
        None,
        HealthPotion,
        ManaPotion
    }
}