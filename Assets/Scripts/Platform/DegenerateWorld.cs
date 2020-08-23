using UnityEngine;

public class DegenerateWorld : MonoBehaviour
{
    public static GameObject dummyTraveller;
    public static GameObject lastPlatform;

    void Awake()
    {
        dummyTraveller = new GameObject("Dummy");
    }

    public static void RunDummy()
    {
        GameObject p = Pool.singleton.GetRandom();

        if (p == null) return;

        if (lastPlatform != null)
        {
            if (lastPlatform.gameObject.CompareTag("platformTSection") || lastPlatform.gameObject.CompareTag("platformLSectionLeft") ||
                lastPlatform.gameObject.CompareTag("platformLSectionRight"))
                dummyTraveller.transform.position = lastPlatform.transform.position + Player.player.transform.forward * 20f;

            else
                dummyTraveller.transform.position = lastPlatform.transform.position + Player.player.transform.forward * 10f;
        }

        lastPlatform = p;
        p.SetActive(true);
        p.transform.position = dummyTraveller.transform.position;
        p.transform.rotation = dummyTraveller.transform.rotation;
    }
}
