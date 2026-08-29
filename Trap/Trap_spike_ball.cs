using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureFruit
{
    public class Trap_spike_ball : HitZoneable
    {
        // Start is called before the first frame update
        private Rigidbody2D rb;
        [SerializeField] private Vector2 pushDirection ;
        [SerializeField] private Vector2 pushDirectionDuringGame ;
        [SerializeField] private float pauseTime ;
        private float pauseTimeCounter ;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.AddForce(pushDirection, ForceMode2D.Impulse);
            pauseTimeCounter = pauseTime;
        }

        // Update is called once per frame
        void Update()
        {
            pauseTimeCounter -= Time.deltaTime;
            if(pauseTimeCounter < .1f)
            {
                int direction = 1;
                if(rb.velocity.x < 0) 
                {
                    direction = -1;
                }
                else
                {
                    direction = 1;
                }
                    rb.AddForce(direction * pushDirectionDuringGame, ForceMode2D.Impulse);
                pauseTimeCounter = pauseTime;
            }   
        }
    }
}
