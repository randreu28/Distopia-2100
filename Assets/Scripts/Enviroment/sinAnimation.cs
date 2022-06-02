using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sinAnimation : MonoBehaviour
{
    public float bounceSpeed = 3f;
    public float height = 0.5f;
    public bool differentCycle = false;
    //public float rotationSpeed = 35f;
    
    private Vector3 initialPos;

    void Start()
    {
        initialPos = gameObject.transform.position;
    }
 
    void Update()
    {
        float newY = differentCycle ? Mathf.Sin(Time.time * bounceSpeed) : Mathf.Cos(Time.time * bounceSpeed);
        gameObject.transform.position = new Vector3(initialPos.x, initialPos.y + newY * height, initialPos.z);
        //gameObject.transform.Rotate(0, Time.deltaTime * rotationSpeed, 0);
    }
}
