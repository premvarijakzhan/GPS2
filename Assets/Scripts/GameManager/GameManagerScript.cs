using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript GMS = null;

    public static int score;
    public Text scoreText;
    public bool isGameOver = false;

    void Awake()
    {
        if (GMS == null)
        {
            GMS = this;
            DontDestroyOnLoad(this);
        }
        else if (GMS != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    void Update()
    {
        if (isGameOver)
        {
            SceneManager.LoadScene("GameOver");
            isGameOver = false;
            SymbolManager.SM.triggerCount = 0;
        }
    }
}
