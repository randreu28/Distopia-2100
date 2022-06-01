using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparenter : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _objects;

    [SerializeField]
    private float _duration;

    [SerializeField]
    private Material[] _defaultMaterial;

    [SerializeField]
    private Material[] _activeMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ChangeAlpha(Material material, float alpha, float duration)
    {
        Color color = material.color;
        float startAlpha = color.a;
        float endAlpha = alpha;
        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            float x = Mathf.Clamp01(t / duration);
            float f = 3 * Mathf.Pow(x, 2) - 2 * Mathf.Pow(x, 3);
            color.a = Mathf.Lerp(startAlpha, endAlpha, f);
            material.color = color;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") { 
            for (int i = 0; i < _objects.Length; i++)
            {
                _objects[i].GetComponent<Renderer>().material = _activeMaterial[i];
            }
            Action(0.3f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Action(1f);
            for (int i = 0; i < _objects.Length; i++)
            {
                _objects[i].GetComponent<Renderer>().material = _defaultMaterial[i];
            }
        }
    }

    public void Action(float value)
    {

        for (int i = 0; i < _objects.Length; i++)
        {
            StartCoroutine(ChangeAlpha(_objects[i].GetComponent<Renderer>().material, value, _duration));
        }
    }

}
