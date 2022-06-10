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
    [Range(0, 1)]
    public float _activeVolume = 1f;

    public AudioClip _inactive;
    [Range(0, 1)]
    public float _inactiveVolume = 1f;

    private AudioSource audioSource;

    [SerializeField]
    private GameObject _materialsGameObject;

    [SerializeField]
    private Material[] _activeMaterials;

    void Start()
    {
        if (TryGetComponent(out AudioSource myAudioSource))
        {
            audioSource = myAudioSource;
        }else {
            audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        }
        if(_isActive)
        {
            audioSource.volume = _activeVolume;
        }else
        {
            audioSource.volume = _inactiveVolume;
        }
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
        if(value == true)
        {
            audioSource.volume = _activeVolume;
        }else
        {
            audioSource.volume = _inactiveVolume;
        }
        if (_materialsGameObject != null) {
            _materialsGameObject.GetComponent<MeshRenderer>().materials = _activeMaterials;
        }
    }

}
