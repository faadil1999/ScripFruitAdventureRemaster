using System.Collections;
using System.Collections.Generic;
using AdventureFruit.Core.StateMachine;
using UnityEngine;

namespace AdventureFruit
{
    public class MushroomEnemy : Enemy
    {
        #region State
        public MushroomIdleState idleState;
        public MushroomMoveState moveState;
        #endregion
        private void Awake()
        {
            stateMachine = new StateMachine();
            idleState = new MushroomIdleState(this, stateMachine, "Idle", this);
            moveState = new MushroomMoveState(this, stateMachine, "Move", this);
        }
        protected override void Start()
        {
            base.Start(); // sets rb/anim (Enemy.Start) - must run before Initialize() calls Enter()
            facingDirection = -1;
            stateMachine.Initialize(idleState);
        }
        protected override void Update()
        {
            base.Update();
            this.stateMachine.currentState.Update();
         
        }


    }
}
