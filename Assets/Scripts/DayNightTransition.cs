using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightTransition : MonoBehaviour
{
    public float transitionDuration = 60.0f;
    public Color dayColor;
    public Color nightColor;

    public Light directionalLight;
    public float transitionTimer = 0.0f;
    private bool isDayTime = true;

    public Transform sunTransform;
    private Quaternion dayRotation;
    private Quaternion nightRotation;

    //<>

    private void Start()
    {
        directionalLight = GameObject.FindObjectOfType<Light>();
        directionalLight.color = dayColor;

        dayRotation = sunTransform.rotation;
        nightRotation = Quaternion.Euler(new Vector3(30, 0, 0));
    }

    private void Update()
    {
        transitionTimer += Time.deltaTime;

        if (transitionTimer >= transitionDuration)
        {
            transitionTimer = 0.0f;
            isDayTime = !isDayTime;

            if (isDayTime)
            {
                StartCoroutine(TransitionColor(dayColor, dayRotation));
            }
            else
                StartCoroutine(TransitionColor(nightColor, nightRotation));

        }
    }


    private IEnumerator TransitionColor(Color targetColor, Quaternion targetRotation)
    {
        Color startColor = directionalLight.color;
        float elapsedTime = 0f;
        Quaternion startRotation = sunTransform.rotation;

        while (elapsedTime < transitionDuration)
        {
            directionalLight.color = Color.Lerp(startColor, targetColor, elapsedTime / transitionDuration);
            sunTransform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / transitionDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        directionalLight.color = targetColor;
        sunTransform.rotation = targetRotation;
    }

}
