using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    private Vector3 startOffset;
    private Vector3 moveVector;

    public float transition = 0f;
    public float animationDuration = 2f;
    public Vector3 animationOffset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - player.position;
    }

    void Update()
    {
        moveVector = player.position + startOffset;
        moveVector.x = 0;
        moveVector.y = Mathf.Clamp(moveVector.y, 5, 8);

        if (transition > 1f)
        {
            transform.position = moveVector;
        }
        else
        {
            // Animation at the start
            transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
            transition += Time.deltaTime * 1 / animationDuration;
            transform.LookAt(player.position + Vector3.up);
        }
    }
}
