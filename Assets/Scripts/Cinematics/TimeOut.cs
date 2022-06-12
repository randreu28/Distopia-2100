using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TimeOut : MonoBehaviour
{
    [Header("Una vez acabe el video reproducido, saltará a la escena especificada")]
    [Header("Este script requiere de un videoPlayer.")]
    public string scene = "Lvl 1";

    private float time;

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

    void OnOmit()
    {
        SceneManager.LoadScene(scene);
    }
    
    void OnStartOmit()
    {
        Debug.Log("Funciona");
    }
}
