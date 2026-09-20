using System.Collections;
using System.Collections.Generic;
using AdventureFruit.Core.StateMachine;
using UnityEngine;

namespace AdventureFruit
{
    public class PlayerAirState : PlayerState
    {
        public PlayerAirState(Player _player, StateMachine _stateMchine, string animName) : base(_player, _stateMchine, animName)
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
            if (player.canDoubleJump)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    player.stateMachine.ChangeState(player.secondJumpState);
                }
            }
            if (player.IsGroundDetected())
            {
                player.stateMachine.ChangeState(player.idleState);
            }
            if (player.IsWallDetected() && rb.velocity.y < 0 )
            {
                player.stateMachine.ChangeState(player.slidingState);
            }
           /* if(xInput!=0)
            {
                player.FlipController(xInput);
                rb.velocity = new Vector2(xInput * player.GetMoveSpeed(), rb.velocity.y);
            }*/
        }
    }
}
