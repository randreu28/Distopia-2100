using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    
    public float travelTime;
    private Vector3 currentPos;
    private bool _moving = false;
    private float _currentTime;
    private bool isStartPoint = true;
    public bool _alwaysMoving = false;
    [SerializeField]
    private bool _startMovingOnTriggerEnter;

    [Header("Auto Return")]
    [SerializeField]
    private bool _autoReturn;
    [SerializeField]
    private float _autoReturnDelay = 5;
    private float _autoReturnTimer;

    [Header("Lever")]
    public Transform Lever;
    [SerializeField]
    private float _startLeverRotation = -90f;
    [SerializeField]
    private float _endLeverRotation = -30f;

    [Header("Buttons")]
    [SerializeField]
    public Transform ButtonUp;
    [SerializeField]
    public Transform ButtonDown;
    [SerializeField]
    private Material _buttonOn;
    private Material _buttonOff;

    [Header("Platform Sounds")]
    public AudioClip MovingAudioClip;
    [Range(0, 1)] public float MovingAudioVolume = 0.5f;
    public AudioClip ButtonAudioClip;
    [Range(0, 1)] public float ButtonAudioVolume = 0.5f;

    private PlayerController _player;

    void Start()
    {
        if (ButtonUp != null) { 
            _buttonOff = ButtonUp.GetChild(0).GetComponent<Renderer>().material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (_moving || _alwaysMoving) {
            float cycleStep = Time.time - _currentTime;

            currentPos = Vector3.Lerp(startPoint.position, endPoint.position,
            Mathf.Cos(cycleStep / travelTime * Mathf.PI * 2) * -.5f + .5f);
            transform.position = currentPos;
            if ((isStartPoint && cycleStep >= travelTime / 2) || (!isStartPoint && cycleStep >= travelTime))
            {
                _moving = false;
                isStartPoint = !isStartPoint;
                if (_autoReturn && !isStartPoint) {
                    _autoReturnTimer = Time.time;
                }
                if (ButtonUp != null && ButtonDown != null) {
                    ButtonUp.GetChild(0).GetComponent<Renderer>().material = _buttonOff;
                    ButtonDown.GetChild(0).GetComponent<Renderer>().material = _buttonOff;
                }
            }
        }
        else if (_autoReturn && !isStartPoint)
        {
            if (Time.time - _autoReturnTimer >= _autoReturnDelay) {
                if (ButtonDown != null) { 
                    ButtonDown.GetChild(0).GetComponent<Renderer>().material = _buttonOn;
                }
                _currentTime = Time.time - (travelTime / 2);
                _moving = true;
                if (MovingAudioClip != null)
                {
                    AudioSource.PlayClipAtPoint(MovingAudioClip, transform.position, MovingAudioVolume);
                }
            }
        }
    }

    public void action(PlayerController Player) {
        _player = Player;
        MovePlatform();
    }

    private void MovePlatform() {
        if (!_moving)
        {
            _moving = true;

            if (MovingAudioClip != null) { 
                AudioSource.PlayClipAtPoint(MovingAudioClip, transform.position, MovingAudioVolume);
            }

            if (isStartPoint)
            {
                Debug.Log("isStartPoint");
                _currentTime = Time.time;
                if (Lever != null) { 
                    StartCoroutine(MoveLever(_endLeverRotation, 0.5f));
                }
            }
            else
            {
                Debug.Log("isStartPoint NO");
                _currentTime = Time.time - (travelTime / 2);
                if (Lever != null)
                {
                    StartCoroutine(MoveLever(_startLeverRotation, 0.5f));
                }
            }
            
        }
    }

    private IEnumerator MoveLever(float rotation, float duration)
    {
        Quaternion startPosition = Lever.transform.rotation;
        Quaternion endPosition = Quaternion.Euler(rotation, 0, -90);
        _player.actionStart();
        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            float x = Mathf.Clamp01(t / duration);
            float f = 3 * Mathf.Pow(x, 2) - 2 * Mathf.Pow(x, 3);
            Lever.transform.rotation = Quaternion.Lerp(startPosition, endPosition, f);
            yield return null;
        }
        //_player.actionEnd();
    }

    public void GoUp(PlayerController Player) {
        Debug.Log("go up");
        _player = Player;
        _player.actionStart();
        if (isStartPoint) {
            ButtonUp.GetChild(0).GetComponent<Renderer>().material = _buttonOn;
            if (ButtonAudioClip != null)
            {
                AudioSource.PlayClipAtPoint(ButtonAudioClip, ButtonUp.transform.position, ButtonAudioVolume);
            }
            MovePlatform();
        }
    }

    public void GoDown(PlayerController Player)
    {
        _player = Player;
        _player.actionStart();
        if (!isStartPoint) {
            ButtonDown.GetChild(0).GetComponent<Renderer>().material = _buttonOn;
            if (ButtonAudioClip != null)
            {
                AudioSource.PlayClipAtPoint(ButtonAudioClip, ButtonDown.transform.position, ButtonAudioVolume);
            }
            MovePlatform();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_startMovingOnTriggerEnter) {
            MovePlatform();
        }
    }

}
