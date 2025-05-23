using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAIBase : MonoBehaviour
{
    [Header("AI 설정")]
    public float detectionRange = 10f;
    public float attackRange = 1.5f;
    public float chaseSpeed = 3.5f;
    public float attackCooldown = 1.5f;
    public BoxCollider attackTrigger;

    protected Transform player;
    protected NavMeshAgent agent;
    protected Animator animator;
    protected MonsterStat monsterStat;

    private float lastAttackTime = 0f;

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        monsterStat = GetComponent<MonsterStat>();
        attackTrigger.enabled = false;
    }

    protected virtual void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            AttackRoutine();
        }
        else if (distance <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            Idle();
        }
    }

    protected virtual void AttackRoutine()
    {
        agent.isStopped = true;
        RotateToPlayer();

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            animator.SetTrigger("Attack");
            lastAttackTime = Time.time;
        }
    }

    protected virtual void ChasePlayer()
    {
        agent.isStopped = false;
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
        animator.SetFloat("MoveSpeed", agent.velocity.magnitude);
        RotateTo(agent.desiredVelocity);
    }

    protected virtual void Idle()
    {
        agent.isStopped = true;
        animator.SetFloat("MoveSpeed", 0f);
    }

    protected void RotateTo(Vector3 direction)
    {
        if (direction.magnitude > 0.1f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }
    }

    protected void RotateToPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f;
        RotateTo(direction);
    }

    public void EnableAttackTrigger() => attackTrigger.enabled = true;
    public void DisableAttackTrigger() => attackTrigger.enabled = false;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (attackTrigger.enabled && other.CompareTag("Player"))
        {
            PlayerStatManager playerStat = other.GetComponent<PlayerStatManager>();
            if (playerStat != null && monsterStat != null)
            {
                playerStat.TakeDamage(monsterStat.attackPower);
                Debug.Log($"플레이어가 {gameObject.name}에게 데미지를 입었습니다. 남은 체력: {playerStat.curHp}");
            }
        }
    }
}
