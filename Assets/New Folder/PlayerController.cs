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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        moveInput = new Vector3(moveX, 0f, moveZ).normalized;

        // 달리기 입력 체크
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        // 움직이는 속도 설정
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        moveVelocity = moveInput * currentSpeed;

        // 애니메이션 MoveSpeed 설정
        animator.SetFloat("MoveSpeed", moveInput.magnitude);

        // 애니메이션 isRunning 설정
        animator.SetBool("IsRunning", isRunning);

        // 이동 방향으로 회전
        if (moveInput.magnitude > 0f) // 이동 입력이 있을 때만 회전
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);  // 이동 방향을 바라보게 회전
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);  // 회전 보간
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}


//public float MoveSpeed = 5f;

//private Vector3 moveInput;

//void Update()
//{
//    // 입력 받기
//    float moveX = Input.GetAxisRaw("Horizontal");
//    float moveZ = Input.GetAxisRaw("Vertical");

//    moveInput = new Vector3(moveX, 0f, moveZ).normalized;

//    // 위치 이동
//    transform.position += moveInput * MoveSpeed * Time.deltaTime;

//    // 방향 회전
//    if (moveInput != Vector3.zero)
//    {
//        Quaternion targetRotation = Quaternion.LookRotation(moveInput);
//        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
//    }
//}