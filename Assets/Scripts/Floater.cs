using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Floater : MonoBehaviour
{

    public Transform[] _floaters;

    public float _underwaterDrag = 3;
    public float _underwaterAngularDrag = 1;

    public float _airDrag = 0f;
    public float _airAngularDrag = 0.05f;
    public float _floatingPower = 15f;

    [SerializeField]
    private GameObject _water;

    Rigidbody _rigidbody;

    bool _underwater;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Float();        
    }

    private void Float() {
        int floatersUnderwater = 0;
        for (int i = 0; i < _floaters.Length; i++)
        {
            float difference = _floaters[i].position.y - _water.transform.position.y;

            if (difference < 0)
            {
                _rigidbody.AddForceAtPosition(Vector3.up * _floatingPower * Mathf.Abs(difference), _floaters[i].position, ForceMode.Force);

                floatersUnderwater++;

                if (!_underwater)
                {
                    _underwater = true;
                    SwitchState(true);
                }
            }
            else if (_underwater && floatersUnderwater == 0)
            {
                _underwater = false;
                SwitchState(false);
            }
        }
    }

    void SwitchState(bool isUnderwater) {
        if (isUnderwater)
        {
            _rigidbody.drag = _underwaterDrag;
            _rigidbody.angularDrag = _underwaterAngularDrag;
        }
        else {
            _rigidbody.drag = _airDrag;
            _rigidbody.angularDrag = _airAngularDrag;
        }
    }
}
