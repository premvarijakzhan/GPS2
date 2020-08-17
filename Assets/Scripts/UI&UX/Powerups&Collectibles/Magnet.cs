using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float magnetTimer;

    private int upgradeTime;
    private float duration;
    private string objectName;

    void Start()
    {
        PlayerPrefs.SetString("Object", ShopSystem.SS.transform.GetChild(0).GetChild(3).GetComponent<ShopItem>().gameObject.name);
        objectName = PlayerPrefs.GetString("Object");
        upgradeTime = PlayerPrefs.GetInt("UpgradeTime" + objectName);

        if (upgradeTime == 0)
        {
            magnetTimer = PlayerPrefs.GetFloat("InitialDuration" + objectName);
        }
        else
        {
            magnetTimer = PlayerPrefs.GetFloat("Duration" + objectName);
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

        if (Player.magnetActive)
        {
            Player.moveToPlayer = true;
            magnetTimer -= Time.deltaTime;

            if (magnetTimer <= 0f)
            {
                transform.gameObject.SetActive(false);

                if (upgradeTime == 0)
                    magnetTimer = PlayerPrefs.GetFloat("InitialDuration" + objectName);
                else
                    magnetTimer = PlayerPrefs.GetFloat("Duration" + objectName);

                Player.magnetActive = false;
                Player.moveToPlayer = false;
            }
        }
    }
}
