using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript GMS = null;

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
        
    }

    void Update()
    {
        
    }
}
