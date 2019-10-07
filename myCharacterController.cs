using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCharacterController : MonoBehaviour
{
    [Header("General")]
    public bool inSpiritForm;
    public CameraShake cameraShake;
    public bool resettingFlag;
    private bool justRespawned;

    [Header("Movement")]
    public float moveSpeed;
    private bool facingRight;
    public Vector2 direction;
    public float fallMultiplier;

    [Header("Jump")]
    public float lowJumpMultiplier;
    private bool isJumpButtonDown;
    public Rigidbody2D rb;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayerMask;
    private bool isGrounded;
    public float jumpSpeed;
    private Vector2 velocityVector;  
    public int jumpLimit;
    public int jumpRemaining;
    private bool pressedJump;
    public bool isJumping;
    private bool jumpCycleStart;
    private Vector3 startingPos;

    [Header("Fall")]
    public float coyoteTimer;
    private float currentCoyoteTimer;
    private bool canCoyoteJump;
    public GameObject coyoteCollider;
    private IEnumerator coyoteCoroutine;
    private bool jumpStartEnd;
  
    [Header("Animation")]
    public Animator animator;

    [Header("Kick")]
    public GameObject kickHitObject;
    public float kickStartTimer;
    public float kickEndTimer;
    public float kickTimerProgress;
    private bool isKicking;
    private bool justKicked;
    public bool isKickingEnabled;

    [Header("Knockback")]
    public float knockbackX;
    public float knockbackY;
    private bool isKnockedBack;
    private float counter;
    public float knockBackTime;
    public SpriteRenderer spriteRen;
    private bool isInvulnerable;
    public float inVulnerableTime;
    private float currentInvulnurableCounter;

    [Header("Push")]
    public GameObject objectToPush;
    public bool isReadyToPush;
    private bool isPushing;

    [Header("Platform")]
    public LayerMask platformLayerMask;
    public bool isPlatform;
    private Collider2D platformCol;

    public static myCharacterController instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Duplicate Found!");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
       
    }

    public void InitializeCC()
    {
        direction = Vector2.zero;
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        isGrounded = true;
        facingRight = true;
        velocityVector = Vector2.zero;
        jumpRemaining = jumpLimit;
        startingPos = gameObject.transform.position;
        canCoyoteJump = true;
        isJumping = false;
        isJumpButtonDown = false;
        coyoteCoroutine = DeactivateCoyoteCollider();
        isKicking = false;
        justKicked = false;
        jumpStartEnd = false;
        resettingFlag = false;
        justRespawned = false;
    }

    void Update()
    {
        if (MainManager.instance.currentGameState == MainManager.GameState.Game)
        {
            //-------------------------------------Move--------------------------------------------
            if (!isPushing)
            {
                if ((Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && !isKicking && !isKnockedBack)
                {
                    direction = Vector2.left;
                    if (facingRight)
                    {
                        FlipX();
                    }
                    MainManager.instance.StartMovingBG(true);
                }
                if ((Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && !isKicking && !isKnockedBack)
                {
                    direction = Vector2.right;
                    if (!facingRight)
                    {
                        FlipX();
                    }
                    MainManager.instance.StartMovingBG(false);
                }
                if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)))
                {
                    if (!justRespawned)
                    {
                        if (justKicked)
                        {
                            direction = Vector2.zero;
                            justKicked = false;
                        }
                        else
                        {
                            direction -= Vector2.left;
                        }
                    }
                    else
                    {
                        justRespawned = false;
                        direction = Vector2.zero;
                    }
                }
                if ((Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)))
                {
                    if (!justRespawned)
                    {
                        if (justKicked)
                        {
                            MainManager.instance.StopMovingBG();
                            direction = Vector2.zero;
                            justKicked = false;
                        }
                        else
                        {
                            direction -= Vector2.right;
                        }
                    }
                    else
                    {
                        justRespawned = false;
                        direction = Vector2.zero;
                    }
                }
            }
            //--------------------------------------------Test-------------------------------------
          /*  if (Input.GetKeyDown(KeyCode.R))
            {
                MainManager.instance.ResetToCheckpoint();

            }
            if(Input.GetKeyDown(KeyCode.X))
            {
                
                Knockback();
            }*/
            //---------------------------------------------Jump----------------------------------
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && !isKicking && !isKnockedBack && !isPushing && !inSpiritForm)
            {
                Jump();
                if(isJumping)
                {
                    SoundManager.instance.JumpSFX();
                    PlayDoubleJumpAnimation();
                    //Debug.Log(jumpRemaining);
                }
                else
                {
                    //Debug.Log("ASDAasdsaSD");
                    SoundManager.instance.JumpSFX();
                    PlayJumpAnimation();
                }
                isJumpButtonDown = true;
            }
            if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow)) && !isKicking && !isKnockedBack &&!inSpiritForm)
            {
                isJumpButtonDown = false;
            }
            //------------------------------------------Pause---------------------------------------
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MainManager.instance.PauseGame();
            }
            //-------------------------------------Kick-------------------------------------------------------
            if (Input.GetKeyDown(KeyCode.B) && isGrounded && !isKicking && !isKnockedBack && isKickingEnabled && !isPushing && !inSpiritForm)
            {
                //Debug.Log("b pressed!");
                SoundManager.instance.KickSFX();
                PlayKickAnimation();
                StartCoroutine(KickCoroutine());
            }
            //----------------------------------------Push----------------------------------------------------------            
            if(isReadyToPush && Input.GetKeyDown(KeyCode.M) && !inSpiritForm)
            {
                objectToPush.transform.SetParent(this.gameObject.transform);
                isPushing = true;
                isReadyToPush = false;
            }
            if(isPushing && !inSpiritForm)
            {
                if(Input.GetKeyUp(KeyCode.M))
                {
                    objectToPush.transform.parent = null;
                    isPushing = false;
                }
                if (Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    direction = Vector2.left;                  
                    MainManager.instance.StartMovingBG(true);
                    PlayPullAnimation();
                }
                if (Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    direction = Vector2.right;                  
                    MainManager.instance.StartMovingBG(true);
                    PlayPushAnimation();
                }
                if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)))
                {                                
                    direction -= Vector2.left;                   
                }
                if ((Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)))
                {                                   
                    direction -= Vector2.right;                   
                }
            }

            //-------------------------------------Animation-------------------------------------------------------
            if (direction != Vector2.zero && isGrounded && !pressedJump && !isKnockedBack &&!isPushing)
            {
                PlayWalkAnimation();
            }
            else if(direction == Vector2.zero && isGrounded && !pressedJump && !isKicking && !isKnockedBack )
            {
                PlayIdleAnimation();
            }
        }

        if(isKicking)
        {
            direction = Vector2.zero;
           
        }
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayerMask);
        if (isGrounded)
        {        
            coyoteCollider.SetActive(true);          
            isJumping = false;     
            if(jumpCycleStart)
            {
                jumpRemaining = jumpLimit;
                jumpCycleStart = false;
            }
        }
        else
        {
            if(pressedJump)
            {
                isJumping = true;
                pressedJump = false;
                jumpCycleStart = true;
            }
            StopCoroutine(coyoteCoroutine);
            ResetCoyoteVariables();
            StartCoroutine(coyoteCoroutine);
            gameObject.transform.parent = null;
        }
        if (direction == Vector2.zero)
        {
            MainManager.instance.StopMovingBG();
        }

        if(Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayerMask))
        {
            platformCol  = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayerMask);
            gameObject.transform.SetParent(platformCol.transform);
        }

        FallDown();
    }

    void FallDown()
    {
        if (gameObject.transform.position.y < -5 && !resettingFlag)
        {
            MainManager.instance.currentGameState = MainManager.GameState.InputDisabled;
            resettingFlag = true;
            direction = Vector2.zero;
            SoundManager.instance.FallSFX();
            cameraShake.StartShake();
            justRespawned = true;
        }
    }
    

    void FixedUpdate()
    {      
        if (MainManager.instance.currentGameState == MainManager.GameState.Game)
        {
            if (isKnockedBack)
            {
                if (facingRight)
                {
                    direction = new Vector2(-1, .5f);
                }
                else
                {
                    direction = new Vector2(1, .5f);
                }
                velocityVector.x = direction.x * knockbackX;
                velocityVector.y = direction.y * knockbackY;               
            }
            else
            {
                velocityVector.x = direction.x * moveSpeed;
                velocityVector.y = rb.velocity.y;
                if (rb.velocity.y < 0)
                {
                    velocityVector.y += Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
                }
                else if (rb.velocity.y > 0 && !isJumpButtonDown)
                {
                    velocityVector.y += Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
                }               
            }
            rb.velocity = velocityVector;
        }
    }

    void Jump()
    {    
        if (jumpRemaining > 0 && (isGrounded || canCoyoteJump || isJumping))
        {
            jumpRemaining--;
            //Debug.Log(jumpRemaining + "<-rem");
            pressedJump = true;
            isGrounded = false;
            rb.velocity = Vector2.up * jumpSpeed;
        }
 
    }

    private void FlipX()
    {
        facingRight = !facingRight;
        Vector3 thisScale = transform.localScale;
        thisScale.x *= -1;
        transform.localScale = thisScale;
    }

    IEnumerator DeactivateCoyoteCollider()
    {
        while(currentCoyoteTimer<=coyoteTimer)
        {
            currentCoyoteTimer += Time.deltaTime;
            yield return null;
        }
        ResetCoyoteVariables();
        yield return 0;

    }

    void ResetCoyoteVariables()
    {
        coyoteCollider.SetActive(false);
        canCoyoteJump = false;
        currentCoyoteTimer = 0;
    }

    IEnumerator KickCoroutine()
    {
        //Debug.Log("kick routine started!");
        kickTimerProgress = 0;
        isKicking = true;
        justKicked = true;
        while (kickTimerProgress< kickStartTimer)
        {
            kickTimerProgress += Time.deltaTime;
            yield return null;
        }        
        kickHitObject.SetActive(true);
        while(kickTimerProgress<kickEndTimer)
        {
            kickTimerProgress += Time.deltaTime;
            yield return null;
        }
        ResetKickVariables();
        //Debug.Log("kick routine ended!");
        yield return null;
    }

    private void ResetKickVariables()
    {
       // Debug.Log("Reset!");
        kickHitObject.SetActive(false);
        isKicking = false;
        kickTimerProgress = 0;
    }

    private void Knockback()
    {
        if (!isInvulnerable)
        {
            PlayDamagedAnimation();
            MainManager.instance.PlayerGetHit();
            isKnockedBack = true;
            isInvulnerable = true;
            SoundManager.instance.HurtSFX();
            StartCoroutine(KnockbackTimer());
            StartCoroutine(IsInvulnerableCountDown());
        }
    }
   
    IEnumerator KnockbackTimer()
    {
        while(counter< knockBackTime)
        {
            spriteRen.enabled = !spriteRen.enabled;
            counter += Time.deltaTime;
            yield return null;
        }
        //Debug.Log("ASDASDA");
        spriteRen.enabled = true;
        counter = 0;
        isKnockedBack = false;
        velocityVector = Vector2.zero;
        direction = Vector2.zero;
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Knockback") && collision.otherCollider.CompareTag("Player")) 
        {         
            Knockback();
        }
        if (collision.collider.CompareTag("Enemy") && collision.otherCollider.CompareTag("Player"))
        {
            Knockback();
        }
        if (collision.collider.CompareTag("Enemy") && collision.otherCollider.CompareTag("Kick"))
        {
            collision.collider.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
  
    IEnumerator IsInvulnerableCountDown()
    {
        while (currentInvulnurableCounter < inVulnerableTime)
        {
            currentInvulnurableCounter += Time.deltaTime + 0.2f;
            spriteRen.enabled = !spriteRen.enabled;
            yield return new WaitForSeconds(0.2f);
        }
        spriteRen.enabled = true;
        isInvulnerable = false;
        currentInvulnurableCounter = 0;
        yield return null;
    }

    private void PlayWalkAnimation()
    {
        switch(MainManager.instance.currentPlayerInfo.characterLevel)
        {
            case -1:
                animator.Play("GhostWalk");
                break;

            case 0:
                animator.Play("0_walk");
                break;
            case 1:
                animator.Play("1_walk");
                break;
            case 2:
                animator.Play("2_walk");
                break;
            case 3:
                animator.Play("3_walk");
                break;
            case 4:
                animator.Play("4_walk");
                break;
            case 5:
                animator.Play("5_byebye");
                break;
        }
    }

    private void PlayIdleAnimation()
    {
        switch(MainManager.instance.currentPlayerInfo.characterLevel)
        {
            case -1:
                animator.Play("GhostIdle");
                break;
            case 0:
                animator.Play("0_idle");
                break;
            case 1:
                animator.Play("1_idle");
                break;
            case 2:
                animator.Play("2_idle");
                break;
            case 3:
                animator.Play("3_idle");
                break;
            case 4:
                animator.Play("4_idle");
                break;
            case 5:
                animator.Play("5_byebye");
                break;

        }
    }

    private void PlayJumpAnimation()
    {
        switch (MainManager.instance.currentPlayerInfo.characterLevel)
        {
            case -1:
                break;
            case 0:
                animator.Play("0_jump");
                break;
            case 1:
                animator.Play("1_jump");
                break;
            case 2:
                animator.Play("2_jump");
                break;
            case 3:
                animator.Play("3_jump");
                break;
            case 4:
                animator.Play("4_jump");
                break;
            case 5:
                animator.Play("5_byebye");
                break;

        }
    }

    private void PlayDoubleJumpAnimation()
    {
        switch (MainManager.instance.currentPlayerInfo.characterLevel)
        {
            case -1:
                break;
            case 0:       
                break;
            case 1:         
                animator.Play("1_doubleJump");
                break;
            case 2:

                animator.Play("2_doubleJump");
                break;
            case 3:
                animator.Play("3_doubleJump");
                break;
            case 4:
                animator.Play("4_doubleJump");
                break;
            case 5:
                animator.Play("5_byebye");
                break;

        }
    }

    private void PlayKickAnimation()
    {
        switch (MainManager.instance.currentPlayerInfo.characterLevel)
        {
            case -1:
                break;
            case 0:
               
                break;
            case 1:
                animator.Play("1_kick");
                break;
            case 2:
                animator.Play("2_kick");
                break;
            case 3:
                animator.Play("3_kick");
                break;
            case 4:
                animator.Play("4_kick");
                break;
            case 5:
                animator.Play("5_byebye");
                break;

        }
    }

    private void PlayPushAnimation()
    {
        switch (MainManager.instance.currentPlayerInfo.characterLevel)
        {
            case -1:
                break;
            case 0:
              
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                animator.Play("3_push");
                break;
            case 4:
                animator.Play("4_push");
                break;
            case 5:
                animator.Play("5_byebye");
                break;

        }
    }
    private void PlayPullAnimation()
    {
        switch (MainManager.instance.currentPlayerInfo.characterLevel)
        {
            case -1:
                break;
            case 0:

                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                animator.Play("3_pull");
                break;
            case 4:
                animator.Play("4_pull");
                break;
            case 5:
                animator.Play("5_byebye");
                break;

        }
    }
    
    private void PlayDamagedAnimation()
    {
        
        switch (MainManager.instance.currentPlayerInfo.characterLevel)
        {
            case -1:
                break;
            case 0:
                animator.Play("0_damaged");
                break;
            case 1:
                animator.Play("1_damaged");
                break;
            case 2:
                animator.Play("2_damaged");
                break;
            case 3:
                animator.Play("3_damaged");             
                break;
            case 4:
                animator.Play("4_damaged");
                break;
            case 5:
                animator.Play("5_byebye");
                break;
        }
    }

}
