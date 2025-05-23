using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropItemData
{
    public GameObject itemPrefab;          
    [Range(0f, 1f)] public float dropChance; 
}