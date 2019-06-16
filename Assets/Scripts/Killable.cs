using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public float HP = 5f;
    public float InvincibilitySeconds = 3f;

    private Animator _charecterAnimator = null;

    public bool IsInvincible
    {
        get { return _charecterAnimator.GetBool("IsInvincible"); }
        set
        {
            _charecterAnimator.SetBool("IsInvincible", value);
        }
    }

    private void Awake()
    {
        _charecterAnimator = GetComponent<Animator>();
    }

    public void TakeDamage()
    {
        if (IsInvincible) return;

        HP--;
        if (HP <= 0)
        {
            Die();
        }
        else
        {
            IsInvincible = true;
            Invoke("StopInvincibility", InvincibilitySeconds);
        }
    }

    private void Die()
    {
        // Todo: perish animation
        Destroy(gameObject);
    }


    private void StopInvincibility()
    {
        Debug.Log("Stop invincibility");
        IsInvincible = false;
    }
}
