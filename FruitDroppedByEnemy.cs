using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureFruit
{
    public class FruitDroppedByEnemy : Fruit_Dropped
    {
        [Header("Drop settings")]
        [SerializeField] private Vector2[] droppedDirection;
        [SerializeField] private float force;
        private Rigidbody2D rb;
        private BoxCollider2D bc;
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            rb = GetComponentInParent<Rigidbody2D>();
            int random = Random.Range(0, droppedDirection.Length);
            rb.velocity = droppedDirection[random] * force;
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
        }
    }
}
