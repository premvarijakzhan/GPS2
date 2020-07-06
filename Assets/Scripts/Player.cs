using UnityEngine;

public class Player : MonoBehaviour
{
    Animator anim;
    public static GameObject player;
    public static GameObject currentPlatform;
    bool canTurn = false;
    Vector3 startPosition;
    Rigidbody rb;

    void OnCollisionEnter(Collision other)
    {
        currentPlatform = other.gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        //anim = this.GetComponent<Animator>();
        player = this.gameObject;
        startPosition = player.transform.position;
        DegenerateWorld.RunDummy();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other is BoxCollider && DegenerateWorld.lastPlatform.tag != "TPlatform")
            DegenerateWorld.RunDummy();

        if (other is SphereCollider)
            canTurn = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other is SphereCollider)
            canTurn = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && canTurn)
        {
            this.transform.Rotate(Vector3.up * 90);
            DegenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            DegenerateWorld.RunDummy();

            if (DegenerateWorld.lastPlatform.tag != "TPlatform")
                DegenerateWorld.RunDummy();

            this.transform.position = new Vector3(startPosition.x, this.transform.position.y,
                                                    startPosition.z);


        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && canTurn)
        {

            this.transform.Rotate(Vector3.up * -90);
            DegenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            DegenerateWorld.RunDummy();


            if (DegenerateWorld.lastPlatform.tag != "TPlatform")
                DegenerateWorld.RunDummy();

            this.transform.position = new Vector3(startPosition.x, this.transform.position.y,
                                                    startPosition.z);

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            this.transform.Translate(-0.5f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {

            this.transform.Translate(0.5f, 0, 0);
        }

    }
}
