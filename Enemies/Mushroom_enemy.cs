using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureFruit
{
    public class Mushroom_enemy : Enemy
    {
        protected override void Start()
        {
            base.Start();
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
