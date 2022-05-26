using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    private Vector3 _endPosition;

    [SerializeField]
    private DoorController _door;

    [SerializeField]
    private float _duration;

    private PlayerController _player;

    private bool _isClose;
    private bool _pressed;

    [SerializeField]
    private Material _pressedMaterial;

    private Renderer _renderer;
    

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void action(PlayerController Player)
    {
        _player = Player;
        StopAllCoroutines();

        if (!_pressed)
        {
            StartCoroutine(PressButton());

            if (_door != null)
            {
                _door.Action(true);
            }

            _pressed = true;
            Debug.Log("Button Pressed");
        }
    }

    private IEnumerator PressButton()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition - _endPosition;
        _player.actionStart();
        for (float t = 0; t <= _duration; t += Time.deltaTime)
        {
            float x = Mathf.Clamp01(t / _duration);
            float f = 3 * Mathf.Pow(x, 2) - 2 * Mathf.Pow(x, 3);
            transform.position = Vector3.Lerp(startPosition, endPosition, f);
            yield return null;
        }
        _renderer.material = _pressedMaterial;
        _player.actionEnd();
        yield return null;
    }

}
