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

    private List<GameObject> _bricks;

    private bool _wallbreaked;

    // Start is called before the first frame update
    void Start()
    {
        _bricks = new List<GameObject>();

        for (int h = 0; h < _bricksH; h++) {
            for (int v = 0; v < _bricksV; v++)
            {
                GameObject go = GameObject.Instantiate(_brick);
                go.transform.rotation = transform.rotation;
                if (v % 2 == 0)
                {
                    go.transform.position = new Vector3(transform.position.x, transform.position.y + (go.transform.localScale.y * v), transform.position.z - (go.transform.localScale.z * 2 * h));
                }
                else
                {
                    go.transform.position = new Vector3(transform.position.x, transform.position.y + (go.transform.localScale.y * v), transform.position.z - (go.transform.localScale.z * 2 * h) - (go.transform.localScale.z / 2));
                }
                go.transform.parent = gameObject.transform;
                _bricks.Add(go);
            }
        }
    }

    public void BreakWall() {
        if (!_wallbreaked) {
            //BroadcastMessage("FreeConstrains");
            _wallbreaked = true;
        }
    }

}
