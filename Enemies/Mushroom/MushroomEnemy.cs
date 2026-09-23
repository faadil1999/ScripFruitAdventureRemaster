using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureFruit
{
    public class MushroomEnemy : Enemy
    {
        protected override void Start()
        {
            base.Start();
            facingDirection = -1;
        }
        private void Update()
        {
            idleTimeCounter -= Time.deltaTime;
            WalkAround();

            CollisionCheck();

            AnimationController();
        }


    }
}
