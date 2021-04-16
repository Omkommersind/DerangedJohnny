using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject ExplosionPrefab = null;
    public float Speed = 20f;
    private Rigidbody2D _rb = null;
    private bool _isExploding = false;
    private bool _firstKillableCollision = true;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = transform.right * Speed;
    }

    private void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If collides with killable - explode immidiatelly
        var killable = collision.gameObject.GetComponent<Killable>();
        if (killable != null)
        {
            if (_firstKillableCollision)
            {
                // First collision always hits player
                _firstKillableCollision = false;
            } else
            {
                _isExploding = true;
                Invoke("Explode", 0);
            }
        }

        //ContactPoint2D contact = collision.contacts[0];
        //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        //Vector3 pos = contact.point;
        if (!_isExploding)
        {
            _isExploding = true;
            Invoke("Explode", 2);
        }
    }

    private void Explode()
    {
        Instantiate(ExplosionPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }
}
