using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject buttons;
    public GameObject pauseButton;
    public bool isPaused = false;
    public bool showCountDown = false;
    public int countDown = 3;
    public float time = 1f;
    public TextMeshProUGUI countDownDisplay;

    public DrawTrails dt;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
        countDownDisplay.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        AudioManager.AM.PlaySFX(AudioTag.SFX_TapButton);
        pauseButton.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        AudioManager.AM.StopPlayerSFX();
    }

    public void Resume()
    {
        AudioManager.AM.PlaySFX(AudioTag.SFX_TapButton);
        countDownDisplay.gameObject.SetActive(true);
        pauseMenuUI.SetActive(false);
        StartCoroutine(GetReady());
    }

    public void MainMenu()
    {
        AudioManager.AM.PlaySFX(AudioTag.SFX_TapButton);
        SceneManager.LoadScene("MainMenu");
        GameManagerScript.coin = 0;
        GameManagerScript.score = 0;
        GameManagerScript.distance = 0;
        SymbolManager.SM.isComplete = false;
        SymbolManager.SM.turnRight = false;
        SymbolManager.SM.turnLeft = false;
        SymbolManager.SM.canJump = false;
        Time.timeScale = 1f;
    }

    void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            isPaused = true;
            Time.timeScale = 0f;
            dt.DestroyTrail();
            AudioManager.AM.StopPlayerSFX();
        }
        else
        {
            pauseMenuUI.SetActive(true);
            pauseButton.SetActive(false);
            AudioManager.AM.StopPlayerSFX();
        }
    }

    IEnumerator GetReady()
    {
        showCountDown = true;

        while (countDown > 0)
        {
            countDownDisplay.text = countDown.ToString();
            yield return StartCoroutine(WaitForRealSeconds(time));
            countDown--;
        }

        countDownDisplay.text = "GO!";
        yield return StartCoroutine(WaitForRealSeconds(time));
        countDownDisplay.gameObject.SetActive(false);
        showCountDown = false;
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        countDown = 3;
        AudioManager.AM.playerSFX.Play();
    }

    IEnumerator WaitForRealSeconds(float waitTime)
    {
        float endTime = Time.realtimeSinceStartup + waitTime;

        while (Time.realtimeSinceStartup < endTime)
            yield return null;
    }
}
