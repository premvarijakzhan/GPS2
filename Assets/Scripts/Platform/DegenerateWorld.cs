using UnityEngine;
using UnityEngine.SceneManagement;

public class DegenerateWorld : MonoBehaviour
{
    static public GameObject dummyTraveller;
    static public GameObject lastPlatform;

    void Awake()
    {
        dummyTraveller = new GameObject("dummy");
    }

    public static void RunDummy()
    {
        GameObject p = Pool.singleton.GetRandom();

        if (p == null) return;

        if (lastPlatform != null)
        {
            if (lastPlatform.gameObject.CompareTag("platformTSection") || lastPlatform.gameObject.CompareTag("platformLSectionLeft") ||
                lastPlatform.gameObject.CompareTag("platformLSectionRight"))
                dummyTraveller.transform.position = lastPlatform.transform.position +
                Player.player.transform.forward * 20f;

            else
                dummyTraveller.transform.position = lastPlatform.transform.position +
                Player.player.transform.forward * 10f;
        }

        lastPlatform = p;
        p.SetActive(true);
        p.transform.position = dummyTraveller.transform.position;
        p.transform.rotation = dummyTraveller.transform.rotation;
    }
}
