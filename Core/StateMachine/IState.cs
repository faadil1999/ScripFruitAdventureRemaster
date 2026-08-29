namespace AdventureFruit.Core.StateMachine
{
    /// <summary>
    /// A single state in a <see cref="StateMachine"/>. Implementations hold a reference
    /// to whatever shared context (components, tuning, input) they need and decide their
    /// own transitions from inside <see cref="Tick"/>.
    /// </summary>
    public interface IState
    {
        /// <summary>Called once when the machine enters this state.</summary>
        void Enter();

        /// <summary>Called every frame from the owner's <c>Update</c>.</summary>
        void Tick(float deltaTime);

        /// <summary>Called every physics step from the owner's <c>FixedUpdate</c>.</summary>
        void FixedTick(float fixedDeltaTime);

        /// <summary>Called once when the machine leaves this state.</summary>
        void Exit();
    }
}
