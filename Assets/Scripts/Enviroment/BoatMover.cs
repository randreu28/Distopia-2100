using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoatMover : MonoBehaviour
{

    Rigidbody _boatRigidbody;
    private bool _canMove = false;

    [SerializeField]
    private Transform _destination;

    private GameObject _player;

    [SerializeField]
    private float _boatSpeed = 10;

    [SerializeField]
    private float _minWavesForce;

    [SerializeField]
    private float _maxWavesForce;

    [SerializeField]
    [Range(1, 100)]
    private int _wavesQuantity;

    private CharacterController _characterController;
    private Rigidbody _playerRigidbody;

    [SerializeField]
    private GameObject _ocean;

    // Start is called before the first frame update
    void Start()
    {
        _boatRigidbody = GetComponent<Rigidbody>();
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
            _player.GetComponent<PlayerController>().WordLimitsEnabled = false;
            _playerRigidbody = other.GetComponent<Rigidbody>();
            _characterController = other.GetComponent<CharacterController>();
            EnableRigidbody(true);
            Destroy(_ocean.GetComponent<KillPlayer>());
        }

        if (other.transform == _destination)
        {
            _canMove = false;
            _player.GetComponent<PlayerController>().boatTravel = false;
            EnableRigidbody(false);
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
        _boatRigidbody.AddForce(transform.right * _boatSpeed);

        if (Random.Range(1, 100-_wavesQuantity) == 1) {
            Debug.Log("Onada");
            _boatRigidbody.AddForce(-transform.up * Random.Range(_minWavesForce,_maxWavesForce), ForceMode.Impulse);
            _boatRigidbody.AddTorque(transform.right * Random.Range(0, 0.1f), ForceMode.Impulse);
            _boatRigidbody.AddTorque(transform.forward * Random.Range(0, 0.1f), ForceMode.Impulse);

        }
    }

    private void EnableRigidbody(bool value)
    {
        if (value)
        {
            _characterController.enabled = false;
            _playerRigidbody.isKinematic = false;
            _playerRigidbody.detectCollisions = true;
        }
        else
        {
            _characterController.enabled = true;
            _playerRigidbody.isKinematic = true;
            _playerRigidbody.detectCollisions = false;
        }
    }

    public void FadeOutEnd() {
        SceneManager.LoadScene("Lvl 2");
    }

}
