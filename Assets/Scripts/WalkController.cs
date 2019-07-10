using UnityEngine;

public class WalkController : MonoBehaviour
{
    public Transform FirePoint = null;
    public float Speed = 3f;

    [HideInInspector]
    public bool FaceRight = true;

    private SpriteRenderer _charecterSpriteRenderer = null;
    private AnimationStatesController _animationStatesController = null;

    private void Start()
    {
        _charecterSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animationStatesController = GetComponent<AnimationStatesController>();
    }

    public void Run(float value = 0, bool isGrounded = false)
    {
        if (isGrounded) // Allow move while jumping
            _animationStatesController.State = AnimationStatesController.StatesEnum.Run;

        Vector3 direction = transform.right * value;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Speed * Time.deltaTime);

        if (direction.x < 0 && FaceRight || direction.x > 0 && !FaceRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        FaceRight = !FaceRight;

        _charecterSpriteRenderer.flipX = !_charecterSpriteRenderer.flipX;

        // If charecter has fire point flip it
        if (FirePoint)
        {
            FirePoint.Rotate(Vector3.up, 180f);
            FirePoint.localPosition = new Vector3(-FirePoint.localPosition.x, FirePoint.localPosition.y, FirePoint.localPosition.z);
        }
    }

}
