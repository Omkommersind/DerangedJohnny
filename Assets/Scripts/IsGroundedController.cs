using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGroundedController : MonoBehaviour
{
    private BoxCollider2D _boxCollider = null;

    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }


    public bool GetIsGrounded()
    {
        // Todo: upgrade
        Vector2 direction = Vector2.down;
        float distance = 1.0f;

        //Hit left side
        Vector2 position = new Vector3(_boxCollider.bounds.min.x, transform.position.y, transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, 1 << 8);
        if (hit.collider != null)
        {
            return true;
        }

        //Hit center
        position.x = _boxCollider.bounds.center.x;
        hit = Physics2D.Raycast(position, direction, distance, 1 << 8);
        if (hit.collider != null)
        {
            return true;
        }

        //Hit right side
        position.x = _boxCollider.bounds.max.x;
        hit = Physics2D.Raycast(position, direction, distance, 1 << 8);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
}
