using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float rotationSpeed = 10f;  // 회전 속도 추가

    private Rigidbody rb;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Animator animator;

    private bool isRunning = false;


    private PlayerStatManager statManager;

    public GameObject statUIPanel; 
    private StatUIManager statUIManager;

    private bool isStatUIOpen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        statManager = GetComponent<PlayerStatManager>();

        statUIManager = statUIPanel.GetComponent<StatUIManager>();

    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        moveInput = new Vector3(moveX, 0f, moveZ).normalized;

       
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        moveVelocity = moveInput * currentSpeed;

     
        animator.SetFloat("MoveSpeed", moveVelocity.magnitude);


        animator.SetBool("IsRunning", isRunning);

        if (moveInput.magnitude > 0f) 
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput); 
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); 
        }

        
        if (Input.GetButtonDown("Fire1")) 
        {
            animator.SetTrigger("Punch");
            
        }

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

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}


