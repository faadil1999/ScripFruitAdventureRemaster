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

            // Air control: steer while pressing left/right, keep momentum otherwise.
            // Done first so a state change below (e.g. landing -> Idle) has the final word.
            if (xInput != 0)
            {
                player.SetVelocity(xInput, rb.velocity.y);
            }

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
        }
    }
}
