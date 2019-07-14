using UnityEngine;

public class CharecterDirectionController : MonoBehaviour
{
    private SpriteRenderer _charecterSpriteRenderer = null;
    public Transform FirePoint = null;
    public bool FaceRight = true;

    void Start()
    {
        _charecterSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Flip()
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

    public Vector2 GetDirection2()
    {
        if (FaceRight)
            return Vector2.right;
        return Vector2.left;
    }

    public Vector3 GetDirection3()
    {
        if (FaceRight)
            return Vector3.right;
        return Vector3.left;
    }
}
