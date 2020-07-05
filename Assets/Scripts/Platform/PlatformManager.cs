using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject[] platformPrefabs;
    public float platformLength = 52.434f;
    public float newPlatformLength = 52f;
    public int initAmount = 5;
    public float spawnZ = 0f;
    public float spawnX = 0f;

    private GameObject platform;
    private Transform lastPlatform;

    void Start()
    {
        for (int i = 0; i < initAmount; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        lastPlatform = transform.GetChild(transform.childCount - 1);

        if (transform.childCount < initAmount)
        {
            if (lastPlatform.CompareTag("Straight"))
            {
                spawnZ = lastPlatform.transform.position.z;
                spawnZ += newPlatformLength;
            }
            
            SpawnNewPlatform();
        }
    }

    void SpawnPlatform()
    {
        platform = Instantiate(platformPrefabs[0]) as GameObject;
        platform.transform.SetParent(transform);
        platform.transform.position = Vector3.forward * spawnZ;
        spawnZ += platformLength;
    }

    void SpawnNewPlatform()
    {
        platform = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length)]) as GameObject;
        platform.transform.SetParent(transform);

        platform.transform.position = Vector3.forward * spawnZ;
    }
}
