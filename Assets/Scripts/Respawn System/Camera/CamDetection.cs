using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamDetection : MonoBehaviour
{
    [Header("Light and material settings")]
    public Color searchColor;
    public Color spotColor;
    public float spotIntensity = 40f;
    public float searchIntensity = 60f;

    [Header("Death parameters")]
    public string deathName = "Te han visto";
    [Range(0,1)] public float volume = 1f;
    public AudioClip SFX;

    private Light myLight;
    private Material lens;
    private bool inFOV;

    private DeadType _deadType;

    void Start()
    {
        myLight = transform.GetChild(0).GetChild(0).GetComponent<Light>();
        lens = transform.GetChild(0).GetComponent<Renderer>().materials[1];
        inFOV = GetComponent<FieldOfView>().inFOV;
        _deadType = DeadType.Seen;
    }

    void Spotting()
    {
        lens.SetColor("_EmissiveColor", spotColor * spotIntensity);
        myLight.color = spotColor;
    }
    
    void Searching()
    {
        lens.SetColor("_EmissiveColor", searchColor * searchIntensity);
        myLight.color = searchColor;
    }

    void Detected(Transform detectedObject)
    {
        if (detectedObject != null)
        {
            Spotting();
            if (!detectedObject.GetComponent<PlayerController>().neverDie)
            {
                detectedObject.GetComponent<PlayerController>().SetCanMove(false);
            }
            detectedObject.GetComponent<RespawnSystem>().KillPlayer(_deadType, deathName, SFX, volume);
            StartCoroutine(waitAndReset(4));
        }
        else
        {
            Searching();
        }
    }
    IEnumerator waitAndReset(float time)
    {
        this.GetComponent<CamRotation>().enabled = false;
        this.GetComponent<AudioSource>().enabled = false;
        yield return new WaitForSeconds(time);
        this.GetComponent<CamRotation>().enabled = true;
        this.GetComponent<AudioSource>().enabled = true;
        Searching();
    }
}
