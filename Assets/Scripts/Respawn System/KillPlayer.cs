using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public GameObject Player;

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject == Player)
        {
            var PlayerInfo = Player.GetComponent<PlayerInfo>();
            collider.gameObject.transform.position = PlayerInfo.SpawnPoint.position;
            Physics.SyncTransforms();
            Time.timeScale = 0;
            PlayerInfo.GameOver.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
