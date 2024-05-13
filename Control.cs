using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// เพิ่ม RequireComponent เพื่อให้ต้องมี CharacterController เพื่อทำงาน
[RequireComponent(typeof(CharacterController))]
public class Control : MonoBehaviour
{
    // ประกาศตัวแปรสำหรับความเร็วและการหมุนของตัวละคร และ Transform ของจุดหมุนกล้อง
    public float speed;
    public float runSpeedMultiplier;
    public float rotationSpeed;
    public Transform cameraPivot;

    // ประกาศตัวแปรสำหรับ CharacterController และ Animator
    private CharacterController characterController;
    private Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        // กำหนดค่าตัวแปร characterController และ animator
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // ตรวจสอบว่าถ้าไม่ได้กำหนดจุดหมุนกล้องให้ใช้ตำแหน่งของกล้องหลัก
        if (cameraPivot == null)
        {
            cameraPivot = Camera.main.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // อ่านข้อมูลการกดปุ่มจากผู้เล่น
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // หาทิศทางของการเดินจากมุมกล้อง
        Vector3 movementDirection = cameraPivot.forward * verticalInput + cameraPivot.right * horizontalInput;
        movementDirection.y = 0f; // ไม่ต้องเคลื่อนที่ในแกน Y

        // หาหน่วยทิศทางของการเคลื่อนที่และมีขนาดเท่ากับความเร็ว
        movementDirection.Normalize();

        // เรียกใช้เมธอดเคลื่อนที่
        Move(movementDirection);
    }
    

    // Method to handle player movement
    private void Move(Vector3 direction)
    {
        // ถ้าทิศทางไม่เท่ากับ Vector3.zero
        if (direction != Vector3.zero)
        {
            // หาการหมุนของตัวละครจากทิศทางการเดิน
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // หาความเร็วของการเดินหรือวิ่ง
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? speed * runSpeedMultiplier : speed;

        // เคลื่อนที่ตัวละครตามทิศทางที่กำหนด
        characterController.Move(direction * currentSpeed * Time.deltaTime);

        // อัพเดทพารามิเตอร์ของอนิเมชัน
        animator.SetBool("isMove", direction != Vector3.zero);
        animator.SetBool("isRun", Input.GetKey(KeyCode.LeftShift));
        
    }
}
