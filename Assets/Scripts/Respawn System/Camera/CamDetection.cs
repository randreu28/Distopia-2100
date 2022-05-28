using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamDetection : MonoBehaviour
{
    public Color searchColor, spotColor;
    public LayerMask _layerMask;
    public LayerMask _layerObstacle;

    public string deathName = "Te han visto";
    [Range(0,1)] public float volume = 1f;
    public AudioClip SFX;

    private Light myLight;
    private Material lens;
    private bool inFOV;

    void Start()
    {
        myLight = transform.GetChild(0).GetChild(0).GetComponent<Light>();
        lens = transform.GetChild(0).GetComponent<Renderer>().materials[1];
        inFOV = GetComponent<FieldOfView>().inFOV;
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

    void Detected(Transform detectedObject)
    {
        if (detectedObject != null)
        {
            Spotting();
            detectedObject.GetComponent<PlayerController>().SetCanMove(false);
            detectedObject.GetComponent<RespawnSystem>().KillPlayer(deathName, SFX, volume);
        }else
        {
            Searching();
        }
    }
}
