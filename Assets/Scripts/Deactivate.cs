using UnityEngine;

public class Deactivate : MonoBehaviour
{
    //bool dScheduled = false;
    void OnCollisionExit(Collision player)
    {
        if (player.gameObject.tag == "Player")
            Invoke("SetInactive", 4.0f);
        // dScheduled = true;
    }

    void SetInactive()
    {
        this.gameObject.SetActive(false);
        // dScheduled = false;
    }
}
