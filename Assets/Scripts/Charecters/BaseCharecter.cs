using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharecter : MonoBehaviour
{
    Rigidbody2D _rigidbody;

    void Start()
    {
        //Fetch the Rigidbody component from the GameObject
        _rigidbody = GetComponent<Rigidbody2D>();
        //Ignore the collisions between charecters
        Physics2D.IgnoreLayerCollision(9, 9, true);
    }
}
