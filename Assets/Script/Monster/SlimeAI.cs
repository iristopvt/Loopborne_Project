using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;

public class SlimeAI : MonoBehaviour
{
    public float detectionRange = 10f;
    public float attackRange = 1.5f;
    public float chaseSpeed = 3.5f;
    public float attackCooldown = 1.5f;

    public BoxCollider attackTrigger;

    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private MonsterStat monsterStat;  

    private float lastAttackTime = 0f;





    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        monsterStat = GetComponent<MonsterStat>(); 

        attackTrigger.enabled = false;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                StartAttack();
                lastAttackTime = Time.time;
            }
            agent.isStopped = true;
        }
        else if (distance <= detectionRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);

          
            if (agent.desiredVelocity.magnitude > 0.1f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(agent.desiredVelocity.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }

        }
        else
        {
            agent.isStopped = true;
        }
    }

    void StartAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void EnableAttackTrigger()
    {
        attackTrigger.enabled = true;
    }

    public void DisableAttackTrigger()
    {
        attackTrigger.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (attackTrigger.enabled && other.CompareTag("Player"))
        {
            PlayerStatManager playerStat = other.GetComponent<PlayerStatManager>();
            if (playerStat != null && monsterStat != null)
            {
                playerStat.TakeDamage(monsterStat.attackPower); 
                Debug.Log("플레이어가 슬라임에게 데미지를 입었습니다. 남은 체력: " + playerStat.curHp);
            }
        }
    }
}