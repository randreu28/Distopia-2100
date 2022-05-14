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
            var RespawnInfo = Player.GetComponent<RespawnInfo>();
            collider.gameObject.transform.position = RespawnInfo.SpawnPoint.position;
            Physics.SyncTransforms();
            Time.timeScale = 0;
            RespawnInfo.GameOver.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
