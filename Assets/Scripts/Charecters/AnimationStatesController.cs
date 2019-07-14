using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStatesController : MonoBehaviour
{
    public float MinimumTakeDamageDuration = 0.2f;
    private bool _allowExitTakeDamageState = true;

    public enum StatesEnum {
        Idle = 0,
        Run = 1,
        Jump = 2,
        Shoot = 3,
        TakeDamage = 4,
        Die = 5
    }

    public StatesEnum State
    {
        get { return (StatesEnum)_charecterAnimator.GetInteger("State"); }
        set {
            if (State == StatesEnum.TakeDamage)
            {
                if (!_allowExitTakeDamageState || value != StatesEnum.Idle) return;
            }

            if (value == StatesEnum.TakeDamage)
            {
                _allowExitTakeDamageState = false;
                Invoke("AllowExitTakeDamageState", MinimumTakeDamageDuration);
            }

            _charecterAnimator.SetInteger("State", (int)value);
        }
    }

    private Animator _charecterAnimator = null;

    private void Awake()
    {
        _charecterAnimator = GetComponent<Animator>();
    }

    private void AllowExitTakeDamageState()
    {
        _allowExitTakeDamageState = true;
    }
}
