using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public string Scene;

    public void PlayButton() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(Scene);
    }

    public void ExitButton() {
        Application.Quit(); //Es ignorada en el Editor
    }
}
