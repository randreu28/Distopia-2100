using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobShadow : MonoBehaviour
{
    [SerializeField]
    private GameObject _blobShadow;

    // Start is called before the first frame update
    void Start()
    {
        _blobShadow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        _blobShadow.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        _blobShadow.SetActive(false);
    }
}
