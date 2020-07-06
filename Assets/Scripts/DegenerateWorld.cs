using UnityEngine;


public class DegenerateWorld : MonoBehaviour
{
    static public GameObject dummyTraveller;
    static public GameObject lastPlatform;
    // Start is called before the first frame update
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
            if (lastPlatform.gameObject.tag == "platformTSection")
                dummyTraveller.transform.position = lastPlatform.transform.position +
                Player.player.transform.forward * 20;

            else
                dummyTraveller.transform.position = lastPlatform.transform.position +
        Player.player.transform.forward * 10;
        }

        lastPlatform = p;
        p.SetActive(true);
        p.transform.position = dummyTraveller.transform.position;
        p.transform.rotation = dummyTraveller.transform.rotation;
    }

}
