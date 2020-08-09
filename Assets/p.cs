using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Translate(Vector3.right * 5 * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(Vector3.right * -5 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
            transform.Rotate(Vector3.up * 90);
    }
}
