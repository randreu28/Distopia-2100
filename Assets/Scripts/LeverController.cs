using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField]
    private Transform Lever;
    [SerializeField]
    private Transform _door;
    [SerializeField]
    private float _duration;

    private PlayerController _player;

    private bool _isClose;
    private bool _isStartPoint;

    [SerializeField]
    private bool _isActive = true;

    public AudioClip _active;
    public AudioClip _inactive;

    private AudioSource audioSource;

    [SerializeField]
    private GameObject _materialsGameObject;

    [SerializeField]
    private Material[] _activeMaterials;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void action(PlayerController Player)
    {
        if (!_isActive)
        {
            audioSource.PlayOneShot(_inactive);
            return;
        }

        _player = Player;
        StopAllCoroutines();

        if (_isStartPoint)
        {
            Debug.Log("ToggleLever: 90");
            StartCoroutine(ToggleLever(-40, 1));
            BroadcastMessage("Action", _isStartPoint);
        }
        else {
            Debug.Log("ToggleLever: -90");
            StartCoroutine(ToggleLever(-120, 1));
            BroadcastMessage("Action", _isStartPoint);
        }

        _isStartPoint = !_isStartPoint;

        audioSource.PlayOneShot(_active);
    }

    private IEnumerator ToggleLever(float rotation, float duration)
    {
        Quaternion startPosition = Lever.transform.rotation;
        Quaternion endPosition = Quaternion.Euler(startPosition.x + rotation, 180, 90);
        _player.actionStart();
        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            float x = Mathf.Clamp01(t / duration);
            float f = 3 * Mathf.Pow(x, 2) - 2 * Mathf.Pow(x, 3);
            Lever.transform.rotation = Quaternion.Lerp(startPosition, endPosition, f);
            yield return null;
        }
        _player.actionEnd();
    }

    public void SetActive(bool value) {
        _isActive = value;
        if (_materialsGameObject != null) {
            _materialsGameObject.GetComponent<MeshRenderer>().materials = _activeMaterials;
        }
    }

}
