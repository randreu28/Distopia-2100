using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deteccion : MonoBehaviour
{

     public static Color myColor;
     public static Color spotcolor;
     public Light targetlight;


    


    public Material buscando, pillado;
    Transform lente;
    string playerTag;
    // Start is called before the first frame update
    void Start()
    {
        
        lente = transform.parent.GetComponent<Transform>();
        playerTag = GameObject.FindGameObjectWithTag("Player").tag;
    }

    // Update is called once per frame
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == playerTag)
        {
            Vector3 direction = col.transform.position - lente.position;
            RaycastHit hit;

            if(Physics.Raycast(lente.transform.position, direction.normalized, out hit, 1000))
            {
                Debug.Log(hit.collider.name);

                if(hit.collider.gameObject.tag == playerTag)
                {
                    
                    myColor = Color.white;
                    lente.GetComponentInParent<MeshRenderer>().material = buscando;
                    targetlight.color = myColor;
                }

                else
                {
                    
                    myColor = Color.red;
                    lente.GetComponentInParent<MeshRenderer>().material = pillado; 
                     targetlight.color = myColor;
                    
                }
                
               
            }

        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.transform.tag == playerTag)
        {
                    myColor = Color.white;
                    lente.GetComponentInParent<MeshRenderer>().material = buscando; 
                     targetlight.color = myColor;
        }
    }
}


