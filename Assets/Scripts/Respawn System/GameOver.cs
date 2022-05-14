using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject Player;

    private RespawnInfo RespawnInfo;

    void Awake(){
        RespawnInfo = Player.GetComponent<RespawnInfo>();
    }

    public void ContinueButton() {
        RespawnInfo.GameOver.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void ExitButton() {
        Application.Quit(); //Es ignorada en el Editor
    }
}
