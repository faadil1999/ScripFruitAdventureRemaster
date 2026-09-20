using AdventureFruit.Core.StateMachine;
using UnityEngine;

namespace AdventureFruit
{
    /// <summary>
    /// Jump off a wall. The horizontal direction follows the player's horizontal input
    /// (xInput); with no input the jump is straight up. Speed and height come from
    /// <see cref="Player.wallJumpDirection"/> (x = horizontal speed, y = vertical speed).
    /// </summary>
    public class PlayerWallJumpState : PlayerState
    {
        private float jumpDirection;

        public PlayerWallJumpState(Player _player, StateMachine _stateMchine, string animName) : base(_player, _stateMchine, animName)
        {
        }

        /// <summary>Set by the sliding state right before entering, with the current xInput.</summary>
        public void Setup(float horizontalInput)
        {
            jumpDirection = horizontalInput == 0f ? 0f : Mathf.Sign(horizontalInput);
        }

        public override void Enter()
        {
            base.Enter();
            player.canDoubleJump = true;
            player.FlipController(jumpDirection);
            rb.velocity = new Vector2(Mathf.Abs(player.wallJumpDirection.x) * jumpDirection, player.wallJumpDirection.y);
            AudioManager.instance.PlaySFX(SoundId.WallJump);
        }

        public override void Update()
        {
            base.Update();
            if (rb.velocity.y < 0)
            {
                player.stateMachine.ChangeState(player.airState);
            }
        }
    }
}
