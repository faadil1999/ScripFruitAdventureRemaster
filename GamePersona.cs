using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureFruit
{
    public class GamePersona : HitZoneable
    {
        public Rigidbody2D rb;
        protected int facingDirection = 1;
        protected bool facingRight = true;

        [Header("Move info")]
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected float jumpForce;

        [Header("Collision info")]
        [SerializeField] protected Transform groundCheck;
        [SerializeField] protected LayerMask whatIsGround;
        [SerializeField] protected LayerMask whatIsWall;
        [SerializeField] protected float groundCheckDistance;
        [SerializeField] protected float wallCheckDistance;

        protected bool isGrounded;
        public Animator anim;
        public bool canMove;

        [Header("KnockBack")]

        [SerializeField] private Vector2 knockDirection;
        public bool isKnocked;
        [SerializeField] private float knockbackTime;
        private bool canbeKnockback = true;
        [SerializeField] private float protectionTime;

        [Header("Particles")]
        [SerializeField] private ParticleSystem dustFx;
        [SerializeField] private float dustFxTimer = 0.7f;
        private float dustFxCounter;

        protected bool isWallDetected;

        protected bool canWallSlide;
        protected bool isWallSliding;

        public void Flip()
        {
            if (dustFxCounter < 0)
            {
                dustFx.Play();
                dustFxCounter = dustFxTimer;
            }
            facingDirection = facingDirection * -1;
            facingRight = !facingRight;
            transform.Rotate(0, 180, 0);

        }

        public virtual void FlipController(float xInput)
        {
            if (xInput < 0 && facingDirection > 0)
                Flip();

            if (xInput > 0 && facingDirection < 0)
                Flip();
        }
        public bool IsWallDetected() => this.isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsWall);

        public bool IsGroundDetected() {
            if(groundCheck == null)
            {
                groundCheck = transform;
            }
            return this.isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        }

        //<summary> For setting isGrounded value </summary>
        public void SetIsGrounded(bool _isGrounded)
        {
            this.isGrounded = _isGrounded;
        }

        public float GetMoveSpeed()
        {
            return this.moveSpeed;
        }

        public int GetFacingDirection()
        {
            return this.facingDirection;
        }

        public void SetCanMove(bool _canMove)
        {
            this.canMove = _canMove;
        }

        #region Velocity

        public void ZeroVelocity()
        {
            if (isKnocked)
            {
                return;
            }
            rb.velocity = new Vector2(0, 0);
        }
        public void SetVelocity(float _xVelocity, float _yVelocity)
        {
            if (isKnocked)
            {
                return;
            }

            rb.velocity = new Vector2(_xVelocity * moveSpeed, _yVelocity);
            Debug.Log(rb.velocity);
            FlipController(_xVelocity);
        }
        #endregion
    }
}
