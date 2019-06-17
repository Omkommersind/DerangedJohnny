using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallExplosion : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 5.0F;

    private bool _exploded = false;
    private ParticleSystem _particleSystem;
 
 
    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!_exploded)
        {
            Explode();
            _exploded = true;
        }

        if (!_particleSystem.IsAlive())
        {
            // Explosion particles animation finished
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        Vector3 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);
        foreach (Collider2D hit in colliders)
        {
            var rb = hit.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                var impulseDirection = new Vector2(transform.position.x - rb.position.x, 
                                                   transform.position.y - rb.position.y).normalized;
                rb.AddForce(-impulseDirection * power, ForceMode2D.Impulse);
            }
        }
    }
}
