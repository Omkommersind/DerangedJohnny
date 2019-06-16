using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallExplosion : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private CircleCollider2D _exprosionRadiusCircleCollider = null;
 
 
    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _exprosionRadiusCircleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (!_particleSystem.IsAlive())
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Killable killableObject = col.gameObject.GetComponent("Killable") as Killable;
        if (killableObject != null)
        {
            killableObject.TakeDamage();
        }
    }
}
