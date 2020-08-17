using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float shieldTimer;

    private int upgradeTime;
    private float duration;
    private string objectName;

    void Start()
    {
        PlayerPrefs.SetString("Object", ShopSystem.SS.transform.GetChild(0).GetChild(5).GetComponent<ShopItem>().gameObject.name);
        objectName = PlayerPrefs.GetString("Object");
        upgradeTime = PlayerPrefs.GetInt("UpgradeTime" + objectName);

        if (upgradeTime == 0)
        {
            shieldTimer = PlayerPrefs.GetFloat("InitialDuration" + objectName);
        }
        else
        {
            shieldTimer = PlayerPrefs.GetFloat("Duration" + objectName);
        }
    }

    void Update()
    {
        duration = ShopSystem.SS.transform.GetChild(0).GetChild(3).GetComponent<ShopItem>().duration;

        if (upgradeTime == 0)
        {
            PlayerPrefs.SetFloat("InitialDuration" + objectName, duration);
        }
        else
        {
            PlayerPrefs.SetFloat("Duration" + objectName, duration);
        }

        if (Player.shieldActive)
        {
            shieldTimer -= Time.deltaTime;

            if (shieldTimer <= 0f)
            {
                transform.gameObject.SetActive(false);

                if (upgradeTime == 0)
                    shieldTimer = PlayerPrefs.GetFloat("InitialDuration" + objectName);
                else
                    shieldTimer = PlayerPrefs.GetFloat("Duration" + objectName);

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
