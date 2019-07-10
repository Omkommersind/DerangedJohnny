using UnityEngine;

public class ShootController : MonoBehaviour
{
    // Todo: merge with Weapon controller
    private AnimationStatesController _animationStatesController = null;

    [HideInInspector]
    public bool IsShooting = false;

    void Start()
    {
        _animationStatesController = GetComponent<AnimationStatesController>();
    }

    public void TryShoot()
    {
        if (IsShooting)
        {
            _animationStatesController.State = AnimationStatesController.StatesEnum.Shoot;
            // Todo: after
            EndShoot();
        }
    }

    private void EndShoot()
    {
        IsShooting = false;
        _animationStatesController.State = AnimationStatesController.StatesEnum.Idle;
    }
}
