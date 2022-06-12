using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDestroyer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _buildingObjs;

    [SerializeField]
    private GameObject[] _enemies;

    [SerializeField]
    private int _delay;

    [SerializeField]
    private OpenDoor _door;

    [SerializeField]
    private GameObject _camZone;

    [SerializeField]
    private GameObject _countDownCanvas;

    [SerializeField]
    private Text _countDownText;

    private int _countDown;

    [SerializeField]
    private AudioClip _countDownAudioClip;

    [SerializeField]
    private AudioClip _killedSound;

    [SerializeField]
    private AudioClip _ExplosionSound;

    private AudioSource _audioSource;

    private bool _buildingExploted;

    private List<Vector3> _positions;
    private List<Vector3> _rotations;

    [SerializeField]
    [Range(0, 1)]
    public float _volume = 1f;

    [SerializeField]
    private ButtonController _button;

    [SerializeField]
    private GameObject _buildingDestroyedCollider;

    [SerializeField]
    private GameObject[] _fakeWalls;

    [SerializeField]
    private GameObject _realWall;

    // Start is called before the first frame update
    void Start()
    {
        _camZone.SetActive(false);
        _countDownCanvas.SetActive(false);
        _buildingDestroyedCollider.SetActive(false);
        _countDown = _delay;
        _audioSource = GetComponent<AudioSource>();

        _positions = new List<Vector3>();
        _rotations = new List<Vector3>();

        for (int i = 0; i < _buildingObjs.Length; i++)
        {
            _positions.Add(_buildingObjs[i].gameObject.transform.position);
            _rotations.Add(_buildingObjs[i].gameObject.transform.rotation.eulerAngles);
            _buildingObjs[i].AddComponent<Rigidbody>();
            _buildingObjs[i].GetComponent<Rigidbody>().isKinematic = true;
        }

        for (int i = 0; i < _fakeWalls.Length; i++)
        {
            _fakeWalls[i].SetActive(false);
        }
        _realWall.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Action() {
        StartCoroutine(DestroyBuilding());
    }

    private IEnumerator DestroyBuilding() {
        _countDownCanvas.SetActive(true);
        _camZone.SetActive(true);

        while (_countDown > 0) {

            _countDownText.text = _countDown.ToString();
            _audioSource.PlayOneShot(_countDownAudioClip, _volume);
            yield return new WaitForSeconds(1f);
            _countDown--;
        }
        _countDownCanvas.SetActive(false);
        _door._canOpen = true;
        _buildingExploted = true;
        for (int i = 0; i < _fakeWalls.Length; i++)
        {
            _fakeWalls[i].SetActive(true);
        }
        _realWall.SetActive(false);
        for (int i = 0; i < _buildingObjs.Length; i++)
        {
            _buildingObjs[i].GetComponent<Rigidbody>().isKinematic = false;
        }
        for (int i = 0; i < _enemies.Length; i++) {
            _enemies[i].SetActive(false);
        }
        _audioSource.PlayOneShot(_ExplosionSound, _volume);
        yield return null;
        yield return new WaitForSeconds(3f);
        _buildingDestroyedCollider.SetActive(true);
        yield return null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (_buildingExploted && other.tag == "Player") {
            other.GetComponent<RespawnSystem>().KillPlayer(DeadType.Killed, "Has muerto en la explosión", _killedSound, 1);
        }
    }

    public void Reset()
    {
        if (_buildingExploted) { 
            for (int i = 0; i < _buildingObjs.Length; i++)
            {
                _buildingObjs[i].GetComponent<Rigidbody>().isKinematic = true;
                _buildingObjs[i].gameObject.transform.position = _positions[i];
                _buildingObjs[i].gameObject.transform.rotation = Quaternion.Euler(_rotations[i]);
            }
            for (int i = 0; i < _enemies.Length; i++)
            {
                _enemies[i].SetActive(true);
            }
            _buildingDestroyedCollider.SetActive(false);
        }
        _button.Reset();
        StopAllCoroutines();
        _countDown = _delay;
        _countDownCanvas.SetActive(false);
        _buildingExploted = false;
    }

}
