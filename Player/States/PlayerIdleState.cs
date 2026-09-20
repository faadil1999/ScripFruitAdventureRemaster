using System.Collections;
using System.Collections.Generic;
using AdventureFruit.Core.StateMachine;
using UnityEngine;

namespace AdventureFruit
{
    public class PlayerIdleState : PlayerGroundState
    {
        public PlayerIdleState(Player _player, StateMachine _stateMchine, string animName) : base(_player, _stateMchine, animName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.ZeroVelocity();
            player.SetIsGrounded(true);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if(xInput != 0)
            {
                player.stateMachine.ChangeState(player.moveState);
            }
        }
    }
}
