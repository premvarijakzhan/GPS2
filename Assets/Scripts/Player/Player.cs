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

    private bool turnRight = false;
    private bool turnLeft = false;

    public static bool magnetActive;
    public static bool moveToPlayer = false;
    public GameObject magnet;

    public static bool shieldActive;
    public GameObject shield;
    public bool usedShield = false;

    public static bool boosterActive;

    public static bool isDead;

    public bool isPlaying = false;

    public CameraController cc;

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

        AudioManager.AM.playerSFX.clip = AudioManager.AM.runningSFX;
        AudioManager.AM.playerSFX.Play();
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > cc.animationDuration)
        {
            transform.Translate(Input.acceleration.x * speed * Time.smoothDeltaTime, 0f, 0f, Space.Self);
            movement.y = rb.velocity.y;
            rb.velocity = movement;
        }     

        if (SymbolManager.SM.turnRight && canTurn)
        {
            AudioManager.AM.PlaySFX(AudioTag.SFX_JumpingTurning);
            transform.Rotate(Vector3.up * 90);
            //turnRight = true;

            DegenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            DegenerateWorld.RunDummy();

            if (!DegenerateWorld.lastPlatform.CompareTag("platformTSection") || !DegenerateWorld.lastPlatform.CompareTag("platformLSectionRight") ||
                !DegenerateWorld.lastPlatform.CompareTag("platformLSectionLeft"))
                DegenerateWorld.RunDummy();

            this.transform.position = new Vector3(startPosition.x, this.transform.position.y, startPosition.z);
            SymbolManager.SM.turnRight = false;
        }
        else if (SymbolManager.SM.turnLeft && canTurn)
        {
            AudioManager.AM.PlaySFX(AudioTag.SFX_JumpingTurning);
            transform.Rotate(Vector3.up, -90);
            //transform.Rotate(0f, -90f * (50f * Time.smoothDeltaTime), 0f);
            //turnLeft = true;

            DegenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            DegenerateWorld.RunDummy();

            if (!DegenerateWorld.lastPlatform.CompareTag("platformTSection") || !DegenerateWorld.lastPlatform.CompareTag("platformLSectionRight") ||
                !DegenerateWorld.lastPlatform.CompareTag("platformLSectionLeft"))
                DegenerateWorld.RunDummy();

            this.transform.position = new Vector3(startPosition.x, this.transform.position.y, startPosition.z);
            SymbolManager.SM.turnLeft = false;
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

    void FixedUpdate()
    {
        if (isGrounded)
        {
            if (SymbolManager.SM.canJump && !isJumping)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                AudioManager.AM.PlaySFX(AudioTag.SFX_JumpingTurning);
            }
        }
    }

    void LateUpdate()
    {
        if (turnRight)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -90, 0), Time.deltaTime * 5f);
            DegenerateWorld.dummyTraveller.transform.position = DegenerateWorld.lastPlatform.transform.position + transform.forward * 20f;
        }

        if (turnLeft)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * 5f);
            DegenerateWorld.dummyTraveller.transform.position = DegenerateWorld.lastPlatform.transform.position + transform.forward * 20f;
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

        if (other.gameObject.CompareTag("Obstacles"))
        {
            if (shieldActive)
            {
                shield.gameObject.SetActive(false);
                shieldActive = false;
                usedShield = true;
                Destroy(SymbolManager.SM.symbol);
                AudioManager.AM.PlaySFX(AudioTag.SFX_ShieldBurst);
            }
            else if (!shieldActive && !usedShield)
            {
                if (!isPlaying)
                {
                    AudioManager.AM.PlaySFX(AudioTag.SFX_Collision);
                    isPlaying = true;
                }
                
                rb.isKinematic = true;
                isDead = true;
                StartCoroutine(PlayerDead());
            }
        }

        if (other.gameObject.CompareTag("Gem"))
        {
            Destroy(other.gameObject);
            GameManagerScript.GMS.UpdateCoins();
            AudioManager.AM.PlaySFX(AudioTag.SFX_GemCollection);
        }

        if (other.gameObject.CompareTag("Magnet"))
        {
            magnet.gameObject.SetActive(true);
            magnetActive = true;
            Destroy(other.gameObject);
            AudioManager.AM.SFX.clip = AudioManager.AM.magnetSFX;
            AudioManager.AM.SFX.Play();
            AudioManager.AM.SFX.loop = true;
        }

        if (other.gameObject.CompareTag("Shield"))
        {
            shield.gameObject.SetActive(true);
            shieldActive = true;
            Destroy(other.gameObject);
            AudioManager.AM.PlaySFX(AudioTag.SFX_ShieldOn);
        }

        if (other.gameObject.CompareTag("Booster"))
        {
            boosterActive = true;
            Destroy(other.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacles"))
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
            AudioManager.AM.PlaySFX(AudioTag.SFX_Collision);
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
    }
}
