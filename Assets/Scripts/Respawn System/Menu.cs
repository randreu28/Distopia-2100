using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject Player;

    private RespawnSystem RespawnSystem;

    private Fader _fader;

    void Awake(){
        RespawnSystem = Player.GetComponent<RespawnSystem>();
        _fader = Player.GetComponent<Fader>();
    }

    public void ContinueButton() {
        _fader.FadeIn();
        RespawnSystem.GameOver.SetActive(false);
        RespawnSystem.PauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void ExitButton() {
        Application.Quit(); //Es ignorada en el Editor
    }
}
