
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Johnny : BaseCharecter
{
    public enum StatesEnum { Idle, Run, Jump, Shoot }
    public StatesEnum State
    {
        get { return (StatesEnum)CharecterAnimator.GetInteger("State"); }
        set { CharecterAnimator.SetInteger("State", (int)value); }
    }

    public Transform FirePoint = null;

    private bool _faceRight = true;
    private bool _isJumping = false;
    private bool _isShooting = false;
    [SerializeField] private bool _isGrounded = false;


    private void Awake() { }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            _isJumping = true;
        if (Input.GetButtonDown("Fire1"))
            _isShooting = true;
    }

    private void FixedUpdate()
    {
        _isGrounded = GetIsGrounded();

        if (_isGrounded)
            State = StatesEnum.Idle;
        else
            State = StatesEnum.Jump;

        if (Input.GetButton("Horizontal"))
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
            State = StatesEnum.Run;

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
        CharecterSpriteRenderer.flipX = !CharecterSpriteRenderer.flipX;

        FirePoint.Rotate(Vector3.up, 180f);
        FirePoint.localPosition = new Vector3(-FirePoint.localPosition.x, FirePoint.localPosition.y, FirePoint.localPosition.z);
    }

    private void Jump()
    {
        CharecterRigidBody.AddForce(transform.up * JumpForce, ForceMode2D.Impulse);
        _isJumping = false;
    }

    private void Shoot()
    {
        State = StatesEnum.Shoot;
    }

    private void EndShoot()
    {
        _isShooting = false;
        State = StatesEnum.Idle;
    }
}
