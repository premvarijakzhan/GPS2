using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSystem : MonoBehaviour
{
    public SceneManagerScript SMS;

    public GameObject[] upgradeSlots;

    public TextMeshProUGUI descriptionText;
    public Button upgradeBtn;
    private TextMeshProUGUI buttonText;

    private const int MAXIMUM_SLOT = 3;
    private string objectName;
    private int upgradeSlot;
    private int cost = 100;
    private float duration = 5;

    void Start()
    {
        buttonText = upgradeBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        PlayerPrefs.SetString("Object", gameObject.name);
        objectName = PlayerPrefs.GetString("Object"); 

        if (PlayerPrefs.HasKey("Cost" + objectName))
        {
            cost = PlayerPrefs.GetInt("Cost" + objectName);
            buttonText.text = cost.ToString();
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

        if (SMS.currentAmount < cost)
            upgradeBtn.interactable = false;
        else
            upgradeBtn.interactable = true;
    }

    public void Upgrade()
    {
        if (SMS.currentAmount >= cost)
        {
            upgradeSlots[upgradeSlot].SetActive(true);
            upgradeSlot++;      
            PlayerPrefs.SetInt("Upgrade" + objectName, upgradeSlot);

            SMS.newAmount = SMS.currentAmount - cost;
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
