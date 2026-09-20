using System.Collections;
using System.Collections.Generic;
using AdventureFruit.Core.StateMachine;
using UnityEngine;

namespace AdventureFruit
{
    public class PlayerMoveState : PlayerGroundState
    {
        public PlayerMoveState(Player _player, StateMachine _stateMchine, string animName) : base(_player, _stateMchine, animName)
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
            player.SetVelocity(xInput, rb.velocity.y);
            if (xInput == 0 || player.IsWallDetected())
                player.stateMachine.ChangeState(player.idleState);
        }
    }
}
