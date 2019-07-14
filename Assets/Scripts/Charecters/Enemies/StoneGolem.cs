using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolem : MonoBehaviour
{
    public bool ShowForwardRaycastPointsGizmos = false;

    private AnimationStatesController _animationStatesController;
    private IsGroundedController _isGroundedController;
    private WalkController _walkController;
    private CharecterDirectionController _directionController;
    private ShootController _shootController;
    private BoxCollider2D _charecterBoxCollider;

    private Vector2 _bottomForwardGizmo;
    private Vector2 _topForwardGizmo;

    //[SerializeField]
    private bool _isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        _charecterBoxCollider = GetComponent<BoxCollider2D>();
        _animationStatesController = GetComponent<AnimationStatesController>();
        _isGroundedController = GetComponent<IsGroundedController>();
        _walkController = GetComponent<WalkController>();
        _directionController = GetComponent<CharecterDirectionController>();
        _shootController = GetComponent<ShootController>();

        _bottomForwardGizmo = new Vector2();
        _topForwardGizmo = new Vector2();
    }

    private void Awake() { }

    private void Update()
    {
        if (_animationStatesController.State == AnimationStatesController.StatesEnum.TakeDamage) return; //Cant control while suffering
    }

    private void FixedUpdate()
    {
        _isGrounded = _isGroundedController.GetIsGrounded();

        if (_isGrounded)
            _animationStatesController.State = AnimationStatesController.StatesEnum.Idle;
        else
            _animationStatesController.State = AnimationStatesController.StatesEnum.Jump;

        if (IsObstacleAhead())
        {
            _directionController.Flip();
        }

        _walkController.Run(1, _isGrounded);

        // _jumpController.TryJump(_isGrounded);
        //_shootController.TryShoot();
    }

    // Todo: optimize
    private bool IsObstacleAhead()
    {
        Vector2 direction = _directionController.GetDirection2();
        float distance = 0.1f;
        float raycastPointOffset = 0.2f;

        if (direction.x > 0)
        {
            //Hit bottom
            Vector2 position = new Vector3(_charecterBoxCollider.bounds.max.x, _charecterBoxCollider.bounds.min.y + raycastPointOffset, transform.position.z);
            _bottomForwardGizmo = position;
            RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, 1 << 8);
            if (hit.collider != null)
            {
                return true;
            }

            //Hit top
            position.y = _charecterBoxCollider.bounds.max.y - raycastPointOffset;
            _topForwardGizmo = position;
            hit = Physics2D.Raycast(position, direction, distance, 1 << 8);
            if (hit.collider != null)
            {
                return true;
            }

            return false;
        } else
        {
            //Hit bottom
            Vector2 position = new Vector3(_charecterBoxCollider.bounds.min.x, _charecterBoxCollider.bounds.min.y + raycastPointOffset, transform.position.z);
            _bottomForwardGizmo = position;
            RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, 1 << 8);
            if (hit.collider != null)
            {
                return true;
            }

            //Hit top
            position.y = _charecterBoxCollider.bounds.max.y - raycastPointOffset;
            _topForwardGizmo = position;
            hit = Physics2D.Raycast(position, direction, distance, 1 << 8);
            if (hit.collider != null)
            {
                return true;
            }

            return false;
        }
    }

    void OnDrawGizmos()
    {
        if (ShowForwardRaycastPointsGizmos)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_bottomForwardGizmo, 0.1f);
            Gizmos.DrawSphere(_topForwardGizmo, 0.1f);
        }
    }
}
