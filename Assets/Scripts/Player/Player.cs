using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    Vector3 startPosition;
    Vector3 movement;

    public static GameObject player;
    public static GameObject currentPlatform;
    public float speed;
    public float jumpForce;
    public bool isGrounded = false;
    public bool isJumping = false;
    public static bool canTurn = false;

    public static bool magnetActive;
    public static bool moveToPlayer = false;
    public GameObject magnet;

    public static bool shieldActive;
    public GameObject shield;
    public bool usedShield = false;

    public static bool boosterActive;

    public GameObject tiltTutorial;

    public static bool isDead;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        player = transform.gameObject;
        startPosition = player.transform.position;
        DegenerateWorld.RunDummy();

        magnetActive = false;
        shieldActive = false;
        boosterActive = false;

        isDead = false;
    }

    void Update()
    {
        transform.Translate(Input.acceleration.x * speed * Time.smoothDeltaTime, 0f, 0f, Space.Self);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1.6f, 1.6f), transform.position.y, transform.position.z);
        movement.y = rb.velocity.y;
        rb.velocity = movement;

        if (SymbolManager.SM.turnRight && canTurn)
        {
            transform.Rotate(Vector3.up * 90);
            //StartCoroutine(RotatePlayer(Vector3.up * 90, 0.8f));
            DegenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            DegenerateWorld.RunDummy();

            if (!DegenerateWorld.lastPlatform.CompareTag("platformTSection"))
                DegenerateWorld.RunDummy();

            this.transform.position = new Vector3(startPosition.x, this.transform.position.y, startPosition.z);
            SymbolManager.SM.turnRight = false;
        }
        else if (SymbolManager.SM.turnLeft && canTurn)
        {
            transform.Rotate(Vector3.up * -90);
            //StartCoroutine(RotatePlayer(Vector3.up * -90, 0.8f));
            DegenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            DegenerateWorld.RunDummy();

            if (!DegenerateWorld.lastPlatform.CompareTag("platformTSection"))
                DegenerateWorld.RunDummy();

            this.transform.position = new Vector3(startPosition.x, this.transform.position.y, startPosition.z);
            SymbolManager.SM.turnLeft = false;
        }

        if (isGrounded)
        {
            if (SymbolManager.SM.canJump && !isJumping)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        if (transform.position.y < 1f)
        {
            isDead = true;
            StartCoroutine(PlayerDead());
        }

        if ((isGrounded || isJumping) && !isDead)
        {
            GameManagerScript.GMS.UpdateScore();
            GameManagerScript.GMS.UpdateDistance();
        }
    }

    IEnumerator RotatePlayer(Vector3 byAngles, float inTime)
    {
        Quaternion fromAngle = transform.rotation;
        Quaternion toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);

        for (float t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
    }

    IEnumerator PlayerDead()
    {
        yield return new WaitForSeconds(1f);
        GameManagerScript.GMS.isGameOver = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other is BoxCollider && !DegenerateWorld.lastPlatform.CompareTag("platformTSection") &&
                                    !DegenerateWorld.lastPlatform.CompareTag("platformLSectionLeft") &&
                                    !DegenerateWorld.lastPlatform.CompareTag("platformLSectionRight"))
            DegenerateWorld.RunDummy();
        
        if (other is SphereCollider && other.gameObject.CompareTag("platformTSection"))
            SymbolManager.SM.SpawnSymbols(SymbolTag.Square, SymbolTag.Triangle);

        if (other is SphereCollider && other.gameObject.CompareTag("platformLSectionLeft"))
            SymbolManager.SM.SpawnSymbol(SymbolTag.Triangle);

        if (other is SphereCollider && other.gameObject.CompareTag("platformLSectionRight"))
            SymbolManager.SM.SpawnSymbol(SymbolTag.Square);

        if (other.gameObject.CompareTag("TurnTrigger"))
            canTurn = true;

        if (other.gameObject.CompareTag("ObstaclesTrigger"))
            SymbolManager.SM.SpawnSymbol(SymbolTag.Alpha);

        if (other.gameObject.CompareTag("Obstacles") || other.gameObject.CompareTag("Box"))
        {
            if (shieldActive)
            {
                shield.gameObject.SetActive(false);
                shieldActive = false;
                usedShield = true;
            }
            else if (!shieldActive && !usedShield)
            {
                rb.isKinematic = true;
                isDead = true;
                StartCoroutine(PlayerDead());
            }
        }

        if (other.gameObject.CompareTag("Box"))
        {
            if (shieldActive)
                Destroy(SymbolManager.SM.symbol);
        }

        if (other.gameObject.CompareTag("Gem"))
        {
            Destroy(other.gameObject);
            GameManagerScript.GMS.UpdateCoins();
        }

        if (other.gameObject.CompareTag("Magnet"))
        {
            magnet.gameObject.SetActive(true);
            magnetActive = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Shield"))
        {
            shield.gameObject.SetActive(true);
            shieldActive = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Booster"))
        {
            boosterActive = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.name == "TutorialTrigger")
        {

        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacles") || other.gameObject.CompareTag("Box"))
        {
            if (usedShield)
            {
                isDead = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("TurnTrigger"))
        {
            canTurn = false;
        }            

        if (other.gameObject.CompareTag("Obstacles") || other.gameObject.CompareTag("Box"))
        {
            if (usedShield)
                usedShield = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        currentPlatform = other.gameObject;

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            isJumping = false;
        }

        if (other.gameObject.CompareTag("DeathCollider"))
        {
            isDead = true;
            StartCoroutine(PlayerDead());
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

        if (other.gameObject.CompareTag("Obstacles") || other.gameObject.CompareTag("Box"))
        {
            rb.isKinematic = false;
        }
    }
}
