using System.Collections;
using System.Collections.Generic;
using AdventureFruit.Core.StateMachine;
using UnityEngine;

namespace AdventureFruit
{
    public class EnemyState : State
    {
        protected string animName;
        protected Enemy enemy;
        public EnemyState(Enemy _enemy,StateMachine _stateMchine, string _animName) : base(_stateMchine)
        {
            this.enemy = _enemy;
            this.animName = _animName;

        }

        public override void Enter()
        {
            base.Enter();
            this.enemy.anim.SetBool(animName, true);
        }

        public override void Exit()
        {
            base.Exit();
            this.enemy.anim.SetBool(animName, false);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
