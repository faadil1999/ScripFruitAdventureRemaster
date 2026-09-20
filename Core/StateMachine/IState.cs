namespace AdventureFruit.Core.StateMachine
{
    /// <summary>
    /// A single state in a <see cref="StateMachine"/>. Implementations hold a reference
    /// to whatever shared context (components, tuning, input) they need and decide their
    /// own transitions from inside <see cref="Tick"/>.
    /// </summary>
    public abstract class State
    {
        protected StateMachine stateMachine;
        protected State(StateMachine _stateMchine)
        {
            this.stateMachine = _stateMchine;
        }

        /// <summary>Called once when the machine enters this state.</summary>
        public virtual void Enter()
        {

        }

        /// <summary>Called every frame from the owner's <c>Update</c>.</summary>
        public virtual void Update()
        {

        }
        /// <summary>Called once when the machine leaves this state.</summary>
        public virtual void Exit()
        {

        }
    }
}
