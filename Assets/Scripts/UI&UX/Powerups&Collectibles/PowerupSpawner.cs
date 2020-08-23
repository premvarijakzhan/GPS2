using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject[] powerupObjs;

    private GameObject powerupObj;

    void Start()
    {
        int randomPowerup = Random.Range(0, powerupObjs.Length);
        powerupObj = Instantiate(powerupObjs[randomPowerup], spawnPoint.position, Quaternion.identity) as GameObject;
        powerupObj.transform.SetParent(spawnPoint);
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (spawnPoint.childCount == 0)
            {
                int randomPowerup = Random.Range(0, powerupObjs.Length);
                powerupObj = Instantiate(powerupObjs[randomPowerup], spawnPoint.position, Quaternion.identity) as GameObject;
                powerupObj.transform.SetParent(spawnPoint);
            }
            else
            {
                Destroy(powerupObj);
                int randomPowerup = Random.Range(0, powerupObjs.Length);
                powerupObj = Instantiate(powerupObjs[randomPowerup], spawnPoint.position, Quaternion.identity) as GameObject;
                powerupObj.transform.SetParent(spawnPoint);
            }
        }
    }
}
