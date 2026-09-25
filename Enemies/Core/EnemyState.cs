using System.Collections;
using System.Collections.Generic;
using AdventureFruit.Core.StateMachine;
using UnityEngine;

namespace AdventureFruit
{
    public class EnemyState : State
    {
        protected string animName;
        protected Enemy baseEnemy;

        public EnemyState(Enemy _enemy,StateMachine _stateMchine, string _animName) : base(_stateMchine)
        {
            this.baseEnemy = _enemy;
            this.animName = _animName;

        }

        public override void Enter()
        {
            base.Enter();
            rb = baseEnemy.rb;
            this.baseEnemy.anim.SetBool(animName, true);
            triggerCalled = false;

        }

        public override void Exit()
        {
            base.Exit();
            this.baseEnemy.anim.SetBool(animName, false);
        }

        public override void Update()
        {
            base.Update();
            stateTimer -= Time.deltaTime;
        }
    }
}
