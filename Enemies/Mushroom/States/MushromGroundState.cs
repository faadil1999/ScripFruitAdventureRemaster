using System.Collections;
using System.Collections.Generic;
using AdventureFruit.Core.StateMachine;
using UnityEngine;

namespace AdventureFruit
{
    public class MushromGroundState : EnemyState
    {
        MushroomEnemy enemy;
        public MushromGroundState(Enemy _baseEnemy, StateMachine _stateMchine, string _animName, MushroomEnemy _enemy) : base(_baseEnemy, _stateMchine, _animName)
        {
            this.enemy = _enemy;
        }

        public override void Update()
        {
            base.Update();
            this.enemy.anim.SetFloat("xVelocity", rb.velocity.x);

        }
    }
}
