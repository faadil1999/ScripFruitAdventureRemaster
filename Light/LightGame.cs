using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightGame : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private float shineTime;
    [SerializeField] private float darkTIme;
    [SerializeField] private float lowIntensity;
    [SerializeField] private float highIntensity;
    [SerializeField] private float fadeDuration;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeInAndOut());
    }


    // Update is called once per frame
    void Update()
    {
       
    }
    IEnumerator FadeInAndOut()
    {
        while (true)
        {
            yield return StartCoroutine(FadeIn());
            yield return StartCoroutine(FadeOut());
        }
    }
    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        float startIntensity = lowIntensity;

        while (elapsedTime < fadeDuration)
        {
            globalLight.intensity = Mathf.Lerp(startIntensity, highIntensity, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the intensity is exactly 1 at the end
        globalLight.intensity = 1f;

        // Start the fade-out effect after waiting for a short delay
        yield return new WaitForSeconds(1f);

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        float endIntensity = lowIntensity;

        while (elapsedTime < fadeDuration)
        {
            globalLight.intensity = Mathf.Lerp(highIntensity, endIntensity, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the intensity is exactly 0 at the end
        globalLight.intensity = 0f;
    }
}
