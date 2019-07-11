﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolem : MonoBehaviour
{
    private AnimationStatesController _animationStatesController;
    private IsGroundedController _isGroundedController;
    private WalkController _walkController;
    // private JumpController _jumpController;
    private ShootController _shootController;

    private BoxCollider2D _charecterBoxCollider;

    [SerializeField]
    private bool _isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        _charecterBoxCollider = GetComponent<BoxCollider2D>();
        _animationStatesController = GetComponent<AnimationStatesController>();
        _isGroundedController = GetComponent<IsGroundedController>();
        _walkController = GetComponent<WalkController>();
        // _jumpController = GetComponent<JumpController>();
        _shootController = GetComponent<ShootController>();

        _isGrounded = true;
    }

    private void Awake() { }

    private void Update()
    {
        if (_animationStatesController.State == AnimationStatesController.StatesEnum.TakeDamage) return; //Cant control while suffering

        /* if (Input.GetButtonDown("Jump"))
            _jumpController.IsJumping = true;

        if (Input.GetButtonDown("Fire1"))
            _shootController.IsShooting = true; */
    }

    private void FixedUpdate()
    {
        /*_isGrounded = _isGroundedController.GetIsGrounded();

        if (_isGrounded)
            _animationStatesController.State = AnimationStatesController.StatesEnum.Idle;
        else
            _animationStatesController.State = AnimationStatesController.StatesEnum.Jump;*/

        //if (Input.GetButton("Horizontal") && _animationStatesController.State != AnimationStatesController.StatesEnum.TakeDamage)
        _walkController.Run(-1, _isGrounded);

        // _jumpController.TryJump(_isGrounded);
        //_shootController.TryShoot();
    }
}
