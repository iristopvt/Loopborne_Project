using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 2.5f; 
    public float runSpeed = 5f;
    public float rotationSpeed = 10f;

    private Rigidbody rb;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Animator animator;

    private bool isRunning = false;

    private PlayerStatManager statManager;

    public GameObject statUIPanel;
    private StatUIManager statUIManager;

    public BoxCollider PunchattackTrigger;
    public BoxCollider SwordAttackTrigger;

    private bool isStatUIOpen = false;

    public GameObject rightHandWeaponObject; 
    public GameObject jacketObject; 
    public GameObject pantsObject; 
    public GameObject HelmetObject; 

    private int attackCount = 0;
    private float lastAttackTime;
    public float comboResetTime = 1.0f;

    private bool isAttacking = false;

    private PlayerSkill shockwaveSkill;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        statManager = GetComponent<PlayerStatManager>();
        statUIManager = statUIPanel.GetComponent<StatUIManager>();

        statManager.ApplyTraits();
        shockwaveSkill = GetComponent<PlayerSkill>(); 


        ApplyDebuffEffects();

    }

    void Update()
    {
        HandleMovement();
        HandleAttack();
        HandleStatUI();
        HandleSkill(); 

    }

    void HandleMovement()
    {
        if (isAttacking) 
        {
            animator.SetFloat("MoveSpeed", 0f);
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        moveInput = new Vector3(moveX, 0f, moveZ).normalized;

        isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        moveVelocity = moveInput * currentSpeed;

        animator.SetFloat("MoveSpeed", moveVelocity.magnitude);
        animator.SetBool("IsRunning", isRunning);

        if (moveInput.magnitude > 0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

    
    }

    void HandleAttack()
    {

        if (Time.time - lastAttackTime > comboResetTime)
        {
            attackCount = 0;
            animator.SetInteger("AttackCount", attackCount);

            animator.ResetTrigger("Attack");
        }

        if (Input.GetButtonDown("Fire1"))
        {
            lastAttackTime = Time.time;

            if (rightHandWeaponObject != null && rightHandWeaponObject.activeSelf)
            {

                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(1); 

                if (!stateInfo.IsName("SwordAttack2")) 
                {
                    attackCount = Mathf.Clamp(attackCount, 0, 2);
                    animator.SetInteger("AttackCount", attackCount);
                    animator.SetTrigger("Attack");
                }
            }
            else
            {
                animator.SetTrigger("Punch");
            }
        }
       
     
    }

    void HandleStatUI()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isStatUIOpen = !isStatUIOpen;
            statUIPanel.SetActive(isStatUIOpen);

            if (isStatUIOpen)
            {
                statUIManager.UpdateStatUI();
            }
        }
    }


    void HandleSkill()
    {
        if (shockwaveSkill == null) return;

        if (Input.GetKeyDown(KeyCode.Alpha1) && shockwaveSkill.CanUseSkill)
        {
            shockwaveSkill.UseSkill();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && shockwaveSkill.CanUseSwordSkill && IsWeaponEquipped())
        {
            shockwaveSkill.UseSwordSkill();
        }
    }

    bool IsWeaponEquipped()
    {
        return rightHandWeaponObject != null && rightHandWeaponObject.activeSelf;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    public void EnableAttackTrigger()
    {
        PunchattackTrigger.enabled = true;
        PunchattackTrigger.GetComponent<PlayerAttackHitBox>().ResetHit();
    }

    public void DisableAttackTrigger()
    {
        PunchattackTrigger.enabled = false;
    }

    public void EnableSwordAttackTrigger()
    {
        SwordAttackTrigger.enabled = true;
        SwordAttackTrigger.GetComponent<PlayerAttackHitBox>().ResetHit();
    }

    public void DisableSwordAttackTrigger()
    {
        SwordAttackTrigger.enabled = false;
    }

    public void StartAttack()
    {
        isAttacking = true;
    }

    public void EndAttack()
    {
        isAttacking = false;
    }


    void ApplyDebuffEffects()
    {
        if (TraitManager.Instance == null) return;

        foreach (var debuff in TraitManager.Instance.selectedDebuffs)
        {
            if (debuff.traitName.Contains("속도감소") || debuff.traitName.ToLower().Contains("slow"))
            {
                walkSpeed *= 0.5f;
                runSpeed *= 0.5f;
              
            }
        }
    }
}

