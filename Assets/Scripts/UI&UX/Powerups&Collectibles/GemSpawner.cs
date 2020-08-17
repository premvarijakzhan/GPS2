using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject gemPrefab;

    private GameObject gem;

    void Start()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            gem = Instantiate(gemPrefab, spawnPoints[i].transform.position, Quaternion.Euler(new Vector3(-90, 0, 0))) as GameObject;
            gem.transform.SetParent(spawnPoints[i]);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (spawnPoints[i].childCount == 0)
                {
                    gem = Instantiate(gemPrefab, spawnPoints[i].transform.position, Quaternion.Euler(new Vector3(-90, 0, 0))) as GameObject;
                    gem.transform.SetParent(spawnPoints[i]);
                }
            }
        }
    }
}
