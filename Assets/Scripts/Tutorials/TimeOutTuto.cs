using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeOutTuto : MonoBehaviour
{
    public GameObject TutorialCanvas;
    public float fadeTime = 2;
    public float delayTime = 5f;
    public bool isActiveAtStart = true;

    void Awake()
    {
        int children = TutorialCanvas.transform.childCount;
        for(int i = 0; i < children; i++)
        {
            if(TutorialCanvas.transform.GetChild(i).TryGetComponent(out Image image))
            {
                Color temp = image.color;
                if(isActiveAtStart)
                {
                    temp.a = 1f;
                }
                else{
                    temp.a = 0f;
                }
                image.color = temp; 
            }
        }
    }

    void Start()
    {
        StartCoroutine(TimeOut());
    }

    IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(delayTime);
        int children = TutorialCanvas.transform.childCount;
        for(int i = 0; i < children; i++)
        {
            if(TutorialCanvas.transform.GetChild(i).TryGetComponent(out Image image))
            {
                StartCoroutine(LerpAlpha(image, 0, fadeTime));
            }
        }
    }

    IEnumerator LerpAlpha(Image image, float endValue, float duration)
    {
        float time = 0;
        Color startValue = image.color;
        while (time < duration)
        {
            float newAlpha = Mathf.Lerp(startValue.a, endValue, time / duration);
            image.color = new Color(startValue.r, startValue.g, startValue.b, newAlpha);
            time += Time.deltaTime;
            yield return null;
        }
        image.color = new Color(startValue.r, startValue.g, startValue.b, endValue);
    }
}
