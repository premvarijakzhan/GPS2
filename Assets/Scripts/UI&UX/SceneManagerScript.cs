using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    private GameObject buttons;
    private GameObject settingsPanel;
    private GameObject storePanel;
    private GameObject creditsPanel;
    private GameObject confirmationPanel;

    private int highScoreCount;
    public Text highScoreText;

    private Scene scene;
    private string sceneName;

    public PauseMenu pm;
    
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;

        if (sceneName == "MainMenu")
        {
            buttons = GameObject.Find("MainMenuCanvas").transform.GetChild(2).gameObject;
            settingsPanel = GameObject.Find("MainMenuCanvas").transform.GetChild(3).gameObject;
            storePanel = GameObject.Find("MainMenuCanvas").transform.GetChild(4).gameObject;
            creditsPanel = GameObject.Find("MainMenuCanvas").transform.GetChild(5).gameObject;
            confirmationPanel = GameObject.Find("MainMenuCanvas").transform.GetChild(6).gameObject;
            
            settingsPanel.SetActive(false);
            storePanel.SetActive(false);
            creditsPanel.SetActive(false);
            confirmationPanel.SetActive(false);

            if (PlayerPrefs.HasKey("highscore"))
                highScoreCount = PlayerPrefs.GetInt("highscore");
        }        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }

        if (scene.name == "MainMenu")
        {
            if (GameManagerScript.score > highScoreCount)
            {
                highScoreCount = GameManagerScript.score;
                PlayerPrefs.SetInt("highscore", highScoreCount);
            }

            highScoreText.text = highScoreCount.ToString();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
        buttons.SetActive(false);
    }

    public void Store()
    {
        storePanel.SetActive(true);
        buttons.SetActive(false);
    }

    public void Credits()
    {
        creditsPanel.SetActive(true);
        buttons.SetActive(false);
    }

    public void Back()
    {
        if (sceneName == "MainMenu")
        {
            if (confirmationPanel)
            {
                confirmationPanel.SetActive(true);
                buttons.SetActive(false);
            }

            if (settingsPanel != null || storePanel != null || creditsPanel != null)
            {
                if (settingsPanel.activeSelf || storePanel.activeSelf || creditsPanel.activeSelf)
                {
                    settingsPanel.SetActive(false);
                    storePanel.SetActive(false);
                    creditsPanel.SetActive(false);
                    confirmationPanel.SetActive(false);
                    buttons.SetActive(true);
                }
            }
        }
        else
        {
            pm.Pause();
        }
    }

    public void Confirmation(int choice)
    {
        // "0" = no    // "1" = yes
        if (choice == 1)
        {
            Debug.Log("Quit");
            Application.Quit();
        }
        else
        {
            confirmationPanel.SetActive(false);
            buttons.SetActive(true);
        }
    }
}
