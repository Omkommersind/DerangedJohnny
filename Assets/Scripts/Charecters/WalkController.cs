using UnityEngine;

public class WalkController : MonoBehaviour
{
    public float Speed = 3f;

    private AnimationStatesController _animationStatesController = null;
    private CharecterDirectionController _directionController;

    private void Start()
    {
        _animationStatesController = GetComponent<AnimationStatesController>();
        _directionController = GetComponent<CharecterDirectionController>();
    }

    public void Run(float value = 0, bool isGrounded = false)
    {
        if (isGrounded) // Allow move while jumping
            _animationStatesController.State = AnimationStatesController.StatesEnum.Run;

        Vector3 direction = _directionController.GetDirection3() * System.Math.Abs(value);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Speed * Time.deltaTime);
    }
}
