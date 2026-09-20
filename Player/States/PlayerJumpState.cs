using System.Collections;
using System.Collections.Generic;
using AdventureFruit.Core.StateMachine;
using UnityEngine;

namespace AdventureFruit
{
    public class PlayerJumpState : PlayerState
    {
        public PlayerJumpState(Player _player, StateMachine _stateMchine, string animName) : base(_player, _stateMchine, animName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            //Refacto later
            player.SetCanMove(true);
            player.Jump();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if(player.rb.velocity.y < 0)
            {
                player.stateMachine.ChangeState(player.airState);
            }

        }
    }
}
