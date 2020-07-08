using UnityEngine;

public class Scroll : MonoBehaviour
{

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position += Player.player.transform.forward * -0.1f;
    }
}
