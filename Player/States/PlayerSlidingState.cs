using System.Collections;
using System.Collections.Generic;
using AdventureFruit.Core.StateMachine;
using UnityEngine;

namespace AdventureFruit
{
    public class PlayerSlidingState : PlayerState
    {
        public PlayerSlidingState(Player _player, StateMachine _stateMchine, string animName) : base(_player, _stateMchine, animName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.canDoubleJump = true;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
            if (player.IsGroundDetected())
            {
                player.stateMachine.ChangeState(player.idleState);
            }
            if (player.canDoubleJump)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    player.stateMachine.ChangeState(player.jumpState);
                }
            }
        }
    }
}
