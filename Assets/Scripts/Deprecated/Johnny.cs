
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Johnny : MonoBehaviour
{
    public Transform FirePoint = null;

    private AnimationStatesController _animationStatesController = null;

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

        _charecterSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

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
            _animationStatesController.State = AnimationStatesController.StatesEnum.Idle;
        else
            _animationStatesController.State = AnimationStatesController.StatesEnum.Jump;

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

    private bool GetIsGrounded()
    {
        // Todo: upgrade
        Vector2 direction = Vector2.down;
        float distance = 1.0f;

        //Hit left side
        Vector2 position = new Vector3(_charecterBoxCollider.bounds.min.x, transform.position.y, transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, 1 << 8);
        if (hit.collider != null)
        {
            return true;
        }

        //Hit center
        position.x = _charecterBoxCollider.bounds.center.x;
        hit = Physics2D.Raycast(position, direction, distance, 1 << 8);
        if (hit.collider != null)
        {
            return true;
        }

        //Hit right side
        position.x = _charecterBoxCollider.bounds.max.x;
        hit = Physics2D.Raycast(position, direction, distance, 1 << 8);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
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
    }

    private void EndShoot()
    {
        _isShooting = false;
        _animationStatesController.State = AnimationStatesController.StatesEnum.Idle;
    }
}
