using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneManagerScript : MonoBehaviour
{
    private GameObject loadingCanvas;
    private Image background;
    private Image overlay;
    private GameObject buttons;
    private GameObject settingsPanel;
    private GameObject storePanel;
    private GameObject creditsPanel;
    private GameObject confirmationPanel;
    private Image fadePanel;

    public Sprite backgroundImg;
    public Sprite normalBackgroundImg;

    public float fadeTimer = 2f;
    public float highScoreCount;
    public int currentAmount;
    public int newAmount;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI coinText;

    private Scene scene;
    private string sceneName;

    public PauseMenu pm;
    
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;

        if (sceneName == "MainMenu")
        {
            loadingCanvas = GameObject.Find("LoadingCanvas").gameObject;
            background = GameObject.Find("MainMenuCanvas").transform.GetChild(0).GetComponent<Image>();
            overlay = GameObject.Find("MainMenuCanvas").transform.GetChild(2).GetComponent<Image>();
            buttons = GameObject.Find("MainMenuCanvas").transform.GetChild(3).gameObject;
            settingsPanel = GameObject.Find("MainMenuCanvas").transform.GetChild(4).gameObject;
            creditsPanel = GameObject.Find("MainMenuCanvas").transform.GetChild(5).gameObject;
            confirmationPanel = GameObject.Find("MainMenuCanvas").transform.GetChild(6).gameObject;
            fadePanel = GameObject.Find("MainMenuCanvas").transform.GetChild(7).GetComponent<Image>();
            storePanel = GameObject.Find("StoreCanvas").transform.GetChild(0).gameObject;
            coinText = storePanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

            loadingCanvas.SetActive(false);
            overlay.gameObject.SetActive(false);
            settingsPanel.SetActive(false);
            storePanel.SetActive(false);
            creditsPanel.SetActive(false);
            confirmationPanel.SetActive(false);

            fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, 1f);

            settingsPanel.GetComponent<Options>().SetVolume();

            if (PlayerPrefs.HasKey("highscore"))
                highScoreCount = PlayerPrefs.GetFloat("highscore");

            currentAmount = PlayerPrefs.GetInt("coin", currentAmount);

            if (PlayerPrefs.HasKey("coin"))
                newAmount = PlayerPrefs.GetInt("coin");

            AudioManager.AM.music.clip = AudioManager.AM.menuMusic;
            AudioManager.AM.music.Play();

            AudioManager.AM.StopPlayerSFX();
            AudioManager.AM.StopSFX();
            AudioManager.AM.SFX.clip = null;
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
            if (Time.timeSinceLevelLoad < fadeTimer)
            {
                float fade = Time.timeSinceLevelLoad / fadeTimer;
                Color imageColor = fadePanel.color;
                imageColor.a = Mathf.Lerp(1, 0, fade);
                fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, imageColor.a);
            }

            if (GameManagerScript.score > highScoreCount)
            {
                highScoreCount = GameManagerScript.score;
                PlayerPrefs.SetFloat("highscore", highScoreCount);
            }

            highScoreText.text = highScoreCount.ToString("F0");

            coinText.text = newAmount.ToString();
        }
    }

    public void StartGame()
    {
        AudioManager.AM.PlaySFX(AudioTag.SFX_TapButton);
        buttons.SetActive(false);
        loadingCanvas.SetActive(true);
        background.sprite = normalBackgroundImg;
        StartCoroutine(loadingCanvas.GetComponent<LoadingBar>().Loading());
    }

    public void Settings()
    {
        AudioManager.AM.PlaySFX(AudioTag.SFX_TapButton);
        settingsPanel.SetActive(true);
        buttons.SetActive(false);
    }

    public void Store()
    {
        AudioManager.AM.PlaySFX(AudioTag.SFX_TapButton);
        storePanel.SetActive(true);
        buttons.SetActive(false);   
    }

    public void Credits()
    {
        AudioManager.AM.PlaySFX(AudioTag.SFX_TapButton);
        creditsPanel.SetActive(true);
        buttons.SetActive(false);
    }

    public void Back()
    {
        AudioManager.AM.PlaySFX(AudioTag.SFX_TapButton);

        if (sceneName == "MainMenu")
        {
            if (confirmationPanel)
            {
                confirmationPanel.SetActive(true);
                buttons.SetActive(false);
                background.sprite = normalBackgroundImg;
                overlay.gameObject.SetActive(true);
            }

            if (settingsPanel != null || storePanel != null || creditsPanel != null)
            {
                if (settingsPanel.activeSelf || storePanel.activeSelf || creditsPanel.activeSelf)
                {
                    overlay.gameObject.SetActive(false);
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
            if (!pm.isPaused)
                pm.Pause();
            else
                pm.Resume();
        }
    }

    public void Confirmation(int choice)
    {
        AudioManager.AM.PlaySFX(AudioTag.SFX_TapButton);

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
            background.sprite = backgroundImg;
            overlay.gameObject.SetActive(false);
        }
    }
}
