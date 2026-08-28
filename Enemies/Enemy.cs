using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : HitZoneable
{

    [SerializeField] protected float speed;
    [SerializeField] protected float idleTime = 3;
    protected RaycastHit2D playerDetection;
    protected float idleTimeCounter;
    protected bool isAggresive;

    protected Animator anim;
                     protected Rigidbody2D rb;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected float distanceGroundCheck;
    [SerializeField] protected float distanceIsGrounded;
    [SerializeField] protected float distanceWallCheck;
    [SerializeField] protected float distancePlayerDetection;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected LayerMask whatToIgnore;

    [Header("FX")]
    [SerializeField] protected GameObject deathFx;

    protected bool groundDetected;
    protected bool isGrounded;
    protected bool isMoving;
    protected bool isWall;
    protected bool isKnocked;
    protected int facedirection = -1;
    protected bool isInvincible = false;
    protected Transform player;

    protected bool canMove = true;
    protected virtual void Start()
    {
        if(PlayerManager.instance?.currentPlayer == null )
        {
            player = null;
        }
        else
        {
            player = PlayerManager.instance?.currentPlayer?.transform;

        }

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if(groundCheck == null)
        {
            groundCheck = transform;
        }
        if( wallCheck == null)
        {
            wallCheck = transform;
        }
    }

    public virtual void Damage()
    {
        if(!isInvincible)
        {
            canMove = false;
            anim.SetTrigger("gotHit");
        }
    }

    public void DestroyMe()
    {
        GameObject newObj = Instantiate(deathFx, transform.position, transform.rotation);
        Destroy(newObj, .3f);
        DestroyObject(gameObject);

        if(GetComponent<EnemyDropController>() != null)
        {
            GetComponent<EnemyDropController>().DropFruits();
        }
        else
        {
            Debug.Log("You dont have enemydropcontroller");
        }
    }

 
    //Flip the character
    protected void Flip ()
    {
        facedirection *= -1;
        transform.Rotate(0, 180, 0);
    }

    //For collision check
    protected virtual void CollisionCheck()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, distanceGroundCheck , whatIsGround);
        isWall = Physics2D.Raycast(wallCheck.position, Vector2.right*facedirection , distanceWallCheck , whatIsGround);
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, distanceIsGrounded, whatIsGround);
        playerDetection = Physics2D.Raycast(wallCheck.position, Vector2.right * facedirection, distancePlayerDetection, ~whatToIgnore);
    }

    //Drawing for collisionCheck
    protected virtual void OnDrawGizmos()
    {
        if(groundCheck != null)
        {
            Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x , groundCheck.position.y - distanceGroundCheck));
        }
        if(wallCheck != null)
        {
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + (distanceWallCheck * facedirection) , wallCheck.position.y));
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + playerDetection.distance * facedirection, wallCheck.position.y));
        }
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x , transform.position.y - distanceIsGrounded));
    }

    //Animation controller
    protected virtual void AnimationController()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
    }

    public bool isInvincibleGet()
    {
        return isInvincible;
    }

    protected void WalkAround()
    {

        idleTimeCounter -= Time.deltaTime;
        if (idleTimeCounter <= 0 && canMove)
        {
            rb.velocity = new Vector2(speed * facedirection, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }

        if (isWall || !groundDetected)
        {
            isMoving = false;
            Flip();
            idleTimeCounter = idleTime;
        }
    }
}
