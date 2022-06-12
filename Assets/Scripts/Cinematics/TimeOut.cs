using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class TimeOut : MonoBehaviour
{
    [Header("Una vez acabe el video reproducido, saltará a la escena especificada")]
    [Header("Este script requiere de un videoPlayer.")]
    public string scene = "Lvl 1";
    public GameObject HoldShowdown;
    private float time;
    private bool isLerping = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        double temp = this.gameObject.GetComponent<VideoPlayer>().clip.length;
        time = (float)temp; 
        StartCoroutine(changeScene());
    }

    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scene);
    }

    IEnumerator Lerp()
    {
        float timeElapsed = 0;
        HoldShowdown.GetComponent<Image>().fillAmount = 0;
        while (isLerping)
        {
            HoldShowdown.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1, timeElapsed / 2.5f); //a los 3 segundos se ejecuta OnOmit(). En 2.5 la carga está completada
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    void OnOmit()
    {
        SceneManager.LoadScene(scene);
    }
    
    void OnOmitStart()
    {
        isLerping = true;
        StartCoroutine(Lerp());
        Debug.Log("OmitStart");
    }

    void OnOmitEnd()
    {
        isLerping = false;
        HoldShowdown.GetComponent<Image>().fillAmount = 0;
        Debug.Log("OmitEnd");
    }
}
