using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform FirePoint = null;
    public GameObject BulletPrefab = null;
    public float CooldownSeconds = 1;

    [HideInInspector]
    public bool IsShooting = false;

    private float _cooldownTimer = 1;

    private AnimationStatesController _animationStatesController = null;

    void Start()
    {
        _animationStatesController = GetComponent<AnimationStatesController>();
    }

    void Update()
    {
        _cooldownTimer += Time.deltaTime;
    }

    void FixedUpdate()
    {

        if (IsShooting)
        {
            if (_cooldownTimer < CooldownSeconds)
            {
                EndShoot();
            }
            else
            {
                _animationStatesController.State = AnimationStatesController.StatesEnum.Shoot;
                Shoot();
                EndShoot();
            }
        }
    }

    private void Shoot()
    {
        Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        _cooldownTimer = 0;
    }


    //public void TryShoot()
    //{
    //    if (IsShooting)
    //    {
            
    //        // Todo: after
    //        EndShoot();
    //    }
    //}

    private void EndShoot()
    {
        IsShooting = false;
        _animationStatesController.State = AnimationStatesController.StatesEnum.Idle;
    }
}
