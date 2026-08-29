namespace AdventureFruit
{
    /// <summary>
    /// Stable names for the entries of <see cref="AudioManager"/>'s <c>sound_fx</c> array.
    /// Values are the existing array indices, so UnityEvent-wired <c>PlaySFX(int)</c> calls
    /// keep working unchanged. Names are derived from each call site's context.
    /// </summary>
    public enum SoundId
    {
        PlayerDeath = 0,
        PlayerHit = 2,
        EnemyStomped = 3,
        Jump = 4,
        MenuNavigate = 5,
        MenuConfirm = 6,
        MenuDenied = 7,
        FruitPickup = 9,
        BossFly = 10,
        PlayerRespawn = 12,
        WallJump = 13,
        Trampoline = 15,
        BossSlam = 17,
    }

    /// <summary>Names for <see cref="AudioManager"/>'s <c>background_fx</c> array.</summary>
    public enum MusicId
    {
        Gameplay = 0,
        Menu = 1,
    }
}
