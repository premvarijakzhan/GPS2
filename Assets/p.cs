using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            transform.Rotate(Vector3.up * 90 * Time.deltaTime);
    }
}
