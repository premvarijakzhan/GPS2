using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float shieldTimer;

    void Start()
    {
        shieldTimer = PlayerPrefs.GetFloat("Duration" + PlayerPrefs.GetString("Object"));
    }

    void Update()
    {
        if (Player.shieldActive)
        {
            shieldTimer -= Time.deltaTime;

            if (shieldTimer <= 0f)
            {
                transform.gameObject.SetActive(false);
                shieldTimer = PlayerPrefs.GetFloat("Duration" + PlayerPrefs.GetString("Object"));
                Player.shieldActive = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other is SphereCollider && other.gameObject.CompareTag("platformTSection"))
            return;
    }
}
