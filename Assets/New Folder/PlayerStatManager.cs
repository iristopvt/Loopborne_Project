using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
  
    public int level = 1;
    public int currentExp = 0;
    public int maxExp = 100;

    public int strength = 5;    
    public int dexterity = 5;    
    public int intelligence = 5;  

    public int bonusStatPoints = 0; 

  
    public int maxHp = 100;
    public int currentHp;

    void Start()
    {
        currentHp = maxHp;
    }

    void Update()
    {
        // 테스트: 키보드 L 누르면 경험치 50 추가
        if (Input.GetKeyDown(KeyCode.L))
        {
            GainExp(50);
        }
    }

    public void GainExp(int amount)
    {
        currentExp += amount;
        Debug.Log($"경험치 +{amount} (현재 경험치: {currentExp}/{maxExp})");

        while (currentExp >= maxExp)
        {
            currentExp -= maxExp;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        bonusStatPoints += 5;
        maxExp += 20;
        Debug.Log($"레벨업! 현재 레벨: {level}, 보너스 포인트: {bonusStatPoints}");
    }


    public void IncreaseStrength()
    {
        if (bonusStatPoints > 0)
        {
            strength++;
            bonusStatPoints--;
            Debug.Log($"힘 증가! 힘: {strength}, 남은 포인트: {bonusStatPoints}");
        }
    }

    public void IncreaseDexterity()
    {
        if (bonusStatPoints > 0)
        {
            dexterity++;
            bonusStatPoints--;
            Debug.Log($"민첩 증가! 민첩: {dexterity}, 남은 포인트: {bonusStatPoints}");
        }
    }

    public void IncreaseIntelligence()
    {
        if (bonusStatPoints > 0)
        {
            intelligence++;
            bonusStatPoints--;
            Debug.Log($"지능 증가! 지능: {intelligence}, 남은 포인트: {bonusStatPoints}");
        }
    }

  
    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        Debug.Log($"플레이어가 {damage}의 데미지를 받았습니다. 남은 HP: {currentHp}");

        if (currentHp <= 0)
        {
            Die();
        }
    }
    public void HealHp(int amount)
    {
        currentHp += amount;
        if (currentHp > maxHp)
            currentHp = maxHp;

        Debug.Log($"HP 회복: +{amount}, 현재 HP: {currentHp}/{maxHp}");
    }

    private void Die()
    {
        Debug.Log("플레이어 사망!");
      
    }
}
