using System.Collections;
using System.Collections.Generic;
using AdventureFruit.Core.StateMachine;
using UnityEngine;

namespace AdventureFruit
{
    public class Enemy : GamePersona
    {
        public float idleTime = 3;
        protected RaycastHit2D playerDetection;
        protected float idleTimeCounter;
        protected bool isAggresive;

        [SerializeField] protected float distanceIsGrounded;
        [SerializeField] protected float distancePlayerDetection;
        [SerializeField] protected Transform wallCheck;
        [SerializeField] protected LayerMask whatToIgnore;

        [Header("FX")]
        [SerializeField] protected GameObject deathFx;

        protected bool canWallSlide;
        protected bool isWallSliding;

        //delete later
        protected bool isMoving;
        protected bool isInvincible = false;
        protected Transform player;

        protected bool canMove = true;

        #region StateMachine
        public StateMachine stateMachine;
        
        #endregion

        protected virtual void Awake()
        {
           
        }
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

            this.anim = GetComponent<Animator>();
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
        protected virtual void Update()
        {
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
            facingDirection *= -1;
            transform.Rotate(0, 180, 0);
        }

        //For collision check
        protected virtual void CollisionCheck()
        {
            this.IsWallDetected();
            this.IsGroundDetected();
            playerDetection = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, distancePlayerDetection, ~whatToIgnore);
        }

        //Drawing for collisionCheck
        protected virtual void OnDrawGizmos()
        {
            if(groundCheck != null)
            {
                Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x , groundCheck.position.y - groundCheckDistance));
            }
            if(wallCheck != null)
            {
                Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + (wallCheckDistance * facingDirection) , wallCheck.position.y));
                Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + playerDetection.distance * facingDirection, wallCheck.position.y));
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
                rb.velocity = new Vector2(moveSpeed * facingDirection, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }

            if (isWallDetected || !isGrounded)
            {
                isMoving = false;
                Flip();
                idleTimeCounter = idleTime;
            }
        }
    }
}
