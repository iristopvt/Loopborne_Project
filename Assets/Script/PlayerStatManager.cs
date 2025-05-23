using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : CharacterStats
{
    public int level = 1;
    public int curExp = 0;
    public int maxExp = 100;

    public int str = 5;
    public int dex = 5;
    public int @int = 5;

    public int bonusStatPoints = 0;

    void Start()
    {
        curHp = maxHp;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GainExp(50);
        }
    }

    public void GainExp(int amount)
    {
        curExp += amount;

        while (curExp >= maxExp)
        {
            curExp -= maxExp;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        bonusStatPoints += 5;
        maxExp += 20;
    }

    public void IncreaseStrength()
    {
        if (bonusStatPoints > 0)
        {
            str++;
            bonusStatPoints--;

        }
    }
    public void HealHp(int amount)
    {
        curHp += amount;
        if (curHp > maxHp)
            curHp = maxHp;

    }

    public void IncreaseDexterity()
    {
        if (bonusStatPoints > 0)
        {
            dex++;
            bonusStatPoints--;

        }
    }

    public void IncreaseIntelligence()
    {
        if (bonusStatPoints > 0)
        {
            @int++;
            bonusStatPoints--;

        }
    }

    protected override void Die()
    {
        Debug.Log("플레이어 사망");

    }

    public void ApplyTraits()
    {
        if (TraitManager.Instance == null || TraitManager.Instance.selectedTraits.Count == 0)
        {
            return;
        }

        foreach (var trait in TraitManager.Instance.selectedTraits)
        {
            str += trait.strModifier;
            dex += trait.dexModifier;
            @int += trait.intModifier;
            maxHp += Mathf.RoundToInt(trait.hpModifier);
        }

        curHp = maxHp;

    }
}
