using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private Transform _startPoint;
    [SerializeField]
    private Transform _endPoint;
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;
    public bool analogMovement;

    private EnemyController _enemyController;

    private Transform _target;

    private FieldOfView _fieldOfView;

    private bool _isGoingToStartPoint;

    // Start is called before the first frame update
    void Start()
    {
        _fieldOfView = GetComponent<FieldOfView>();
        transform.position = _startPoint.position;
        StartCoroutine(Move());
        _enemyController = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Move() {

        if (!_fieldOfView.inFOV || !_enemyController.Grounded)
        {
            sprint = false;
            if (!_isGoingToStartPoint)
            {
                if (transform.position.x - _endPoint.position.x > 0)
                {
                    move.x = -1;
                }
                else
                {
                    move.x = 0;
                    yield return new WaitForSeconds(0.3f);
                    _isGoingToStartPoint = !_isGoingToStartPoint;
                }

                if (transform.position.y - _endPoint.position.y > 0)
                {
                    move.y = 1;
                }
                else
                {
                    move.y = 0;
                }
            }
            else
            {
                if (transform.position.x - _startPoint.position.x < 0)
                {
                    move.x = 1;
                }
                else
                {
                    move.x = 0;
                    yield return new WaitForSeconds(0.3f);
                    _isGoingToStartPoint = !_isGoingToStartPoint;
                }
            }

            if (Mathf.Abs(transform.position.z - _startPoint.position.z) > 0.1f)
            {
                if (transform.position.z - _startPoint.position.z > 0)
                {
                    move.y = -1;
                }
                else if (transform.position.z - _startPoint.position.z < 0)
                {
                    move.y = 1;
                }
            }
            else {
                move.y = 0;
            }

            //Debug.Log("transform.position.z: " + transform.position.z + " | _startPoint.position.z: " + _startPoint.position.z);

        }
        else {
            _target = _fieldOfView.objectInFOV;
            sprint = true;

            if (transform.position.x - _target.position.x > 0)
            {
                move.x = -1;
            }
            else if (transform.position.x - _target.position.x < 0)
            {
                move.x = 1;
            }
            else {
                move.x = 0;
            }

            if (transform.position.z - _target.position.z > 0)
            {
                move.y = -1;
            }
            else if (transform.position.z - _target.position.z < 0)
            {
                move.y = 1;
            }
            else {
                move.y = 0;
            }
            //Debug.Log("transform.position: " + transform.position + " | _fieldOfView.objectInFOV.position: " + _target.position);
        }

        yield return null;
        StartCoroutine(Move());
    }
}
