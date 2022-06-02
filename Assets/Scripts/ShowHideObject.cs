using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideObject : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _objects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            ShowObjects(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ShowObjects(false);
        }
    }

    private void ShowObjects(bool value)
    {
        for (int i = 0; i < _objects.Length; i++)
        {
            _objects[i].SetActive(value);
        }
    }
}
