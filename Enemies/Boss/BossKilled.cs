using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKilled : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] GameObject greating;
    // Start is called before the first frame update
    void Start()
    {
        greating.SetActive(false);  
    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null)
        {
            greating.SetActive(true);
        }
    }
}
