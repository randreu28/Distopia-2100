using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnSystem : MonoBehaviour
{
    public Transform SpawnPoint;
    public GameObject GameOver;
    public GameObject PauseMenu;
    [HideInInspector] public bool isDead = false;
    
    private Animator _animator;
    private bool _hasAnimator;
    private int _animIDDie;
    private int _animIDCrouch;
    private int _animIDPull;
    private int _animIDPush;
    private int _animIDPress;
    private int _animIDFreeFall;
    private int _animIDSeen;
    private string _deadMessage;
    private bool cooldown = false;

    [SerializeField]
    NPC[] _enemies;


    private void Start()
    {
        _hasAnimator = TryGetComponent(out _animator);
        AssignAnimationID();
    }

    public void KillPlayer(DeadType deadType, string message, AudioClip audioClip, float volume)
    {
        if (!gameObject.GetComponent<PlayerController>().neverDie && !isDead && !gameObject.GetComponent<PlayerController>().boatTravel) {
            gameObject.GetComponent<PlayerController>().SetCanMove(false);
            isDead = true;
            if (audioClip)
            {
                if(TryGetComponent(out AudioSource SFX))
                {
                    SFX.clip = audioClip;
                    SFX.volume = volume;
                    SFX.Play();
                }else{
                    Debug.LogError("RespawnSystem is trying to access an unexisting audioSource");
                }
            }else{
                Debug.LogError("There is no SFX for this death");
            }
            _deadMessage = message;
            if (_hasAnimator)
            {
                if (deadType == DeadType.Killed)
                {
                    _animator.SetBool(_animIDDie, true);
                }
                else if (deadType == DeadType.Seen) {
                    _animator.SetBool(_animIDSeen, true);
                }
                _animator.SetBool(_animIDCrouch, false);
                _animator.SetBool(_animIDPull, false);
                _animator.SetBool(_animIDPush, false);
                _animator.SetBool(_animIDPress, false);
                _animator.SetBool(_animIDFreeFall, false);
            }
        }
    }

    public void Dead() {
        BroadcastMessage("FadeOut"); 
    }

    public void FadeOutEnd() {
        for (int i = 0; i < _enemies.Length; i++)
        {
            _enemies[i].Reset();
        }
        _animator.SetBool(_animIDDie, false);
        _animator.SetBool(_animIDSeen, false);
        gameObject.GetComponent<PlayerController>().SetCanMove(true);
        gameObject.transform.position = SpawnPoint.position;
        Physics.SyncTransforms();
        Time.timeScale = 0;
        GameOver.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameObject.Find("DeathName").GetComponent<Text>().text = _deadMessage;
        isDead = false;
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
        _animIDCrouch = Animator.StringToHash("Crouch");
        _animIDPull = Animator.StringToHash("Pulling");
        _animIDPush = Animator.StringToHash("Pushing");
        _animIDPress = Animator.StringToHash("PressButton");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDSeen = Animator.StringToHash("Seen");
    }

    
}
