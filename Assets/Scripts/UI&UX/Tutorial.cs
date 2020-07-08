using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public bool firstTime = true;

    void Start()
    {
        if (firstTime)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
}
