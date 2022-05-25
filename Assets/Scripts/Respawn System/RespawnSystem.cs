using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnSystem : MonoBehaviour
{
    public Transform SpawnPoint;
    public GameObject GameOver;
    public GameObject PauseMenu;

    private Animator _animator;
    private bool _hasAnimator;
    private int _animIDDie;
    private string _deadMessage;


    private void Start()
    {
        _hasAnimator = TryGetComponent(out _animator);
        AssignAnimationID();
    }

    public void KillPlayer(string message)
    {
        _deadMessage = message;
        if (_hasAnimator)
        {
            _animator.SetBool(_animIDDie, true);
        }
    }

    public void Dead() {
        _animator.SetBool(_animIDDie, false);
        gameObject.transform.position = SpawnPoint.position;
        Physics.SyncTransforms();
        Time.timeScale = 0;
        GameOver.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameObject.Find("DeathName").GetComponent<Text>().text = _deadMessage;
    }

    public void ChangeSpawn(Transform newSpawnPoint)
    {
        SpawnPoint = newSpawnPoint;
    }

    public void HandleFlag(GameObject Flag, float LightIntensity)
    {
        Flag.GetComponent<Renderer>().material.SetColor("_EmissiveColor", Color.red * LightIntensity);
    }

    private void AssignAnimationID()
    {
        _animIDDie = Animator.StringToHash("Die");
    }
}
