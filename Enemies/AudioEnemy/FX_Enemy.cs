using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureFruit
{
    public class FX_Enemy : MonoBehaviour
    {
        [SerializeField] private AudioSource m_audioSource;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void FxStep()
        {
            m_audioSource.Play();
        }
    }
}
