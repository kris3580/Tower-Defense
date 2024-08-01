using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DayNightTransition : MonoBehaviour
{
    private PostProcessVolume[] postProcessVolumes;
    private bool isTransitioning = false;
    private bool isDay = true; // Initial state

    private void Awake()
    {
        postProcessVolumes = GameObject.Find("PostProcessing").GetComponents<PostProcessVolume>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && !isTransitioning)
        {
            StartCoroutine(LerpVolumeWeights(postProcessVolumes[0], postProcessVolumes[1], 1f)); 
        }
    }

    private IEnumerator LerpVolumeWeights(PostProcessVolume volume1, PostProcessVolume volume2, float duration)
    {
        isTransitioning = true;
        float elapsedTime = 0f;
        float startWeight1 = volume1.weight;
        float startWeight2 = volume2.weight;


        float targetWeight1 = isDay ? 0f : 1f;
        float targetWeight2 = isDay ? 1f : 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            volume1.weight = Mathf.Lerp(startWeight1, targetWeight1, t);
            volume2.weight = Mathf.Lerp(startWeight2, targetWeight2, t);

            yield return null;
        }

        // Ensure the final values are set
        volume1.weight = targetWeight1;
        volume2.weight = targetWeight2;


        isDay = !isDay;
        isTransitioning = false;
    }
}