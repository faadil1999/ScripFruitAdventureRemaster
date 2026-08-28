using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public static AudioManager instance;

  [SerializeField] private AudioSource[] sound_fx;
  [SerializeField] private AudioSource[] background_fx;

  private void Awake()
  {
     DontDestroyOnLoad(this.gameObject);
     if (instance == null)
     {
        instance = this;
     }
     else
     {
        Destroy(this.gameObject);
     }
  }

    public void Start()
    {
       PlayBGSound(1);   
    }

    //Function for playing sounds
    public void PlaySFX(int sfxToPlay, float pitch = 0.0f)
    {
        if(pitch == 0.0f)
        {
            if(sound_fx.Length > sfxToPlay)
            {
                sound_fx[sfxToPlay].pitch = Random.Range(0.85f, 1.20f);
                sound_fx[sfxToPlay].Play();
            }
        }
        else
        {
            sound_fx[sfxToPlay].pitch = pitch;
            sound_fx[sfxToPlay].Play();
        }
    }

    //Function for stopping play sounds
    public void StopSFX(int sfxToStop)
    {
        sound_fx[sfxToStop].Stop();
    }

    public void PlayBGSound(int bgmToPlay)
    {
        for (int i = 0; i < background_fx.Length; i++)
        {
            background_fx[i].Stop();
        }

        background_fx[bgmToPlay].Play(); 
    }

    public void StopBGSound()
    {
        for (int i = 0; i < background_fx.Length; i++)
        {
            background_fx[i].Stop();
        }
    }
}
