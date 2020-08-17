using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float boosterTimer;

    private int upgradeTime;
    private float duration;
    private string objectName;

    void Start()
    {
        PlayerPrefs.SetString("Object", ShopSystem.SS.transform.GetChild(0).GetChild(4).GetComponent<ShopItem>().gameObject.name);
        objectName = PlayerPrefs.GetString("Object");
        upgradeTime = PlayerPrefs.GetInt("UpgradeTime" + objectName);

        if (upgradeTime == 0)
        {
            boosterTimer = PlayerPrefs.GetFloat("InitialDuration" + objectName);
        }
        else
        {
            boosterTimer = PlayerPrefs.GetFloat("Duration" + objectName);
        }
    }

    void Update()
    {
        duration = ShopSystem.SS.transform.GetChild(0).GetChild(4).GetComponent<ShopItem>().duration;

        if (upgradeTime == 0)
        {
            PlayerPrefs.SetFloat("InitialDuration" + objectName, duration);
        }
        else
        {
            PlayerPrefs.SetFloat("Duration" + objectName, duration);
        }
    }

    void FixedUpdate()
    {
        if (!Player.isDead)
        {
            if (!Player.boosterActive)
            {
                this.transform.position += Player.player.transform.forward * -GameManagerScript.defaultSpeed;
            }
            else
            {
                this.transform.position += Player.player.transform.forward * -GameManagerScript.boostSpeed;
                boosterTimer -= Time.deltaTime;

                if (boosterTimer <= 0f)
                {
                    if (upgradeTime == 0)
                        boosterTimer = PlayerPrefs.GetFloat("InitialDuration" + objectName);
                    else
                        boosterTimer = PlayerPrefs.GetFloat("Duration" + objectName);

                    Player.boosterActive = false;
                }
            }
        }
    }
}
