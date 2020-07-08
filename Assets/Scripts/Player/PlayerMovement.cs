using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;

    public float jumpForce;
    public bool isGrounded = false;
    public bool isJumping = false;

    private CameraController cc;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GameObject.Find("TrackingCam").GetComponent<CameraController>();
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > cc.animationDuration)
        {
            Vector3 movement = new Vector3(Input.acceleration.x, 0f, 0f) * speed;
            movement.y = rb.velocity.y;
            rb.velocity = movement;
        }

        if (isGrounded)
        {
            /*if (SymbolManager.SM.canJump && !isJumping)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }*/
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
        isJumping = true;
        //SymbolManager.SM.canJump = false;
    }
}
