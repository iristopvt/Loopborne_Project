using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    [Header("기본 스탯")]
    public int maxHp = 100;
    public int currentHp;

    public int maxMp = 50;
    public int currentMp;

    public int attackPower = 10;

    [Header("드롭 보상")]
    public int MonsterDropExp = 20;

    private Animator animator;
    private bool isDead = false;

    void Awake()
    {
        animator = GetComponent<Animator>();

        if (currentHp == 0) currentHp = maxHp;
        if (currentMp == 0) currentMp = maxMp;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        GetComponent<MonsterHpUI>()?.ShowHpBar();

        currentHp -= damage;
        Debug.Log($"{gameObject.name}이(가) {damage}의 데미지를 입었습니다. 남은 체력: {currentHp}");

        if (currentHp > 0)
        {
            animator.SetInteger("DamageType", 1); 
            animator.SetTrigger("Damage");
        }
        else
        {
            Die();
        }
    }

    public void UseMana(int amount)
    {
        currentMp -= amount;
        if (currentMp < 0) currentMp = 0;
        Debug.Log($"{gameObject.name}이(가) 마나 {amount} 사용. 남은 마나: {currentMp}");
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.SetBool("isDead", true);

        Debug.Log($"{gameObject.name} 사망!");

        GiveExpToPlayer();


        Destroy(gameObject, 1.5f);
    }


    void GiveExpToPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerStatManager statManager = player.GetComponent<PlayerStatManager>();
            if (statManager != null)
            {
                statManager.GainExp(MonsterDropExp);
                Debug.Log($"플레이어가 {MonsterDropExp} 경험치를 얻었습니다.");
            }
        }
    }
}
