using UnityEngine;

namespace AdventureFruit
{
    public class Trunk_enemy : Enemy
    {
        [Header("Trunks specific")]
        [SerializeField] private float moveBackTime;
        private float moveBackTimeCounter;

        private bool wallBehind;
        private bool groundBehind;

        private bool playerDetected;

        [Header("Collision specific")]
        [SerializeField] private float checkRadius;
        [SerializeField] private LayerMask whatIsPlayer;
        [SerializeField] private GameObject bulletprefab;
        [SerializeField] private Transform shoot_position;
        [SerializeField] private Transform groundBehindCheck;


        [Header("Bullet specific")]

        [SerializeField] private float attackCooldown;
        [SerializeField] private float bulletSpeed;
        private float attackCooldownCounter;
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }


        // Update is called once per frame
        void Update()
        {
            CollisionCheck();

            if (!canMove)
            {
                rb.velocity = new Vector2(0, 0);
            }

            attackCooldownCounter -= Time.deltaTime;
            moveBackTimeCounter -= Time.deltaTime;

            if (playerDetected && moveBackTimeCounter < 0 )
            {
                moveBackTimeCounter = moveBackTime;
            }

            if (playerDetection.collider.GetComponent<Player>() != null)
            {
                if (attackCooldownCounter < 0)
                {
                    attackCooldownCounter = attackCooldown;
                    anim.SetTrigger("attack");
                    canMove = false;
                }
                else if (playerDetection.distance < 3)
                {
                    MoveBackWards(1.5f);
                }
            }
            else
            {
                if (moveBackTimeCounter > 0)
                {
                    MoveBackWards(4);
                }
                else
                {
                    WalkAround();
                }
            }
            anim.SetFloat("xVelocity", rb.velocity.x);
        }

        protected override void CollisionCheck()
        {
            base.CollisionCheck();
            playerDetected = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
            groundBehind = Physics2D.Raycast(groundBehindCheck.position, Vector2.down, distanceGroundCheck, whatIsGround);
            wallBehind = Physics2D.Raycast(wallCheck.position, Vector2.right * (-facedirection + 1), distanceWallCheck, whatIsGround);
        }

        private void AttackEvent()
        {
            GameObject bullet = Instantiate(bulletprefab, shoot_position.position, shoot_position.rotation);
            bullet.GetComponent<Bullet_Plant>().SetupSpeed(bulletSpeed * facedirection, 0);
            ReturnMovement();
        }

        private void ReturnMovement()
        {
            canMove = true;
        }

        private void MoveBackWards(float multiplier)
        {
            if (wallBehind)
            { return; }


            if (!groundBehind)
            {
                return;
            }

            rb.velocity = new Vector2(multiplier * speed * -facedirection, rb.velocity.y);
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.DrawLine(groundBehindCheck.position, new Vector2(groundBehindCheck.position.x, groundBehindCheck.position.y - distanceGroundCheck));
            Gizmos.DrawWireSphere(transform.position, checkRadius);

        }
    }
}
