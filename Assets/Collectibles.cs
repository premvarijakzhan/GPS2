using UnityEngine;

public class Collectibles : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == ("Player"))
        {
            Debug.Log("Pickup");
        }
    }
}
