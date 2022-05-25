using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{

    public GameObject task;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            task.SetActive(true);
        }

        /*if (Input.GetKeyDown(KeyCode.E))
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, (Screen.height / 2)));
            Instantiate(task, new Vector3(position.x, position.y, 0), Quaternion.identity, null);
        }
        */
    }
}