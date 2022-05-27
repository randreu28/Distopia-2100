using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMaker : MonoBehaviour
{

    [SerializeField]
    private GameObject _brick;

    [SerializeField]
    private int _bricksH;

    [SerializeField]
    private int _bricksV;

    private GameObject[] _bricks;

    // Start is called before the first frame update
    void Start()
    {
        for (int h = 0; h < _bricksH; h++) {
            for (int v = 0; v < _bricksV; v++)
            {
                GameObject go = GameObject.Instantiate(_brick);
                go.transform.rotation = transform.rotation;
                go.transform.position = new Vector3(transform.position.x, transform.position.y + (go.transform.localScale.x * v), transform.position.z - (go.transform.localScale.x * h));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
