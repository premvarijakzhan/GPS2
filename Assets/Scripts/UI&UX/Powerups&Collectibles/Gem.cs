using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    GameObject player;

    public float speed;
    public float minDist = 5f;
    public bool isMoving = false;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, 100f) * Time.deltaTime);

        if (Player.moveToPlayer)
        {
            if ((Vector3.Distance(transform.position, player.transform.position) <= minDist) && transform.position.z < player.transform.position.z)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                isMoving = true;
            }
        }
        else
        {
            if (isMoving)
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            else
                return;
        }
    }
}
