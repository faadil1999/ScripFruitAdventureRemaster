using System.Collections;
using System.Collections.Generic;
using AdventureFruit.Core.StateMachine;
using UnityEngine;

namespace AdventureFruit
{
    public class MushroomMoveState : EnemyState
    {
        public MushroomMoveState(Enemy _enemy, StateMachine _stateMchine, string _animName) : base(_enemy, _stateMchine, _animName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
