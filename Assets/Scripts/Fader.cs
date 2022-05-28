using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class Fader : MonoBehaviour
{

    [SerializeField]
    Volume _volume;

    private ColorAdjustments caComponent;

    private float _defaultColorAdjustment = 0.5f;

    [SerializeField]
    private float _FadeOutValue = -10f;

    [SerializeField]
    private float _FadeInDuration = 1;

    [SerializeField]
    private float _FadeOutDuration = 1;

    // Start is called before the first frame update
    void Start()
    {
        ColorAdjustments colorAdjustments;
        if (_volume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            caComponent = colorAdjustments;
            caComponent.postExposure.value = _FadeOutValue;
            FadeIn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Fade(float colorAdjustment, float duration, string endBroadcastMessage)
    {
        float startColorAdjustment = caComponent.postExposure.value;
        float endColorAdjustmnet = colorAdjustment;

        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            float x = Mathf.Clamp01(t / duration);
            float f = 3 * Mathf.Pow(x, 2) - 2 * Mathf.Pow(x, 3);
            caComponent.postExposure.value = Mathf.Lerp(startColorAdjustment, endColorAdjustmnet, f);
            yield return null;
        }

        BroadcastMessage(endBroadcastMessage);
    }

    public void FadeOut() {
        StartCoroutine(Fade(_FadeOutValue, _FadeOutDuration,  "FadeOutEnd"));
    }

    public void FadeIn() {
        StartCoroutine(Fade(_defaultColorAdjustment, _FadeInDuration, "FadeInEnd"));
    }

}
