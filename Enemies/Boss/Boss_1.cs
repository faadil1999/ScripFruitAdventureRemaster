using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss_1 : Enemy
{
    [Header("Boss specifics")]
    [SerializeField] private bool isAttacking;
    [SerializeField] private bool isFlying;
    [SerializeField] private bool isFalling;
    [SerializeField] private float radiusDetection;
    [SerializeField] private float flyForce;
    [SerializeField] private float maxFlyHeight;
    [SerializeField] private float timeBeforeAttack;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private int stateBoss;
    [SerializeField] private ParticleSystem fireFx;
    [SerializeField] private ParticleSystem wingsDustFx;
    [SerializeField] private int bossLife;
    [SerializeField] private int fallingGravity;
    [SerializeField] private PhysicsMaterial2D bouncing;
    private float timeBeforeAttackCounter;
    [SerializeField] private float angryTime;
    [SerializeField] private float angryTimeCounter;
    private Vector2 attackPoint;
    private bool groundAboveDetected;
    private bool playerDetected;

    [Header("Camera manager")]
    [SerializeField] private CinemachineImpulseSource impulse;
    [SerializeField] private Vector2 shakeDirection;
    [SerializeField] private float forceShake;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        isAttacking = false;
        isFlying = false;
        timeBeforeAttackCounter = timeBeforeAttack;
        idleTimeCounter = idleTime;
        angryTimeCounter = 0;
        bouncing.bounciness = 0.0f;

    }

    // Update is called once per frame
    void Update()
    {
        player = PlayerManager.instance?.currentPlayer?.transform;
        idleTimeCounter -= Time.deltaTime;

        if (bossLife < 10)
        {
            fallingGravity = 30;
            idleTime = 2;

            if (bossLife < 7)
            {
                if (isAggresive)
                {
                    if (!isFlying)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, attackPoint, 15 * Time.deltaTime);
                    }
                    bouncing.bounciness = 0.9f;
                }
                else
                {
                    bouncing.bounciness = 0.0f;
                }

                angryTimeCounter += Time.deltaTime;

                if (angryTimeCounter >= 7f)
                {
                    isAggresive = true;
                }
                if (angryTimeCounter > 17f)
                {
                    angryTimeCounter = 0f;
                    isAggresive = false;

                }
            }
        }

        if (idleTimeCounter < 0)
        {
            rb.gravityScale = 1;
            isFlying = true;
            isInvincible = true;
        }
        else
        {
            isInvincible = false;
            isFlying = false;
        }

        if (groundDetected)
        {
            StopAllFX();
        }

        if (groundAboveDetected)
        {
            rb.velocity = new Vector2(0, -1);
            timeBeforeAttackCounter -= Time.deltaTime;
        }

        if (timeBeforeAttackCounter < 0 && playerDetected)
        {
            BossFall();
        }
        else
        {
            isAttacking = false;
        }

        FlipController();
        CollisionCheck();
        AnimationController();
        if (isFalling && groundDetected)
        {
            groundedEffect();
            isFalling = false;
        }
    }

    private void BossFall()
    {
        rb.gravityScale = 0;
        isAttacking = true;
        attackPoint = new Vector2(player.transform.position.x, 3);
        wingsDustFx.Stop();

        transform.position = Vector2.MoveTowards(transform.position, attackPoint, 15 * Time.deltaTime);
        if (Vector2.Distance(transform.position, attackPoint) < 0.1f)
        {
            isFalling = true;
            AudioManager.instance.PlaySFX(17, 4.64f);
            rb.gravityScale = fallingGravity;
            fireFx.Stop();
            timeBeforeAttackCounter = timeBeforeAttack;
            idleTimeCounter = idleTime;
        }
    }

    public void ScreenShake()
    {
        impulse.m_DefaultVelocity = new Vector3(shakeDirection.x, shakeDirection.y) * forceShake;
        impulse.GenerateImpulse();
    }

    public void groundedEffect()
    {
        ScreenShake();
    }

    private void FlyEvent()
    {
        AudioManager.instance.PlaySFX(10);
        wingsDustFx.Play();
        fireFx.Stop();
        rb.velocity = new Vector2(speed * facedirection, flyForce);
    }

    public void FireEffectEvent()
    {
        fireFx.Play();
    }

    protected override void AnimationController()
    {
        base.AnimationController();
        anim.SetBool("attacking", isAttacking);
        anim.SetBool("flying", isFlying);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position, radiusDetection);
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + maxFlyHeight));

    }

    private void StopAllFX()
    {
        fireFx.Stop();
        wingsDustFx.Stop();
    }

    private void FlipController()
    {
        if (player == null)
            return;

        if (player.transform.position.x > transform.position.x && facedirection == -1)
        {
            Flip();
            facedirection = 1;
        }
        else if (player.transform.position.x < transform.position.x && facedirection == 1)
        {
            Flip();
            facedirection = -1;
        }
    }

    public override void Damage()
    {
        base.Damage();
        if (!isInvincible)
        {
            if (bossLife > 0)
            {
                bossLife--;
            }
            else
            {
                DestroyMe();
            }
        }
    }


    protected override void CollisionCheck()
    {
        base.CollisionCheck();
        groundAboveDetected = Physics2D.Raycast(transform.position, Vector2.up, maxFlyHeight, whatIsGround);
        playerDetected = Physics2D.OverlapCircle(transform.position, radiusDetection, whatIsPlayer);
    }
}
