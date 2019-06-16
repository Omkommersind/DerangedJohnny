using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStatesController : MonoBehaviour
{
    public enum StatesEnum {
        Idle = 0,
        Run = 1,
        Jump = 2,
        Shoot = 3,
        Die = 4
    }

    public StatesEnum State
    {
        get { return (StatesEnum)_charecterAnimator.GetInteger("State"); }
        set {
            _charecterAnimator.SetInteger("State", (int)value);
        }
    }

    private Animator _charecterAnimator = null;

    private void Awake()
    {
        _charecterAnimator = GetComponent<Animator>();
    }
}
