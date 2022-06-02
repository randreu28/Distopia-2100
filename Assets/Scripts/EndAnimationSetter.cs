using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnimationSetter : MonoBehaviour
{
    private bool _hasAnimator;
    private Animator _animator;
    private int _animIDEnd;

    // Start is called before the first frame update
    void Start()
    {        
        AssignAnimationIDs();
    }

    private void AssignAnimationIDs()
    {
        _animIDEnd = Animator.StringToHash("End");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            other.GetComponent<Animator>().SetBool(_animIDEnd, true);
        }
    }

}