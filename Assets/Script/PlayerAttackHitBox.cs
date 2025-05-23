using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHitBox : MonoBehaviour
{
    public int damage = 20;
    private bool hasHit = false;

    public void ResetHit()
    {
        hasHit = false; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit) return; 

        if (other.CompareTag("Monster"))
        {
            MonsterStat monster = other.GetComponent<MonsterStat>();
            if (monster != null)
            {
                monster.TakeDamage(damage);
                hasHit = true;
            }
        }
    }

}