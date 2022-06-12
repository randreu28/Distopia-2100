using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDestroyer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _buildingObjs;

    [SerializeField]
    private GameObject[] _enemies;

    [SerializeField]
    private float _delay;

    [SerializeField]
    private OpenDoor _door;

    [SerializeField]
    private GameObject _camZone;

    // Start is called before the first frame update
    void Start()
    {
        _camZone.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Action() {
        StartCoroutine(DestroyBuilding());
    }

    private IEnumerator DestroyBuilding() {
        _camZone.SetActive(true);
        yield return new WaitForSeconds(_delay);
        _door._canOpen = true;
        for (int i = 0; i < _buildingObjs.Length; i++)
        {
            _buildingObjs[i].AddComponent<Rigidbody>();
            //Debug.Log("Obj: " + _buildingObjects[i].name);
        }
        for (int i = 0; i < _enemies.Length; i++) {
            _enemies[i].SetActive(false);
        }
        yield return null;
    }

}
