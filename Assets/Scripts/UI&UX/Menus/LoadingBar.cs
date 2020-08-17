using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingBar : MonoBehaviour
{
    public float loadingTimer = 1f;
    public Slider slider;
    public TextMeshProUGUI progressText;

    private Scene scene;
    private string sceneName;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;

        StartCoroutine(Loading());
    }

    public IEnumerator Loading()
    {
        yield return null;

        while (slider.value < slider.maxValue)
        {
            slider.value += Random.Range(0.1f, 0.3f);
            progressText.text = (slider.value * 100f).ToString("F0") + "%";
            yield return new WaitForSeconds(loadingTimer);

            if (slider.value == slider.maxValue)
            {
                if (sceneName == "LoadingScene")
                {
                    SceneManager.LoadScene("MainMenu");
                }
                else if (sceneName == "MainMenu")
                {
                    SceneManager.LoadScene("GameScene");
                    AudioManager.AM.music.clip = AudioManager.AM.gameMusic;
                    AudioManager.AM.music.Play();
                }
            }    
            
            yield return null;
        }
    }
}
