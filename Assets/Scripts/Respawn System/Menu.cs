using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject Player;

    private PlayerInfo PlayerInfo;

    void Awake(){
        PlayerInfo = Player.GetComponent<PlayerInfo>();
    }

    public void ContinueButton() {
        PlayerInfo.GameOver.SetActive(false);
        PlayerInfo.PauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void ExitButton() {
        Application.Quit(); //Es ignorada en el Editor
    }
}
