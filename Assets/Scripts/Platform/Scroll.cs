using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float boosterTimer;

    void Start()
    {
        boosterTimer = PlayerPrefs.GetFloat("Duration" + PlayerPrefs.GetString("Object"));
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
                    boosterTimer = PlayerPrefs.GetFloat("Duration" + PlayerPrefs.GetString("Object"));
                    Player.boosterActive = false;
                }
            }
        }
    }
}
