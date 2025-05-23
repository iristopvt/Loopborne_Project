using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    [Header("기본 스탯")]
    public int maxHp = 100;
    public int curHp;

    protected virtual void Awake()
    {
        if (curHp == 0) curHp = maxHp;
    }

    public virtual void TakeDamage(int damage)
    {
        curHp -= damage;

        OnDamaged(damage);  

        if (curHp <= 0)
        {
            Die();
        }
    }

    protected virtual void OnDamaged(int damage) { }

    public void Heal(int amount)
    {
        curHp = Mathf.Min(curHp + amount, maxHp);
    }

    protected abstract void Die();
}