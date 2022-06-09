using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour, IPointerEnterHandler
{
    public string Scene;
    
    public AudioClip AudioClip;
    [Range(0, 1)]
    public float volume = 1f;
    private AudioSource SFX;

    void Awake()
    {
        SFX = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        SFX.clip = AudioClip;
        SFX.volume = volume;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SFX.Play();
    }

    public void PlayButton() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(Scene);
    }

    public void ExitButton() {
        Application.Quit(); //Es ignorada en el Editor
    }
}
