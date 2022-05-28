using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamDetection : MonoBehaviour
{
    public Color searchColor, spotColor;

    private Light myLight;
    private Material lens;
    private Transform tracker;

    void Start()
    {
        myLight = transform.GetChild(0).GetChild(0).GetComponent<Light>();
        lens = transform.GetChild(0).GetComponent<Renderer>().materials[1];
        tracker = GetComponent<CamRotation>().tracker.transform;
    }

    void FixedUpdate()
    {
        var ray = new Ray(transform.position, tracker.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, tracker.position, out hit, 1000) && hit.collider.gameObject.tag == "Player")
        {
            //Debug.Log(hit.collider.name);
            Spotting();
        }
        else
        {
            //Debug.Log(hit.collider.name);
            Searching();
        }
    }

    void Spotting()
    {
        lens.SetColor("_EmissiveColor", spotColor * 40f);
        myLight.color = spotColor;
    }
    
    void Searching()
    {
        lens.SetColor("_EmissiveColor", searchColor * 20f);
        myLight.color = searchColor;
    }
}
