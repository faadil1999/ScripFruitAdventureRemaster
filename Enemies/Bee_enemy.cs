using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AdventureFruit
{
    public class Bee_enemy : Enemy
    {
        // All about the bee enemy
        [Header("Bee specific")]
        [SerializeField] private Transform[] idlePoints;
        [SerializeField] private LayerMask whatIsPlayer;
        [SerializeField] private float checkRadius;
        [SerializeField] private Transform playerCheck;
        [SerializeField] private float yOffset;

        [Header("Bullet specifics")]
        [SerializeField] private Transform shoot_position;
        [SerializeField] private GameObject bulletprefab;
        [SerializeField] private float bulletSpeed;

        private bool playerDetected;

        private float defaultSpeed;
        private Vector2 destination;
        private int indexIdlePoint = 0;

        protected override void Start()
        {
            base.Start();
            defaultSpeed = speed;
            destination = idlePoints[0].position;
            isAggresive = false;
        }


        // Update is called once per frame
        void Update()
        {
            player = PlayerManager.instance?.currentPlayer?.transform;

            bool idle = idleTimeCounter > 0;
            anim.SetBool("idle", idle);
            idleTimeCounter -= Time.deltaTime;

            if (idle)
                return;

            if (player == null)
                return;

            playerDetected = Physics2D.OverlapCircle(playerCheck.position, checkRadius, whatIsPlayer);

            if (playerDetected && !isAggresive)
            {
                isAggresive = true;
                speed *= 1.5f;
            }

            if (!isAggresive)
            {

                destination = idlePoints[indexIdlePoint].position;
                transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

                if(Vector2.Distance(transform.position, idlePoints[indexIdlePoint].position) < .1f)
                {
                    indexIdlePoint++;
                    if (indexIdlePoint >= idlePoints.Length)
                        indexIdlePoint = 0;
                }


            }
            else
            {
                Vector2 newPosition = new Vector2(player.transform.position.x, player.transform.position.y + yOffset);
                transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

                float xDifference = transform.position.x - player.position.x;

                if (Mathf.Abs(xDifference) < .15f)
                {
                    anim.SetTrigger("attack");
                }

            }
        }

        private void AttackEvent()
        {
            GameObject bullet = Instantiate(bulletprefab, shoot_position.transform.position, shoot_position.transform.rotation);

            bullet.GetComponent<Bullet_Plant>().SetupSpeed(0, -bulletSpeed);
            speed = defaultSpeed;
            idleTimeCounter = idleTime;
            anim.ResetTrigger("attack");
            isAggresive = false;
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.DrawWireSphere(playerCheck.position, checkRadius);
        }
    }
}
