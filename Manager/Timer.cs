using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AdventureFruit
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        // Start is called before the first frame update
        void Start()
        {
            timerText = GetComponent<TextMeshProUGUI>();  
        }

        // Update is called once per frame
        void Update()
        {
            timerText.text = "Timer: " + GameManager.instance.timer.ToString("00")+ " s";
        }
    }
}
