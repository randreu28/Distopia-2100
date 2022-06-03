using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    [SerializeField]
    private Vector3 _endPosition;

    [SerializeField]
    private float _duration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Action(bool isStartPoint) {
        StartCoroutine(OpenDoor(_duration));
    }

    private IEnumerator OpenDoor(float duration)
    {

        Vector3 DoorStartPosition = transform.position;

        Vector3 DoorEndPosition = new Vector3(DoorStartPosition.x + _endPosition.x, DoorStartPosition.y + _endPosition.y, DoorStartPosition.z + _endPosition.z);

        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            float x = Mathf.Clamp01(t / duration);
            float f = 3 * Mathf.Pow(x, 2) - 2 * Mathf.Pow(x, 3);

            transform.position = Vector3.Lerp(DoorStartPosition, DoorEndPosition, f);
            yield return null;
        }

        transform.position = DoorEndPosition;
    }

}
