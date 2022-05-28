using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointChanger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _spawnPoints;
    private PlayerController _playerController;
    private CharacterController _characterController;

    private int _current = 0;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _characterController = GetComponent<CharacterController>();
        transform.position = _spawnPoints[0].transform.position;
        _playerController._lastGroundedPositionY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnChangeSpawnPoint() {
        if (_current < _spawnPoints.Length-1)
        {
            _current++;
        }
        else {
            _current = 0;
        }
        _characterController.enabled = false;
        transform.position = _spawnPoints[_current].transform.position;
        _playerController._lastGroundedPositionY = transform.position.y;
        _characterController.enabled = true;
    }
}
