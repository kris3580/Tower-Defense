using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DayNightTransition : MonoBehaviour
{
    private PostProcessVolume[] postProcessVolumes;
    private bool isDayLocal = true; 
    public static bool isDay = true;

    private void Awake()
    {
        postProcessVolumes = GameObject.Find("PostProcessing").GetComponents<PostProcessVolume>();
    }



    public IEnumerator StartTransition()
    {
        isDay = !isDay;

        PostProcessVolume volume1 = postProcessVolumes[0];
        PostProcessVolume volume2 = postProcessVolumes[1];
        float duration = 2f;

        float elapsedTime = 0f;
        float startWeight1 = volume1.weight;
        float startWeight2 = volume2.weight;


        float targetWeight1 = isDayLocal ? 0f : 1f;
        float targetWeight2 = isDayLocal ? 1f : 0f;

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

        
    }
}