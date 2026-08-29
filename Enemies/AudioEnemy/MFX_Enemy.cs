using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureFruit
{
    public class MFX_Enemy : MonoBehaviour
    {
        [SerializeField] private AudioSource sourceFx;
        [SerializeField] private AudioClip[] clipFx;

        void SFXStep1()
        {
            sourceFx.clip = clipFx[0];
            sourceFx.Play();
        }

        void SFXStep2()
        {
            sourceFx.clip = clipFx[1];
            sourceFx.Play();
        }
    }
}
