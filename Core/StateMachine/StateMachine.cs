using System;
using System.Diagnostics;

namespace AdventureFruit.Core.StateMachine
{
    /// <summary>
    /// Minimal finite state machine. The owning MonoBehaviour forwards its
    /// <c>Update</c>/<c>FixedUpdate</c> to <see cref="Tick"/>/<see cref="FixedTick"/> and
    /// swaps states with <see cref="ChangeState"/>.
    /// </summary>
    public sealed class StateMachine
    {
        public State currentState { get; private set; }

        /// <summary>Raised after a state change, with (previous, next). Previous is null on the first change.</summary>
        public event Action<State, State> Changed;
        public void Initialize(State startingState)
        {
            currentState = startingState;
            currentState.Enter();
        }

        public void ChangeState(State next)
        {
            if (next == null) throw new ArgumentNullException(nameof(next));
            if (ReferenceEquals(next, currentState)) return;
            Debug.WriteLine($"StateMachine: {currentState?.GetType().Name} -> {next.GetType().Name}");
            State previous = currentState;
            previous?.Exit();
            currentState = next;
            next.Enter();
            Changed?.Invoke(previous, next);
        }

        //public void Tick(float deltaTime) => currentState?.Tick(deltaTime);

       // public void FixedTick(float fixedDeltaTime) => currentState?.FixedTick(fixedDeltaTime);
    }
}
