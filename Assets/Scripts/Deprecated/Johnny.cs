
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Johnny : MonoBehaviour
{
    private AnimationStatesController _animationStatesController = null;
    private IsGroundedController _isGroundedController = null;
    private WalkController _walkController = null;

    private bool _isJumping = false;
    private bool _isShooting = false;
    [SerializeField] private bool _isGrounded = false;

    public int HP = 3;
    public float JumpForce = 15f;

    private Rigidbody2D _charecterRigidBody = null;
    private BoxCollider2D _charecterBoxCollider = null;

    // Start is called before the first frame update
    void Start()
    {
        _charecterBoxCollider = GetComponent<BoxCollider2D>();
        _charecterRigidBody = GetComponent<Rigidbody2D>();
        _animationStatesController = GetComponent<AnimationStatesController>();
        _isGroundedController = GetComponent<IsGroundedController>();
        _walkController = GetComponent<WalkController>();
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
            _walkController.Run(Input.GetAxis("Horizontal"), _isGrounded);

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
