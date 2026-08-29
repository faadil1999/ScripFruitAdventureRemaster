using System;

namespace AdventureFruit.Core.StateMachine
{
    /// <summary>
    /// Minimal finite state machine. The owning MonoBehaviour forwards its
    /// <c>Update</c>/<c>FixedUpdate</c> to <see cref="Tick"/>/<see cref="FixedTick"/> and
    /// swaps states with <see cref="ChangeState"/>.
    /// </summary>
    public sealed class StateMachine
    {
        public IState Current { get; private set; }

        /// <summary>Raised after a state change, with (previous, next). Previous is null on the first change.</summary>
        public event Action<IState, IState> Changed;

        public void ChangeState(IState next)
        {
            if (next == null) throw new ArgumentNullException(nameof(next));
            if (ReferenceEquals(next, Current)) return;

            IState previous = Current;
            previous?.Exit();
            Current = next;
            next.Enter();
            Changed?.Invoke(previous, next);
        }

        public void Tick(float deltaTime) => Current?.Tick(deltaTime);

        public void FixedTick(float fixedDeltaTime) => Current?.FixedTick(fixedDeltaTime);
    }
}
