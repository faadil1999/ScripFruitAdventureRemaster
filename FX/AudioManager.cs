using UnityEngine;

namespace AdventureFruit
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        [SerializeField] private AudioSource[] sound_fx;
        [SerializeField] private AudioSource[] background_fx;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            PlayBGSound(MusicId.Menu);
        }

        // --- Typed API (preferred from code) --------------------------------

        public void PlaySFX(SoundId id, float pitch = 0f) => PlaySFX((int)id, pitch);

        public void StopSFX(SoundId id) => StopSFX((int)id);

        public void PlayBGSound(MusicId id) => PlayBGSound((int)id);

        // --- Index API (kept for UnityEvent-wired inspector calls) ----------

        public void PlaySFX(int sfxToPlay, float pitch = 0f)
        {
            if (sound_fx == null || sfxToPlay < 0 || sfxToPlay >= sound_fx.Length)
                return;

            AudioSource source = sound_fx[sfxToPlay];
            if (source == null)
                return;

            source.pitch = pitch == 0f ? Random.Range(0.85f, 1.20f) : pitch;
            source.Play();
        }

        public void StopSFX(int sfxToStop)
        {
            if (sound_fx == null || sfxToStop < 0 || sfxToStop >= sound_fx.Length)
                return;

            if (sound_fx[sfxToStop] != null)
                sound_fx[sfxToStop].Stop();
        }

        public void PlayBGSound(int bgmToPlay)
        {
            StopBGSound();

            if (background_fx == null || bgmToPlay < 0 || bgmToPlay >= background_fx.Length)
                return;

            if (background_fx[bgmToPlay] != null)
                background_fx[bgmToPlay].Play();
        }

        public void StopBGSound()
        {
            if (background_fx == null)
                return;

            for (int i = 0; i < background_fx.Length; i++)
            {
                if (background_fx[i] != null)
                    background_fx[i].Stop();
            }
        }
    }
}
