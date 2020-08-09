using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float magnetTimer;

    void Start()
    {
        magnetTimer = PlayerPrefs.GetFloat("Duration" + PlayerPrefs.GetString("Object"));
    }

    void Update()
    {
        if (Player.magnetActive)
        {
            Player.moveToPlayer = true;
            magnetTimer -= Time.deltaTime;

            if (magnetTimer <= 0f)
            {
                transform.gameObject.SetActive(false);
                magnetTimer = PlayerPrefs.GetFloat("Duration" + PlayerPrefs.GetString("Object"));
                Player.magnetActive = false;
                Player.moveToPlayer = false;
            }
        }
    }
}
