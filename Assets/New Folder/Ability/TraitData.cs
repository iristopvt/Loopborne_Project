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

    // 플레이어용 스탯
    public int strModifier;
    public int dexModifier;
    public int intModifier;
}
