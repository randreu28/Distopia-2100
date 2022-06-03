using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnimationSetter : MonoBehaviour
{
    private bool _hasAnimator;
    private Animator _animator;

    private int _animIDEndLevel1;
    private int _animIDEnd;

    [SerializeField]
    private bool _level1;
    [SerializeField]
    private bool _level2;

    // Start is called before the first frame update
    void Start()
    {        
        AssignAnimationIDs();
    }

    private void AssignAnimationIDs()
    {
        _animIDEndLevel1 = Animator.StringToHash("EndLevel1");
        _animIDEnd = Animator.StringToHash("End");
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            if (_level1) { 
                other.GetComponent<Animator>().SetBool(_animIDEndLevel1, true);
            }
            if (_level2) {
                other.GetComponent<Animator>().SetBool(_animIDEnd, true);
            }
        }
    }

}