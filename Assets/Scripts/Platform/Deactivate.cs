using UnityEngine;

public class Deactivate : MonoBehaviour
{
    public bool inactivate;

    void Start()
    {
        if (inactivate)
            Invoke("SetInactive", 4f);
    }

    void OnCollisionExit(Collision player)
    {
        if (player.gameObject.CompareTag("Player") || player.gameObject.CompareTag("Chaser"))
            Invoke("SetInactive", 4f);
    }

    void SetInactive()
    {
        this.gameObject.SetActive(false);
    }
}
