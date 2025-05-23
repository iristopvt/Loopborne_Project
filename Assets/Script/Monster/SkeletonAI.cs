using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonAI : MonoBehaviour
{
    public float detectRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;

    private Transform player;
    private Animator animator;
    private NavMeshAgent agent;
    private float lastAttackTime;
    private MonsterStat monsterStat; 

    public BoxCollider attackTrigger;

    private bool isAttacking = false;


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
            agent.isStopped = true;
            animator.SetFloat("MoveSpeed", 0f);

            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0f;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }

            if (Time.time - lastAttackTime > attackCooldown)
            {
                animator.SetTrigger("Attack");
                lastAttackTime = Time.time;
            }
        }

        else if (distance <= detectRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);

            float speed = agent.velocity.magnitude;
            animator.SetFloat("MoveSpeed", speed);

            if (agent.desiredVelocity.magnitude > 0.1f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(agent.desiredVelocity.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }
        }
        else
        {
            agent.isStopped = true;
            animator.SetFloat("MoveSpeed", 0f);
        }


      


    }



    public void EnableWeaponCollider()
    {
        attackTrigger.enabled = true;
    }

    public void DisableWeaponCollider()
    {
        attackTrigger.enabled = false;
    }

    public void EndAttack()
    {
        isAttacking = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (attackTrigger.enabled && other.CompareTag("Player"))
        {
            PlayerStatManager playerStat = other.GetComponent<PlayerStatManager>();
            if (playerStat != null && monsterStat != null)
            {
                playerStat.TakeDamage(monsterStat.attackPower); 
            }
        }
    }
}
