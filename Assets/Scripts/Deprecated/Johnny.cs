﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Johnny : MonoBehaviour
{
    public Transform FirePoint = null;

    private AnimationStatesController _animationStatesController = null;
    private IsGroundedController _isGroundedController = null;

    private bool _faceRight = true;
    private bool _isJumping = false;
    private bool _isShooting = false;
    [SerializeField] private bool _isGrounded = false;

    public float Speed = 3f;
    public int HP = 3;
    public float JumpForce = 15f;

    private Rigidbody2D _charecterRigidBody = null;
    private BoxCollider2D _charecterBoxCollider = null;
    private SpriteRenderer _charecterSpriteRenderer = null;

    // Start is called before the first frame update
    void Start()
    {
        _charecterBoxCollider = GetComponent<BoxCollider2D>();
        _charecterRigidBody = GetComponent<Rigidbody2D>();
        _animationStatesController = GetComponent<AnimationStatesController>();
        _isGroundedController = GetComponent<IsGroundedController>();

        _charecterSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Awake() { }

    private void Update()
    {
        if (_animationStatesController.State == AnimationStatesController.StatesEnum.TakeDamage) return; //Cant control while suffering

        if (Input.GetButtonDown("Jump"))
            _isJumping = true;

        if (Input.GetButtonDown("Fire1"))
            _isShooting = true;
    }

    private void FixedUpdate()
    {
        _isGrounded = _isGroundedController.GetIsGrounded();

        if (_isGrounded)
            _animationStatesController.State = AnimationStatesController.StatesEnum.Idle;
        else
           _animationStatesController.State = AnimationStatesController.StatesEnum.Jump;

        if (Input.GetButton("Horizontal") && _animationStatesController.State != AnimationStatesController.StatesEnum.TakeDamage)
            Run();

        if (_isJumping && _isGrounded)
        {
            Jump();
        }
        else
        {
            _isJumping = false;
        }

        if (_isShooting && _isGrounded)
        {
            Shoot();
        }
        else
        {
            _isShooting = false;
        }
    }

    private void Run()
    {
        if (_isGrounded)
            _animationStatesController.State = AnimationStatesController.StatesEnum.Run;

        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Speed * Time.deltaTime);

        if (direction.x < 0 && _faceRight || direction.x > 0 && !_faceRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _faceRight = !_faceRight;
        _charecterSpriteRenderer.flipX = !_charecterSpriteRenderer.flipX;

        FirePoint.Rotate(Vector3.up, 180f);
        FirePoint.localPosition = new Vector3(-FirePoint.localPosition.x, FirePoint.localPosition.y, FirePoint.localPosition.z);
    }

    private void Jump()
    {
        _charecterRigidBody.AddForce(transform.up * JumpForce, ForceMode2D.Impulse);
        _isJumping = false;
    }

    private void Shoot()
    {
        _animationStatesController.State = AnimationStatesController.StatesEnum.Shoot;
        // Todo: after
        EndShoot();
    }

    private void EndShoot()
    {
        _isShooting = false;
        _animationStatesController.State = AnimationStatesController.StatesEnum.Idle;
    }
}
