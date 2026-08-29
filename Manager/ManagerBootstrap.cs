using UnityEngine;

namespace AdventureFruit
{
    /// <summary>
    /// Guarantees the persistent managers (Audio / Game / Player) exist before any
    /// scene script runs, so a level scene can be played on its own and not only by
    /// going through the Menu scene.
    ///
    /// No-op when a scene already provides a manager: each manager's own singleton
    /// guard in <c>Awake</c> destroys the extra copy. Prefabs live in
    /// <c>Assets/Resources/Managers/</c> so they can be loaded at runtime.
    ///
    /// This is a stop-gap; Phase C replaces it with a persistent "Systems" scene.
    /// </summary>
    public static class ManagerBootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void EnsureManagers()
        {
            Ensure<AudioManager>("Managers/AudioManager");
            Ensure<GameManager>("Managers/GameManager");
            Ensure<PlayerManager>("Managers/PlayerManager");
        }

        private static void Ensure<T>(string resourcePath) where T : Component
        {
            if (Object.FindObjectOfType<T>() != null)
                return;

            GameObject prefab = Resources.Load<GameObject>(resourcePath);
            if (prefab == null)
            {
                Debug.LogError($"[ManagerBootstrap] Resources/{resourcePath}.prefab not found");
                return;
            }

            Object.Instantiate(prefab).name = prefab.name;
        }
    }
}
