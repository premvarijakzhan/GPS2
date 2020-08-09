using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPowerups : MonoBehaviour
{
    void Start()
    {
        int randomNum = Random.Range(0, 2);

        if (randomNum == 0)
            gameObject.tag = "Magnet";
        else if (randomNum == 1)
            gameObject.tag = "Shield";
        else if (randomNum == 2)
            gameObject.tag = "Booster";
    }
}
