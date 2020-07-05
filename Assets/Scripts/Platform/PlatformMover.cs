using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public float speed = 5f;
    public float despawnDistance = -100f;

    void Update()
    {
        if (!GameManagerScript.GMS.isGameOver)
            transform.position += -transform.forward * speed * Time.deltaTime;

        if (transform.position.z <= despawnDistance)
        {
            Destroy(gameObject);
        }
    }
}
