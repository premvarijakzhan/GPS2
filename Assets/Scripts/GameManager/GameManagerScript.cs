using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript GMS = null;

    public static float score;
    public static int coin;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;

    public GameObject distancePanel;
    private TextMeshProUGUI distanceText;
    public float inactiveTimer = 1f;

    public static float defaultSpeed = 0.15f;
    public static float boostSpeed = 0.25f;
    public static float distance;
    private float targetDistance = 50f;
    float startTime;
    float currentTime;

    public Player player;
    public PauseMenu pm;
    public bool isGameOver = false;

    void Awake()
    {
        GMS = this;
    }

    void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
        coin = 0;
        coinText.text = coin.ToString();
        distance = 0;

        distanceText = distancePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        distancePanel.SetActive(false);

        startTime = Time.timeSinceLevelLoad;
    }

    void Update()
    {
        if (isGameOver)
        {
            SceneManager.LoadScene("GameOver");
            isGameOver = false;
            SymbolManager.SM.triggerCount = 0;
            SymbolManager.SM.isComplete = false;
            SymbolManager.SM.turnRight = false;
            SymbolManager.SM.turnLeft = false;
            SymbolManager.SM.canJump = false;
            Destroy(SymbolManager.SM.symbol);
            Destroy(SymbolManager.SM.symbol1);
            Destroy(SymbolManager.SM.symbol2);
        }
    }

    void FixedUpdate()
    {
        if (distance >= targetDistance)
        {
            distancePanel.SetActive(true);
            distanceText.text = distance.ToString("F0") + "m"; 
            targetDistance += 50f;
            StartCoroutine(Inactive());
        }
    }

    IEnumerator Inactive()
    {
        yield return new WaitForSeconds(inactiveTimer);
        distancePanel.SetActive(false);
    }

    public void UpdateScore()
    {
        if (!pm.isPaused)
        {
            score += 1;
            scoreText.text = score.ToString("F0");
        }
    }

    public void UpdateDistance()
    {
        if (!pm.isPaused)
        {
            currentTime = Time.timeSinceLevelLoad;

            if (!Player.boosterActive)
            {
                distance = ((currentTime - startTime) + defaultSpeed) * player.speed;
            }
            else
            {
                distance = ((currentTime - startTime) + boostSpeed) * player.speed;
            }
        }
    }

    public void UpdateCoins()
    {
        coin++;
        coinText.text = coin.ToString();
    }
}
