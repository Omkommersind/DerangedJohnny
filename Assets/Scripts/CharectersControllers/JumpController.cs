using UnityEngine;

public class JumpController : MonoBehaviour
{
    public float JumpForce = 15f;

    [HideInInspector]
    public bool IsJumping = false;

    private Rigidbody2D _charecterRigidBody;

    void Start()
    {
        _charecterRigidBody = GetComponent<Rigidbody2D>();
    }

    public void TryJump(bool isGrounded = false)
    {
        if (IsJumping && isGrounded)
        {
            _charecterRigidBody.AddForce(transform.up * JumpForce, ForceMode2D.Impulse);
            IsJumping = false;
        }
        else
        {
            isGrounded = false;
        }
    }
}
