using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;

    [Header("장비 오브젝트")]
    public GameObject helmetObject;
    public GameObject armorObject;
    public GameObject pantsObject;
    public GameObject weaponObject;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ActivateEquipment(Item.EquipmentType type, string prefabName)
    {
        switch (type)
        {
            case Item.EquipmentType.Helmet:
                if (helmetObject != null)
                    helmetObject.SetActive(true);
                break;

            case Item.EquipmentType.Armor:
                if (armorObject != null)
                    armorObject.SetActive(true);
                break;
            case Item.EquipmentType.Pants:
                if (pantsObject != null)
                    pantsObject.SetActive(true);
                break;

            case Item.EquipmentType.Weapon:
                if (weaponObject != null)
                    weaponObject.SetActive(true);
                break;

            default:
                break;
        }

    }
}
