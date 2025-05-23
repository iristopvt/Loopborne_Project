using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterStat : CharacterStats
{
    public int maxMp = 50;
    public int currentMp;
    public int attackPower = 10;
    public int MonsterDropExp = 20;
    public List<DropItemData> dropItems;

    private Animator animator;
    private bool isDead = false;

    [Header("드랍 아이템")]
    public GameObject dropPrefab;

    public Item dropItem;        
    [Range(0f, 1f)]
    public float dropChance = 1f;  

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        if (currentMp == 0) currentMp = maxMp;
    }

    protected override void OnDamaged(int damage)
    {
        if (isDead) return;

        GetComponent<MonsterHpUI>()?.ShowHpBar();

        animator.SetInteger("DamageType", 1);
        animator.SetTrigger("Damage");
    }

    protected override void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.SetBool("isDead", true);

        DropItem();
        GiveExpToPlayer();
        Destroy(gameObject, 1.5f);
    }

    public void UseMana(int amount)
    {
        currentMp -= amount;
        if (currentMp < 0) currentMp = 0;
        Debug.Log($"{gameObject.name}이(가) 마나 {amount} 사용. 남은 마나: {currentMp}");
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
            }
        }
    }
        void DropItem()
    { if (dropPrefab != null && Random.value <= dropChance)
        {
            Instantiate(dropPrefab);
            Debug.Log("아이템 드랍됨!");
        }
        foreach (var drop in dropItems)
        {
            if (Random.value <= drop.dropChance)
            {
                Instantiate(drop.itemPrefab);
             
            }
        }
    }
}
    
