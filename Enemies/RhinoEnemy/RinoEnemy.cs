using UnityEngine;

namespace AdventureFruit
{
    public class RinoEnemy : Enemy
    {
        // Start is called before the first frame update
        [SerializeField] private float chargeSpeed;
        [SerializeField] private float shockTime;
        [SerializeField] private AudioSource SFXAudioRhino;
        [SerializeField] private AudioClip stunnedClip;

        private float shockTimeCounter;

        protected override void Start()
        {
            base.Start();
            isInvincible = true;
            isAggresive = false;
        }

        // Update is called once per frame
        void Update()
        {

            AnimationController();
            CollisionCheck();
            if(!playerDetection)
            {
                WalkAround();
                return;
            }
            //Algo Rhino for charging player when the player is near by the rhino
            bool playerDetected = playerDetection.collider.GetComponent<Player>() != null;
            isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
            if (playerDetected)
            { isAggresive = true; } 

            if (!isAggresive)
            {
                WalkAround();
            }
            else
            {
                rb.velocity = new Vector2(chargeSpeed * facingDirection, rb.velocity.y);

                if (!isGrounded) 
                {
                    Flip();
                    isAggresive = false;
                }

                if(isWallDetected && isInvincible)
                {   
                    //Rhino stunned audio
                    SFXAudioRhino.clip = stunnedClip;
                    SFXAudioRhino.Play();
                    //Rhino schockTime
                    shockTimeCounter = shockTime;
                    isInvincible = false;
                }

                if(!isInvincible && shockTimeCounter <= 0 )
                {

                    isInvincible = true;
                    Flip();
                    isAggresive = false;
                }
                shockTimeCounter -= Time.deltaTime;
            }
        }
        protected override void AnimationController()
        {
            base.AnimationController();
            anim.SetBool("isInvincible", isInvincible); 
        }
    }
}
