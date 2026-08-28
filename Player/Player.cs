using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net.NetworkInformation;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player controls info")]
    public VariableJoystick joystick;

    public bool pcTesting = false;
    
    private float hInput;
    private float vInput;

    [Header("Move info")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private Rigidbody2D rb;
    public Vector2 wallJumpDirection;
    private bool canBeControlled = false;
    private float playerGravityScale;
    private bool readyToLand;

    [Header("Particles")]
    [SerializeField] private ParticleSystem dustFx;
    [SerializeField] private float dustFxTimer = 0.7f;
    private float dustFxCounter;
    
    [Header("Collision info")]

//ground and wall detection stuffs
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private float groundCheckDistance;
                     private bool isGrounded;
                     private bool canDoubleJump = true;
                     private Animator anim;
                     private bool canMove;

    //enemy detection stuffs
    [SerializeField] private Transform enemyCheckcenter;
    [SerializeField] private float enemyCheckRadius;

//wall detection stuffs

    public float wallCheckDistance;
    private bool isWallDetected;

    private bool canWallSlide;
    private bool isWallSliding;

    private bool facingRight = true ;
    private int facingDirection = 1;

    // Bufferjump and cayotejump
    [Header("Buffer and Cayote info")]
    [SerializeField] private float bufferJumpTime;
    private float bufferJumpCounter;
    [SerializeField] private float cayoteJumpTime;
                     private float cayoteJumpCounter;
                     private bool  canHaveCayoteJump;

    [Header("KnockBack")]

    [SerializeField] private Vector2 knockDirection;
                     public bool isKnocked;
    [SerializeField] private float knockbackTime;
                     private bool canbeKnockback = true;
    [SerializeField] private float protectionTime;

    [Header("Respawn player")]

    [SerializeField] private float protectionTimeAfterRespawn;

    [Header("Dash Special")]
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCounterTime;
    [SerializeField] private float dashSpeed;
    public float distanceBetweenImage;
    private float lastImageXpos;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //For initiating the chosen skin
        ChangePlayerSkin();
        playerGravityScale = rb.gravityScale;
        rb.gravityScale = 0;

    }

    // Update is called once per frame
    void Update()
    {
        AnimationControllers();
        if (isKnocked)
            return;
        FlipController();
        CollisionCheck();
        InputChecks();
        dashCounterTime -= Time.deltaTime;

        //enemy damage
        EnemyDamage();

        bufferJumpCounter -= Time.deltaTime;
        cayoteJumpCounter -= Time.deltaTime;

        if (isGrounded)
        {

            canDoubleJump = true;
            canMove = true;
            if (bufferJumpCounter > 0)
            {
                bufferJumpCounter = -1;
                Jump();
            }
            canHaveCayoteJump = true;

            if (readyToLand)
            {
                dustFx.Play();  
                readyToLand = false;
            }
        }
        else
        {
            if (!readyToLand)
            { 
                readyToLand = true; 
            } 

            if (canHaveCayoteJump)
            {
                canHaveCayoteJump = false;
                cayoteJumpCounter = cayoteJumpTime;
            }
        }

        if (canWallSlide)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
        }

        Move();
    }

    public void IncrementFruits()
    {
        PlayerManager.instance.fruits++; 
    }

    public void DecrementFruits()
    {
        PlayerManager.instance.fruits--;
    }

    //the function that inflict damage to enemies
    private void EnemyDamage()
    {
        Collider2D[] hitedCollider = Physics2D.OverlapCircleAll(enemyCheckcenter.position, enemyCheckRadius);
        foreach (var enemy in hitedCollider)
        {
            if (enemy.GetComponent<Enemy>() != null)
            {
                var single_enemy = enemy.GetComponent<Enemy>();
                if(rb.velocity.y < 0)
                {
                    if (single_enemy.isInvincibleGet())
                    {
                        Knockback(single_enemy.transform);
                    }
                    else
                    {
                        AudioManager.instance.PlaySFX(3);
                        enemy.GetComponent<Enemy>().Damage();
                        Jump();
                        anim.SetBool("flipping", true);
                    }
                }
                    
            }
        }
    }

    //function for animation controller
    private void AnimationControllers()
    {
        anim.SetBool("isKnocked", isKnocked);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity",rb.velocity.y);
        anim.SetBool("canBeControlled", canBeControlled);
        anim.SetBool("isMoving", ((int)rb.velocity.x) != 0);
        anim.SetBool("isWallSliding",isWallSliding);
        anim.SetBool("isWallDetected", isWallDetected);
    }

    //function that allow to controlle the player
    public void AllowToBeControlled()
    {
        canBeControlled = true;
        rb.gravityScale = playerGravityScale;
    }

    //function for stopping flipping after killing an enemy
    public void StopFlipping()
    {
        anim.SetBool("flipping", false);
    }

    private void InputChecks()
    {
        if (!canBeControlled)
        {
            return;
        }
        if(pcTesting)
        {
            hInput = Input.GetAxisRaw("Horizontal");
            vInput = Input.GetAxisRaw("Vertical");
        }
        else
        {
            hInput = joystick.Horizontal;
            vInput = joystick.Vertical;
        }

        if(vInput < 0)
        {
            canWallSlide = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            JumpButton();

        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            dashCounterTime = dashTime;
        }
    }

    private void WallJump()
    {
        canMove = false;
        rb.velocity = new Vector2(wallJumpDirection.x * -facingDirection,wallJumpDirection.y);
    }

    //Change player skin
    public void ChangePlayerSkin()
    {
        int skinId = PlayerManager.instance.choosenCharacterId;

        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }

        anim.SetLayerWeight((skinId), 1);
    }

    public void JumpButton()
    {

        if(!isGrounded)
        {
            bufferJumpCounter = bufferJumpTime; 
        }

        if(isWallSliding)
        {
            canDoubleJump = true;
            AudioManager.instance.PlaySFX(13);
            WallJump();
        }

        else if (isGrounded || cayoteJumpCounter > 0)
        {
            AudioManager.instance.PlaySFX(4);
            Jump();

        }
        else if(canDoubleJump)
        {
            canMove = true;
            canDoubleJump = false;
            AudioManager.instance.PlaySFX(4);
            SecondJump();
        }
        canWallSlide = false;
    }


    private void FlipController()
    {
        dustFxCounter -= Time.deltaTime;
        if(facingRight && ((int)rb.velocity.x) < 0)
        {
            Flip();
        }
        else if (!facingRight && ((int)rb.velocity.x) > 0)
        {
            Flip();
        }
    }

    private void Flip() 
    {
        if(dustFxCounter < 0)
        {
            dustFx.Play();
            dustFxCounter = dustFxTimer;
        }
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

    }

    private void Move()
    {
        if(canMove)
        {
            if (dashCounterTime > 0)
            {
                rb.velocity = new Vector2(dashSpeed * hInput, 0);
                lastImageXpos = transform.position.x;
                if(Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImage)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;

                }
            }
            else
            {
                rb.velocity = new Vector2(moveSpeed * hInput, rb.velocity.y);
                //GetAxisRaw for having an instant stoping GetAxis for having slowly stopping movement
            }
        }
    }

    private void CollisionCheck() { 
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsWall);
    
        if(isWallDetected && rb.velocity.y < 0)
        {
            canWallSlide = true;
        }
        if (!isWallDetected) 
        {
            isWallSliding = false;
            canWallSlide = false;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        if (isGrounded)
        {
            dustFx.Play();
        }
    }

    //PushPlayer function is for pushing up player specialy with trampoline
    public void PushPlayer(float pushForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, pushForce);
    }
    private void SecondJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce * .8f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position , new Vector2(transform.position.x + (wallCheckDistance * facingDirection), transform.position.y));
        Gizmos.DrawWireSphere(enemyCheckcenter.position, enemyCheckRadius);
    }
    public void Knockback(Transform damageTransform)
    {
        if (!canbeKnockback)
            return;

        //camera shake after impating 
        PlayerManager.instance.ScreenShake(-facingDirection);
        AudioManager.instance.PlaySFX(2);
        if (GameManager.instance.game_difficulty > 0)
        {
            PlayerManager.instance.OnTakingDamage();
        }

        isKnocked = true;
        canbeKnockback = false;
        int hdirection = 0;
        if(transform.position.x > damageTransform.position.x)
            hdirection = 1;
        else if(transform.position.x < damageTransform.position.x)
            hdirection = -1;
        rb.velocity = new Vector2(knockDirection.x * hdirection, knockDirection.y);
        
        Invoke(nameof(CancelKnockback), knockbackTime);
        Invoke(nameof(AllowProtection), protectionTime);
        
    }

    public void CancelKnockback()
    {
        isKnocked = false;
    }

    public void AllowProtection()
    {
        canbeKnockback = true;
    }

    public void ProtectionAfterRespawn()
    {
        canbeKnockback = false;
        Invoke(nameof(AllowProtection), protectionTimeAfterRespawn);
    }

    public void ToogleMovementPlayer()
    {
        canMove = !canMove;
    }

}
