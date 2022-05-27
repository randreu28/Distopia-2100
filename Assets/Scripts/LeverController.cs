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
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void action(PlayerController Player)
    {
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

}
