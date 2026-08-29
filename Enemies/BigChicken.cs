using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureFruit
{
    public class BigChicken : Enemy
    {
        [SerializeField] private bool isFalling;
        [SerializeField] private bool isFlying;
        [SerializeField] private float variableflyForce;
        [SerializeField] private float currentFlyForce;
        [SerializeField] private LayerMask whatIsPlayer;
        [SerializeField] private float detectionOffset;
        [SerializeField] private float fallingGravity;
        [SerializeField] private bool groundAboveDetected;
        [SerializeField] private float maxFlyHeight;
        private bool playerDetected;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

        }

        // Update is called once per frame
        void Update()
        {
            idleTimeCounter -= Time.deltaTime;

            if (idleTimeCounter < 0)
            {
                if (groundAboveDetected)
                {
                    variableflyForce = 5;
                }
                else
                {
                    variableflyForce = 0;
                }
                rb.gravityScale = 1;
            }

            if(playerDetected)
            {
                isFalling = true;
                idleTimeCounter = idleTime;
                rb.gravityScale = fallingGravity;
            }

            if (groundDetected && idleTimeCounter < 0)
            {
                isFalling = false;
            }

            currentFlyForce = variableflyForce;
            CollisionCheck();
            AnimationController();
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.DrawLine(new Vector2(transform.position.x + detectionOffset, transform.position.y), new Vector2(transform.position.x + detectionOffset, transform.position.y - distancePlayerDetection));
        }

        //Function for fly event
        private void FlyEvent()
        {
            rb.velocity = new Vector2(speed * facedirection, currentFlyForce);
        }

        protected override void CollisionCheck()
        {
            base.CollisionCheck();
            groundAboveDetected = Physics2D.Raycast(transform.position, Vector2.down, distancePlayerDetection, whatIsGround);
            playerDetected = Physics2D.Raycast(new Vector2(transform.position.x + detectionOffset, transform.position.y), Vector2.down, distancePlayerDetection, whatIsPlayer);
        }

        protected override void AnimationController()
        {
            anim.SetBool("isFalling", isFalling);
            anim.SetBool("isGrounded", groundDetected);
        }
    }
}
