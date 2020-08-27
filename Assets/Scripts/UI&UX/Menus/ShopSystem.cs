using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopSystem : MonoBehaviour
{
    public static ShopSystem SS = null;

    public SceneManagerScript SMS;

    void Awake()
    {
        if (SS == null)
        {
            SS = this;
            DontDestroyOnLoad(this);
        }
        else if (SS != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        SMS = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
    }

    void Update()
    {
        SMS = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
    }

    public void Back()
    {
        SMS.Back();
    }
}
