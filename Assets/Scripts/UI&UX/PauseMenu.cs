using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject buttons;
    public GameObject pauseButton;
    public bool isPaused = false;
    public bool showCountDown = false;
    public int countDown = 3;
    public float time = 1f;
    public Text countDownDisplay;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        countDownDisplay.gameObject.SetActive(false);
    }

    public void Pause()
    {
        pauseButton.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        countDownDisplay.gameObject.SetActive(true);
        pauseMenuUI.SetActive(false);
        StartCoroutine(GetReady());
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            isPaused = true;
            Time.timeScale = 0f;
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
    }

    IEnumerator WaitForRealSeconds(float waitTime)
    {
        float endTime = Time.realtimeSinceStartup + waitTime;

        while (Time.realtimeSinceStartup < endTime)
            yield return null;
    }
}
