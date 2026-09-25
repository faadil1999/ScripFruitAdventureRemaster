using System.Collections;
using System.Collections.Generic;
using AdventureFruit.Core.StateMachine;
using UnityEngine;

namespace AdventureFruit
{
    public class MushroomIdleState : EnemyState
    {
       MushroomEnemy mushroomEnemy;
        public MushroomIdleState(Enemy _baseenemy, StateMachine _stateMchine, string _animName, MushroomEnemy _enemy) : base(_baseenemy, _stateMchine, _animName)
        {
            this.mushroomEnemy = _enemy;
        }

        public override void Enter()
        {
            base.Enter();
            this.mushroomEnemy.ZeroVelocity();
            this.stateTimer = this.mushroomEnemy.idleTime;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if (stateTimer <0f)
            {
                this.mushroomEnemy.stateMachine.ChangeState(mushroomEnemy.moveState);
            }
        }
    }
}
