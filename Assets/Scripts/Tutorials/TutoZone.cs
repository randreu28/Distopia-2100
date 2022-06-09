using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoZone : MonoBehaviour
{
    public GameObject TutorialCanvas;
    public float fadeTime = 2;

    void Awake() //Para asegurarse que todas las imagenes al iniciar el juego tienen alpha 0
    {
        int children = TutorialCanvas.transform.childCount;
        for(int i = 0; i < children; i++)
        {
            if(TutorialCanvas.transform.GetChild(i).TryGetComponent(out Image image))
            {
                Color temp = image.color;
                temp.a = 0f;
                image.color = temp; 
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            int children = TutorialCanvas.transform.childCount;
            for(int i = 0; i < children; i++)
            {
                if(TutorialCanvas.transform.GetChild(i).TryGetComponent(out Image image))
                {
                    StartCoroutine(LerpAlpha(image, 1, fadeTime));
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            int children = TutorialCanvas.transform.childCount;
            for(int i = 0; i < children; i++)
            {
                if(TutorialCanvas.transform.GetChild(i).TryGetComponent(out Image image))
                {
                    //image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
                    StartCoroutine(LerpAlpha(image, 0, fadeTime));
                }
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
