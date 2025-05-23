using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Trait/Create New Trait")]
public class TraitData : ScriptableObject
{
    public string traitName;
    public string description;
    public int pointCost; 

    public float attackModifier;
    public float hpModifier;
    public float skillRangeModifier;

   
    public int strModifier;
    public int dexModifier;
    public int intModifier;
}
