using System.Collections;
using System.Collections.Generic;
using AdventureFruit.Core.StateMachine;
using UnityEngine;

namespace AdventureFruit
{
    public class PlayerSecondJumpState : PlayerState
    {
        public PlayerSecondJumpState(Player _player, StateMachine _stateMchine, string animName) : base(_player, _stateMchine, animName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.canMove = true;
            player.canDoubleJump = false;
            AudioManager.instance.PlaySFX(SoundId.Jump);
            player.SecondJump();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if (player.rb.velocity.y < 0)
            {
                player.stateMachine.ChangeState(player.airState);
            }
        }
    }
}
