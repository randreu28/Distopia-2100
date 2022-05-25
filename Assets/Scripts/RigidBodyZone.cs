using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyZone : MonoBehaviour
{

    private CharacterController _characterController;
    private Rigidbody _rigidbody;
    private CapsuleCollider _capsuleCollider;
    private bool _inRigidbodyZone;
    private Vector3 _direction;

    private Animator _animator;
    private bool _hasAnimator;

    private int _animIDSlide;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _hasAnimator = TryGetComponent(out _animator);
        AssignAnimationIDs();
        EnableRigidbody(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "rbZone")
        {
            //_direction = other.transform.eulerAngles;
            Debug.Log("Enter rigidbody zone");
            EnableRigidbody(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "rbZone")
        {
            Debug.Log("Exit rigidbody zone");
            EnableRigidbody(false);
        }
    }

    private void EnableRigidbody(bool value)
    {
        if (value)
        {
            _characterController.enabled = false;
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(new Vector3(1, -1, 0) * 4, ForceMode.Impulse);
            //_rigidbody.detectCollisions = true;
            //_capsuleCollider.enabled = true;
        }
        else
        {
            _characterController.enabled = true;
            _rigidbody.isKinematic = true;
            _rigidbody.detectCollisions = false;
            //_capsuleCollider.enabled = false;
        }
        if (_hasAnimator)
        {
            _animator.SetBool(_animIDSlide, value);
        }
    }

    private void AssignAnimationIDs()
    {
        _animIDSlide = Animator.StringToHash("Slide");
    }
}
