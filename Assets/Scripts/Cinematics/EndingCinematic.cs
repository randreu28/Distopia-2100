using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCinematic : MonoBehaviour
{
    public GameObject EndingCredits;

    private Fader _fader;

    void OnTriggerEnter(Collider Player)
    {
        if(Player.gameObject.tag == "Player")
        {
            FadeOutEnd();
            //Me mata si hago esto y no sé porqué! @Jordi
            
            /* _fader = Player.GetComponent<Fader>();
           _fader.FadeOut(); */
        }
    }

    public void FadeOutEnd()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //Time.timeScale = 0;
        EndingCredits.SetActive(true);
    }
}
