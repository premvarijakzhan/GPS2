using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    public float speed;

    void Update()
    {
        if (Player.isDead)
        {
            speed = 2f;
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        else
        {
            speed = 0f;
        }
    }
}
