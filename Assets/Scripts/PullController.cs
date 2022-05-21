using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullController : MonoBehaviour
{

    private Inputs _input;
    private Rigidbody _rbToPull;
    [Range(5f, 500f)] public float strength = 11f;
    public bool Action;
    private Animator _animator;
    private bool _hasAnimator;
    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<Inputs>();
        _hasAnimator = TryGetComponent(out _animator);
        _playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pushable")
        {
            _rbToPull = other.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!Action || _rbToPull == null || _rbToPull.isKinematic) return;
        
        Vector3 directionVector = new Vector3(_input.move.x, 0, _input.move.y);

        var distance = transform.position - _rbToPull.position;

        if (distance.x > 0 && directionVector.x > 0 || distance.x < 0 && directionVector.x < 0 || distance.z > 0 && directionVector.z > 0 || distance.z < 0 && directionVector.z < 0) {

            _playerController.Pulling = true;
            Debug.Log("Distance: " + distance);
            Debug.Log("DirectionVector: " + directionVector);

            _rbToPull.AddForce(directionVector * strength * Time.deltaTime, ForceMode.Impulse);

            if (_hasAnimator)
            {
                _animator.SetBool("Pulling", true);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Pushable")
        {
            _rbToPull = null;
            _playerController.Pulling = false;

            if (_hasAnimator)
            {
                _animator.SetBool("Pulling", false);
            }
        }
    }

    public void OnAction()
    {
        Debug.Log("Action");
        Action = true;
    }

    public void OnActionEnd()
    {
        Action = false;
    }
}