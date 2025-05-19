using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveSkill : MonoBehaviour
{

    [Header("Shockwave Skill")]
    public float skillRange = 3f;
    public float skillWidth = 2f;
    public int damage = 20;
    public float cooldown = 5f;
    public GameObject shockwaveEffect;

    [Header(" Sword Skill")]
    public float swordSkillRange = 4f;
    public float swordSkillWidth = 2f;
    public int swordSkillDamage = 40;
    public float swordSkillCooldown = 7f;
    public GameObject swordSkillEffect;

    [Header(" Dependencies")]
    public LayerMask enemyLayer;

    private float lastUseTime;
    private float lastSwordSkillTime;
    private Animator animator;

    public bool CanUseSkill => Time.time > lastUseTime + cooldown;
    public bool CanUseSwordSkill => Time.time > lastSwordSkillTime + swordSkillCooldown;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void UseSkill()
    {
        lastUseTime = Time.time;
        if (animator != null) animator.SetTrigger("Shockwave");

        if (shockwaveEffect != null)
        {
            Vector3 effectPos = transform.position + transform.forward * 1.5f + Vector3.up * 0.5f;
            Destroy(Instantiate(shockwaveEffect, effectPos, transform.rotation), 1f);
        }

        Vector3 center = transform.position + transform.forward * skillRange * 0.5f;
        Vector3 halfExtents = new Vector3(skillWidth / 2f, 1f, skillRange / 2f);
        Collider[] hits = Physics.OverlapBox(center, halfExtents, transform.rotation, enemyLayer);

        foreach (var col in hits)
        {
            if (col.CompareTag("Monster") && col.TryGetComponent(out MonsterStat monster))
            {
                monster.TakeDamage(damage);
                Debug.Log(" 충격파 데미지 입힘!");
            }
        }
    }

    public void UseSwordSkill()
    {
        lastSwordSkillTime = Time.time;
        if (animator != null) animator.SetTrigger("SwordSkill");

        if (swordSkillEffect != null)
        {
            Vector3 effectPos = transform.position + transform.forward * 1.5f + Vector3.up * 0.5f;
            Destroy(Instantiate(swordSkillEffect, effectPos, transform.rotation), 1.5f);
        }

        Vector3 center = transform.position + transform.forward * swordSkillRange * 0.5f;
        Vector3 halfExtents = new Vector3(swordSkillWidth / 2f, 1f, swordSkillRange / 2f);
        Collider[] hits = Physics.OverlapBox(center, halfExtents, transform.rotation, enemyLayer);

        foreach (var col in hits)
        {
            if (col.CompareTag("Monster") && col.TryGetComponent(out MonsterStat monster))
            {
                monster.TakeDamage(swordSkillDamage);
                Debug.Log(" 검 스킬 데미지 입힘!");
            }
        }
    }


}
