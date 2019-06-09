using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharecter : MonoBehaviour
{
    public float Speed = 3f;
    public int HP = 3;
    public float JumpForce = 15f;

    internal Rigidbody2D CharecterRigidBody = null;
    internal BoxCollider2D CharecterBoxCollider = null;
    internal Animator CharecterAnimator = null;
    internal SpriteRenderer CharecterSpriteRenderer = null;

    // Start is called before the first frame update
    void Start()
    {
        CharecterBoxCollider = GetComponent<BoxCollider2D>();
        CharecterRigidBody = GetComponent<Rigidbody2D>();
        CharecterAnimator = GetComponent<Animator>();
        CharecterSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal bool GetIsGrounded()
    {
        // Todo: upgrade
        Vector2 direction = Vector2.down;
        float distance = 1.0f;

        //Hit left side
        Vector2 position = new Vector3(CharecterBoxCollider.bounds.min.x, transform.position.y, transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, 1 << 8);
        if (hit.collider != null)
        {
            return true;
        }

        //Hit right side
        position.x = CharecterBoxCollider.bounds.max.x;
        hit = Physics2D.Raycast(position, direction, distance, 1 << 8);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
}
