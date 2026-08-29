using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AdventureFruit
{
    public class SplashIntro : MonoBehaviour
    {
        [SerializeField] private float waitTime = 3f;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(TimeBeforSplash());

        }

        IEnumerator TimeBeforSplash()
        {
            yield return new WaitForSeconds(waitTime);
            SceneManager.LoadScene(1);
        }

    }
}
