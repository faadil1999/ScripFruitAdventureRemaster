using System.Collections;
using System.Collections.Generic;
using AdventureFruit.Core.StateMachine;
using UnityEngine;
using static log4net.Appender.RollingFileAppender;

namespace AdventureFruit
{
    public class MushroomMoveState : EnemyState
    {
        private MushroomEnemy enemy;
        public MushroomMoveState(Enemy _baseEnemy, StateMachine _stateMchine, string _animName, MushroomEnemy _enemy) : base(_baseEnemy, _stateMchine, _animName)
        {
            this.enemy = _enemy;
        }

        public override void Enter()
        {
            base.Enter();
            this.enemy.SetCanMove(true);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            this.enemy.SetVelocity(this.enemy.GetMoveSpeed() * this.enemy.GetFacingDirection(), rb.velocity.y);
            if (this.enemy.IsWallDetected() || !this.enemy.IsGroundDetected())
            {
                this.enemy.Flip();
                this.enemy.stateMachine.ChangeState(enemy.idleState);
            }
        }
    }
}
