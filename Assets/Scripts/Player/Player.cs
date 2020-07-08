using UnityEngine;

public class Player : MonoBehaviour
{
    Animator anim;

    Rigidbody rb;
    Vector3 startPosition;
    public static GameObject player;
    public static GameObject currentPlatform;
    public float speed;
    public float jumpForce;
    public bool isGrounded = false;
    public bool isJumping = false;
    bool canTurn = false;

    void Start()
    {
        //anim = this.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        player = this.gameObject;
        startPosition = player.transform.position;
        DegenerateWorld.RunDummy();
    }

    void Update()
    {
        Vector3 movement = new Vector3(Input.acceleration.x, 0f, 0f) * speed;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1.6f, 2f), transform.position.y, transform.position.z);
        movement.y = rb.velocity.y;
        rb.velocity = movement;

        //if (canTurn)
        //{
            if (SymbolManager.SM.turnRight)
            {
                this.transform.Rotate(Vector3.up * 90);
                DegenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
                DegenerateWorld.RunDummy();

                if (!DegenerateWorld.lastPlatform.CompareTag("platformTSection"))
                    DegenerateWorld.RunDummy();

                this.transform.position = new Vector3(startPosition.x, this.transform.position.y, startPosition.z);
                SymbolManager.SM.turnRight = false;
            }
            else if (SymbolManager.SM.turnLeft)
            {
                this.transform.Rotate(Vector3.up * -90);
                DegenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
                DegenerateWorld.RunDummy();

                if (!DegenerateWorld.lastPlatform.CompareTag("platformTSection"))
                    DegenerateWorld.RunDummy();

                this.transform.position = new Vector3(startPosition.x, this.transform.position.y, startPosition.z);
                SymbolManager.SM.turnLeft = false;
            }
        //}

        if (isGrounded)
        {
            if (SymbolManager.SM.canJump && !isJumping)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        if (transform.position.y < 0)
        {
            GameManagerScript.GMS.isGameOver = true;
        }

        /*if (Input.GetKeyDown(KeyCode.RightArrow) && canTurn)
        {
            this.transform.Rotate(Vector3.up * 90);
            DegenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            DegenerateWorld.RunDummy();

            if (DegenerateWorld.lastPlatform.tag != "platformTSection")
                DegenerateWorld.RunDummy();

            this.transform.position = new Vector3(startPosition.x, this.transform.position.y,
                                                    startPosition.z);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && canTurn)
        {

            this.transform.Rotate(Vector3.up * -90);
            DegenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            DegenerateWorld.RunDummy();

            if (DegenerateWorld.lastPlatform.tag != "platformTSection")
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
        }*/
    }

    void OnCollisionEnter(Collision other)
    {
        currentPlatform = other.gameObject;

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            isJumping = false;
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            isJumping = false;
        }
    }

    void OnCollisionExit(Collision other)
    {
        isGrounded = false;
        isJumping = true;
        SymbolManager.SM.canJump = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other is BoxCollider && DegenerateWorld.lastPlatform.gameObject.tag != "platformTSection")
            DegenerateWorld.RunDummy();

        if (other is SphereCollider)
        {
            //canTurn = true;
            int randomNum = Random.Range(0, 11) % 2;
            if (randomNum == 0)
                SymbolManager.SM.SpawnSymbol(SymbolTag.Square);
            else
                SymbolManager.SM.SpawnSymbol(SymbolTag.Triangle);
        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        if (other is SphereCollider)
            canTurn = false;
    }*/
}
