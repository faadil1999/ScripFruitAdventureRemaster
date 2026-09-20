using System.Collections;
using System.Collections.Generic;
using AdventureFruit.Core.StateMachine;
using UnityEngine;

namespace AdventureFruit
{
    public class PlayerDashState : PlayerState
    {
        public PlayerDashState(Player _player, StateMachine _stateMchine, string animName) : base(_player, _stateMchine, animName)
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
