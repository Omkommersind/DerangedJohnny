using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGroundedController : MonoBehaviour
{
    public bool ShowGroundCollisionGizmo = false;

    private BoxCollider2D _boxCollider = null;
    private List<Vector2> _raycastGizmos = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }


    public bool GetIsGrounded()
    {
        // Todo: upgrade
        _raycastGizmos = new List<Vector2>();
        Vector2 direction = Vector2.down;
        float distance = 0.1f;

        //Hit left side
        Vector2 position = new Vector3(_boxCollider.bounds.min.x, _boxCollider.bounds.min.y, transform.position.z);
        _raycastGizmos.Add(position);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, 1 << 8);
        if (hit.collider != null)
        {
            return true;
        }

        //Hit center
        position.x = _boxCollider.bounds.center.x;
        _raycastGizmos.Add(position);
        hit = Physics2D.Raycast(position, direction, distance, 1 << 8);
        if (hit.collider != null)
        {
            return true;
        }

        //Hit right side
        position.x = _boxCollider.bounds.max.x;
        _raycastGizmos.Add(position);
        hit = Physics2D.Raycast(position, direction, distance, 1 << 8);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    void OnDrawGizmos()
    {
        //if (ShowGroundCollisionGizmo)
        //{
        //    Gizmos.color = Color.red;
        //    foreach (var pos in _raycastGizmos)
        //    {
        //        Gizmos.DrawLine(new Vector3(pos.x, pos.y + 0.2f, 0), pos);
        //        Gizmos.DrawLine(new Vector3(pos.x - 0.2f, pos.y + 0.1f, 0), pos);
        //        Gizmos.DrawLine(new Vector3(pos.x + 0.2f, pos.y + 0.1f, 0), pos);
        //    }
        //}
    }
}
