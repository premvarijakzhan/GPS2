using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    private SceneManagerScript SMS;

    public GameObject[] upgradeSlots;

    public TextMeshProUGUI descriptionText;
    public Button upgradeBtn;
    public TextMeshProUGUI buttonText;

    private const int MAXIMUM_SLOT = 3;
    private string objectName;
    private int upgradeSlot;
    private int upgradeTime;
    private int cost = 100;
    public float duration = 5;

    void Start()
    {
        SMS = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();

        PlayerPrefs.SetString("Object", gameObject.name);
        objectName = PlayerPrefs.GetString("Object");

        if (PlayerPrefs.HasKey("Cost" + objectName))
        {
            cost = PlayerPrefs.GetInt("Cost" + objectName);
            buttonText.text = cost.ToString();
        }

        upgradeTime = PlayerPrefs.GetInt("UpgradeTime" + objectName);

        if (upgradeTime == 0)
        {
            PlayerPrefs.SetFloat("InitialDuration" + objectName, duration);

            if (SMS.newAmount >= cost)
                upgradeBtn.interactable = true;
            else if (SMS.newAmount < cost)
                upgradeBtn.interactable = false;
        }

        if (PlayerPrefs.HasKey("Duration" + objectName))
        {
            duration = PlayerPrefs.GetFloat("Duration" + objectName);
            descriptionText.text = "Increase duration to " + duration.ToString("F0") + " seconds";
        }

        if (PlayerPrefs.HasKey("Upgrade" + objectName))
        {
            upgradeSlot = PlayerPrefs.GetInt("Upgrade" + objectName);

            for (int i = 0; i < upgradeSlot; i++)
            {
                upgradeSlots[i].SetActive(true);

                if (upgradeSlot > MAXIMUM_SLOT - 1)
                {
                    upgradeBtn.interactable = false;
                    upgradeBtn.enabled = false;
                    buttonText.text = "MAX";
                }
            }
        }
    }

    void Update()
    {
        if (upgradeSlot > MAXIMUM_SLOT - 1)
        {
            upgradeBtn.interactable = false;
            upgradeBtn.enabled = false;
            buttonText.text = "MAX";
        }

        if (SMS.newAmount >= cost)
            upgradeBtn.interactable = true;
        else if (SMS.newAmount < cost)
            upgradeBtn.interactable = false;
    }

    public void Upgrade()
    {
        PlayerPrefs.SetInt("UpgradeTime" + objectName, -1);

        if (SMS.newAmount >= cost)
        {
            upgradeSlots[upgradeSlot].SetActive(true);
            upgradeSlot++;      
            PlayerPrefs.SetInt("Upgrade" + objectName, upgradeSlot);

            SMS.newAmount -= cost;
            PlayerPrefs.SetInt("coin", SMS.newAmount);
            SMS.coinText.text = SMS.newAmount.ToString();
        }

        if (upgradeSlot < MAXIMUM_SLOT - 1)
            cost += 50;
        else
            cost += 100;
        
        PlayerPrefs.SetInt("Cost" + objectName, cost);
        buttonText.text = cost.ToString();

        if (upgradeSlot < MAXIMUM_SLOT)
            duration += 2;
        else
            duration += 3;

        PlayerPrefs.SetFloat("Duration" + objectName, duration);
        descriptionText.text = "Increase duration to " + duration.ToString("F0") + " seconds";
    }
}
