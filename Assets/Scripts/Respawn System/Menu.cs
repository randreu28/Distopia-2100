using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour, IPointerEnterHandler
{
    public GameObject Player;
    [Space(10)]
    public AudioClip AudioClip;
    [Range(0, 1)]
    public float volume = 1f;
    
    private RespawnSystem RespawnSystem;
    private Fader _fader;
    private AudioSource SFX;

    void Awake(){
        RespawnSystem = Player.GetComponent<RespawnSystem>();
        _fader = Player.GetComponent<Fader>();

        SFX = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        SFX.clip = AudioClip;
        SFX.volume = volume;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SFX.Play();
    }

    public void ContinueButton() {
        _fader.FadeIn();
        RespawnSystem.GameOver.SetActive(false);
        RespawnSystem.PauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void MainMenuButton() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void ExitButton() {
        Application.Quit(); //Es ignorada en el Editor
    }
}
