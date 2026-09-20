using System.Collections;
using System.Collections.Generic;
using AdventureFruit.Core.StateMachine;
using UnityEngine;

namespace AdventureFruit
{
    public class PlayerState : State
    {
        protected Player player;
        protected string animName;
        protected Rigidbody2D rb;
        protected float xInput;
        protected float yInput;
        protected float stateTimer;
        protected bool triggerCalled = false;


        public PlayerState(Player _player, StateMachine _stateMchine,string animName) : base(_stateMchine)
        {
            this.player = _player;
            this.animName = animName;
        }

        public override void Enter()
        {
            base.Enter();
            this.player.anim.SetBool(animName, true);
            rb = player.rb;
            triggerCalled = false;
        }

        public override void Exit()
        {
            base.Exit();
            this.player.anim.SetBool(animName, false);
        }

        public override void Update()
        {
            base.Update();
            stateTimer -= Time.deltaTime;
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");
            player.anim.SetFloat("yVelocity", rb.velocity.y);

        }

        public virtual void AnimationFinishedTrigger()
        {
            triggerCalled = true;
        }
    }
}
