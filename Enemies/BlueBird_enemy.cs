using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureFruit
{
    public class BlueBird_enemy : Enemy
    {
        private RaycastHit2D groundAboveDetected;
        [Header("BlueBird Specific")]
        [SerializeField] private float distanceFlyFromGround;
        [SerializeField] private float distanceGroundabove;
        [SerializeField] private float fly_force_up;
        [SerializeField] private float fly_force_down;
        private float fly_force;   
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }


        // flying event for flying effect
        private void FlyEvent()
        {
            rb.velocity = new Vector2(speed * facedirection, fly_force);
        }

        // Update is called once per frame
        void Update()
        {
            CollisionCheck();

            if(groundDetected)
            {
                fly_force = fly_force_up;
            }
            if(groundAboveDetected)
            {
                fly_force = fly_force_down;
            }

            if(isWall)
            {
                Flip();
            }
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - distanceFlyFromGround));
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + distanceGroundabove));
        }

        protected override void CollisionCheck()
        {
            base.CollisionCheck();
            groundAboveDetected = Physics2D.Raycast(transform.position, Vector2.up, distanceGroundabove, whatIsGround);
        }

        public override void Damage()
        {
            rb.velocity = new Vector2(0,0);
            base.Damage();
        }
    }
}
