using UnityEngine;

public class Deactivate : MonoBehaviour
{
    void OnCollisionExit(Collision player)
    {
        if (player.gameObject.CompareTag("Player"))
            Invoke("SetInactive", 4.0f);
    }

    void SetInactive()
    {
        this.gameObject.SetActive(false);
    }
}
