using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{

    [SerializeField]
    private Transform _raycastPoint;

    [SerializeField]
    private LayerMask _obstaclesLayer;

    Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }

    private void Check() {

        if (!Physics.Raycast(_raycastPoint.position, Vector3.down, 1, _obstaclesLayer)) {
            _rigidbody.constraints = RigidbodyConstraints.None;
            StartCoroutine(FreezAgain());
        }
    }

    private IEnumerator FreezAgain() {
        yield return new WaitForSeconds(5);
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

}
