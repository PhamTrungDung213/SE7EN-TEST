using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 12f;
    private Rigidbody rb;
    private Animator anim;
    private Vector3 moveDirection;
    [SerializeField] private float nearDis = 1.2f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(h, 0f, v).normalized;

        if (moveDirection.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (anim!= null)
        {
            anim.SetFloat("Blend", moveDirection.magnitude);
        }

        CheckBallNear();
    }

    void FixedUpdate()
    {
        Vector3 targetVelocity = moveDirection * moveSpeed;
        rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);
    }

    private void OnCollisionEnter(Collision other) 
    {
        anim.SetBool("isGrounded",true); 
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;  
    }

    private void CheckBallNear()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        bool isNear = false;

        foreach(GameObject b in balls)
        {
            if(Vector3.Distance(transform.position, b.transform.position) <= nearDis)
            {
                isNear = true;
            }
        }

        if (UI.Instance != null)
        {
            UI.Instance.ShowKickButton(isNear);
        }
    }
}
