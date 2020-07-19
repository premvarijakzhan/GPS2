using UnityEngine;

public class Scroll : MonoBehaviour
{
    void FixedUpdate()
    {
        this.transform.position += Player.player.transform.forward * -0.1f;
    }
}
