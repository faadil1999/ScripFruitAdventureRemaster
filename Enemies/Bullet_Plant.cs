using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureFruit
{
    public class Bullet_Plant : HitZoneable
    {
        private Rigidbody2D rb;
        private float xSpeed;
        private float ySpeed;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
             rb.velocity = new Vector2(xSpeed, ySpeed);
        }

        public void SetupSpeed(float x, float y)
        {
            xSpeed = x;
            ySpeed = y;
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Bullet_Plant>() == null)
            {
                base.OnTriggerEnter2D(collision);
                Destroy(gameObject);
            }
        }
    }
}
