using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float travelTime;
    private Vector3 currentPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        currentPos = Vector3.Lerp(startPoint.position, endPoint.position,
            Mathf.Cos(Time.time / travelTime * Mathf.PI * 2) * -.5f + .5f);
        transform.position = currentPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Collider>().transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Collider>().transform.parent = null;
        }
    }

}
