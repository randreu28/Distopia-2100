using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public Transform Lever;
    public float travelTime;
    private Vector3 currentPos;
    private bool _moving = false;
    private float _currentTime;
    private bool isStartPoint = true;
    public AudioClip MovingAudioClip;
    [Range(0, 1)] public float MovingAudioVolume = 0.5f;
    private PlayerController _player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (_moving) {
            float cycleStep = Time.time - _currentTime;

            currentPos = Vector3.Lerp(startPoint.position, endPoint.position,
            Mathf.Cos(cycleStep / travelTime * Mathf.PI * 2) * -.5f + .5f);
            transform.position = currentPos;
            if ((isStartPoint && cycleStep >= travelTime / 2) || (!isStartPoint && cycleStep >= travelTime))
            {
                _moving = false;
                isStartPoint = !isStartPoint;
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

            AudioSource.PlayClipAtPoint(MovingAudioClip, transform.position, MovingAudioVolume);

            if (isStartPoint)
            {  
                _currentTime = Time.time;
                StartCoroutine(MoveLever(-30f, 0.5f));
            }
            else
            {
                _currentTime = Time.time - (travelTime / 2);
                StartCoroutine(MoveLever(-90f, 0.5f));
            }
            
        }
    }

    private IEnumerator MoveLever(float rotation, float duration)
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

}
