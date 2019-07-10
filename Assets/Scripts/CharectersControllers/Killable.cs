using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public float HP = 5f;
    public float InvincibilitySeconds = 3f;
    public ParticleSystem DieParticlesPrefab = null;

    private Animator _charecterAnimator = null;
    private AnimationStatesController _animationStatesController = null;

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
        _animationStatesController = GetComponent<AnimationStatesController>();
    }

    public void TakeDamage()
    {
        if (IsInvincible) return;

        _animationStatesController.State = AnimationStatesController.StatesEnum.TakeDamage;
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
        Instantiate(DieParticlesPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }


    private void StopInvincibility()
    {
        IsInvincible = false;
    }
}
