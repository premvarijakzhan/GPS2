using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    Vector3 startPosition;
    Vector3 movement;

    public Animator animator;
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

    public static bool isDead;
    public float deadTime = 2f;

    public bool isPlaying = false;
    private int playTime;

    public CameraController cc;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = transform.gameObject;
        startPosition = player.transform.position;
        DegenerateWorld.RunDummy();

        magnetActive = false;
        shieldActive = false;
        boosterActive = false;

        isDead = false;

        AudioManager.AM.playerSFX.Play();
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > cc.animationDuration && !isDead)
        {
            transform.Translate(Input.acceleration.x * speed * Time.smoothDeltaTime, 0f, 0f, Space.Self);
            movement.y = rb.velocity.y;
            rb.velocity = movement;
        }     

        if (SymbolManager.SM.turnRight && canTurn)
        {
            AudioManager.AM.PlaySFX(AudioTag.SFX_JumpingTurning);
            transform.Rotate(Vector3.up * 90);

            DegenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            DegenerateWorld.RunDummy();

            if (!DegenerateWorld.lastPlatform.CompareTag("platformTSection") && !DegenerateWorld.lastPlatform.CompareTag("platformLSectionRight") &&
                !DegenerateWorld.lastPlatform.CompareTag("platformLSectionLeft"))
                DegenerateWorld.RunDummy();

            this.transform.position = new Vector3(startPosition.x, this.transform.position.y, startPosition.z);
            SymbolManager.SM.turnRight = false;
        }
        else if (SymbolManager.SM.turnLeft && canTurn)
        {
            AudioManager.AM.PlaySFX(AudioTag.SFX_JumpingTurning);
            transform.Rotate(Vector3.up * -90);

            DegenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            DegenerateWorld.RunDummy();

            if (!DegenerateWorld.lastPlatform.CompareTag("platformTSection") && !DegenerateWorld.lastPlatform.CompareTag("platformLSectionRight") &&
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

        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isRunning", true);
        }

        if ((isGrounded || isJumping) && !isDead)
        {
            GameManagerScript.GMS.UpdateScore();
            GameManagerScript.GMS.UpdateDistance();
        }

        if (isDead)
        {
            AudioManager.AM.StopPlayerSFX();

            magnetActive = false;
            shieldActive = false;
            boosterActive = false;

            animator.SetBool("isDead", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("isJumping", false);

            Destroy(SymbolManager.SM.symbol);
            Destroy(SymbolManager.SM.symbol1);
            Destroy(SymbolManager.SM.symbol2);
        }
    }

    void FixedUpdate()
    {
        if (isGrounded)
        {
            if (SymbolManager.SM.canJump && !isJumping)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                animator.SetBool("isJumping", true);
                animator.SetBool("isRunning", false);
                AudioManager.AM.PlaySFX(AudioTag.SFX_JumpingTurning);
            }
        }
    }

    IEnumerator PlayerDead()
    {
        yield return new WaitForSeconds(deadTime);
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
            SymbolManager.SM.SpawnSymbol(SymbolTag.Arrow);

        if (other.gameObject.CompareTag("Obstacles"))
        {
            if (shieldActive)
            {
                shield.gameObject.SetActive(false);
                shieldActive = false;
                usedShield = true;
                Destroy(SymbolManager.SM.symbol);
                AudioManager.AM.PlaySFX(AudioTag.SFX_Shield);
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
            AudioManager.AM.PlaySFX(AudioTag.SFX_Shield);
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

        if (other.gameObject.CompareTag("Obstacles"))
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
            if (playTime == 0)
            {
                AudioManager.AM.PlaySFX(AudioTag.SFX_Collision);
                playTime++;
            }
            
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
