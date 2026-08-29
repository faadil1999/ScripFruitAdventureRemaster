using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureFruit
{
    public class AudioListenerEnable : MonoBehaviour
    {
        [SerializeField] AudioListener audioCamera;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(PlayerManager.instance.currentPlayer == null)
            {
                audioCamera.enabled = true;
            }
            else
            {
                audioCamera.enabled = false;
            }
        }
    }
}
