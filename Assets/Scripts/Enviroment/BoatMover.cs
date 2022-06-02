using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMover : MonoBehaviour
{

    Rigidbody _rigidbody;
    private bool _canMove = false;

    [SerializeField]
    private Transform _destination;

    private GameObject _player;

    [SerializeField]
    private float _boatSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CanMove())
            Move();
    }

    private bool CanMove()
    {
        return _canMove;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            _canMove = true;
            _player = other.gameObject;
            _player.GetComponent<PlayerController>().boatTravel = true;
        }

        if (other.transform == _destination)
        {
            _canMove = false;
            _player.GetComponent<PlayerController>().boatTravel = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            _canMove = false;
            _player.GetComponent<PlayerController>().boatTravel = false;
        }
    }

    private void Move()
    {
        _rigidbody.AddForce(transform.right * _boatSpeed);
    }

}
