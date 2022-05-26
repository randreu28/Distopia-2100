using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    public Transform _startPoint;
    [SerializeField]
    private Transform _endPoint;
    [SerializeField]
    private Transform _EnemyZone;

    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;
    public bool analogMovement;

    private EnemyController _enemyController;

    private Transform _target;

    private FieldOfView _fieldOfView;

    private bool _isGoingToStartPoint;

    private bool _canMove;

    // Start is called before the first frame update
    void Start()
    {
        _canMove = true;
        _fieldOfView = GetComponent<FieldOfView>();
        if (_startPoint != null)
        {
            transform.position = _startPoint.position;
        }
        StartCoroutine(Move());
        _enemyController = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Move() {
        Debug.Log("Can Move: " + _canMove);
        if (_canMove)
        {
            if (_startPoint != null && _endPoint != null)
            {
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
                    else
                    {
                        move.y = 0;
                    }
                }
            }

            if (_fieldOfView.inFOV)
            {
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
                else
                {
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
                else
                {
                    move.y = 0;
                }
                //Debug.Log("transform.position: " + transform.position + " | _fieldOfView.objectInFOV.position: " + _target.position);
            }
        }
        else {
            move = Vector2.zero;
        }

        yield return null;
        StartCoroutine(Move());
    }

    private void OnTriggerExit(Collider other)
    {
        if (_EnemyZone != null) { 
            if (other.transform == _EnemyZone) {
                _canMove = false;
            }
        }
    }

    public void Reset()
    {
        transform.position = _startPoint.position;
        transform.rotation = _startPoint.rotation;
        _canMove = true;
    }
}
