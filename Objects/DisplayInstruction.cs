using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayInstruction : MonoBehaviour
{
    [SerializeField] GameObject instructionWidget;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowInstruction());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShowInstruction()
    {
        yield return new WaitForSeconds(7f);
        instructionWidget.SetActive(false);
    }
}
