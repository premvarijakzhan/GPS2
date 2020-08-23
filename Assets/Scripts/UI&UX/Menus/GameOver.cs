using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    private float highscore;
    private int currentAmount;
    private int newAmount;

    private float bestDistance;
    public TextMeshProUGUI bestRecordText;
    public TextMeshProUGUI lastRecordText;

    void Start()
    {
        if (PlayerPrefs.HasKey("BestRecord"))
            bestDistance = PlayerPrefs.GetFloat("BestRecord");

        if (PlayerPrefs.HasKey("highscore"))
            highscore = PlayerPrefs.GetFloat("highscore");

        currentAmount = PlayerPrefs.GetInt("coin", currentAmount);

        if (PlayerPrefs.HasKey("coin"))
            newAmount = PlayerPrefs.GetInt("coin");

        AudioManager.AM.playerSFX.clip = null;
        AudioManager.AM.SFX.clip = null;
    }

    void Update()
    {
        if (GameManagerScript.distance > bestDistance)
        {
            bestDistance = GameManagerScript.distance;
            PlayerPrefs.SetFloat("BestRecord", bestDistance);
        }

        bestRecordText.text = bestDistance.ToString("F0") + "m";
        lastRecordText.text = GameManagerScript.distance.ToString("F0") + "m";

        if (GameManagerScript.score > highscore)
        {
            highscore = GameManagerScript.score;
            PlayerPrefs.SetFloat("highscore", highscore);
        }

        newAmount = currentAmount + GameManagerScript.coin;
        PlayerPrefs.SetInt("coin", newAmount);
    }

    public void RunAgain()
    {
        AudioManager.AM.PlaySFX(AudioTag.SFX_TapButton);
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenu()
    {
        AudioManager.AM.PlaySFX(AudioTag.SFX_TapButton);
        SceneManager.LoadScene("MainMenu");
    }
}
