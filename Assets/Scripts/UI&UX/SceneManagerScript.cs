using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    private GameObject buttons;
    private GameObject settingsPanel;
    private GameObject shopPanel;
    private GameObject creditsPanel;
    private GameObject confirmationPanel;

    private Scene scene;
    private string sceneName;

    public PauseMenu pm;
    
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;

        if (sceneName == "MainMenu")
        {
            buttons = GameObject.Find("MainMenuCanvas").transform.GetChild(0).gameObject;
            settingsPanel = GameObject.Find("MainMenuCanvas").transform.GetChild(1).gameObject;
            shopPanel = GameObject.Find("MainMenuCanvas").transform.GetChild(2).gameObject;
            creditsPanel = GameObject.Find("MainMenuCanvas").transform.GetChild(3).gameObject;
            confirmationPanel = GameObject.Find("MainMenuCanvas").transform.GetChild(4).gameObject;
            
            settingsPanel.SetActive(false);
            shopPanel.SetActive(false);
            creditsPanel.SetActive(false);
            confirmationPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
        buttons.SetActive(false);
    }

    public void Shop()
    {
        shopPanel.SetActive(true);
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

            if (settingsPanel != null || shopPanel != null || creditsPanel != null)
            {
                if (settingsPanel.activeSelf || shopPanel.activeSelf || creditsPanel.activeSelf)
                {
                    settingsPanel.SetActive(false);
                    shopPanel.SetActive(false);
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
