using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float jumpForce;

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
            Vector3 movement = new Vector3(Input.acceleration.x, 0f, 0f);
            rb.velocity = movement * speed;
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
