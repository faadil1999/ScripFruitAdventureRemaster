using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureFruit
{
    public class Greating : MonoBehaviour
    {
        private ScrollRect scrollRect;

        // Start is called before the first frame update
        void Start()
        {
            scrollRect = GetComponent<ScrollRect>();
        }

        // Update is called once per frame
        void Update()
        {
            Scroll();
        }

        /**
        * Scrolls the scrollview.
        * Note that positive value scrolls down, negative value scrolls up.
        */
        public void Scroll(float value = .003f)
        {
            if(scrollRect.verticalNormalizedPosition > .0f)
            {
                scrollRect.verticalNormalizedPosition -= value;
            }
            else
            {
                scrollRect.verticalNormalizedPosition = .0f;
            }
            Debug.Log(scrollRect.verticalNormalizedPosition);
        }
    }
}
