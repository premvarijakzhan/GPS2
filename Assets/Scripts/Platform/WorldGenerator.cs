using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    GameObject dummyTraveller;

    void Start()
    {
        dummyTraveller = new GameObject("dummy");

        for (int i = 0; i < 20; i++)
        {
            GameObject p = Pool.singleton.GetRandom();
            if (p == null) return;

            p.SetActive(true);
            p.transform.position = dummyTraveller.transform.position;
            p.transform.rotation = dummyTraveller.transform.rotation;

            if (p.tag == "platformTSection")
            {
                if (Random.Range(0, 2) == 0)
                    dummyTraveller.transform.Rotate(new Vector3(0, 90, 0));
                else
                    dummyTraveller.transform.Rotate(new Vector3(0, -90, 0));

                dummyTraveller.transform.Translate(Vector3.forward * -10);
            }
            dummyTraveller.transform.Translate(Vector3.forward * -10);
        }
    }
}
