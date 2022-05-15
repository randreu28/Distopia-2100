using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Action(bool isStartPoint) {
        if (isStartPoint)
        {
            StartCoroutine(ToggleDoor("close", 1));
        }
        else {
            StartCoroutine(ToggleDoor("open", 1));
        }
        
    }

    private IEnumerator ToggleDoor(string action, float duration)
    {

        int xScale;

        Vector3 DoorStartScale = transform.localScale;

        if (action == "close")
        {
            xScale = 1;
            //transform.gameObject.SetActive(true);
        }
        else
        {
            xScale = 0;
        }

        Vector3 DoorEndScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);

        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            float x = Mathf.Clamp01(t / duration);
            float f = 3 * Mathf.Pow(x, 2) - 2 * Mathf.Pow(x, 3);

            transform.localScale = Vector3.Lerp(DoorStartScale, DoorEndScale, f);
            yield return null;
        }

        transform.localScale = DoorEndScale;
        

        if (action == "open")
        {
            //transform.gameObject.SetActive(false);
        }
    }

}
