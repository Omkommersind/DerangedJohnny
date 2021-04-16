using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetectionController : MonoBehaviour
{
    public Transform TargetDetectionSource = new RectTransform();
    public float TargetDetectionRadius = 5.0f;
    public BoxCollider2D EnemyCollider2D = new BoxCollider2D(); //Todo: multiple

    private bool _targetDetected = false;
    public bool TargetDetected
    {
        get { return _targetDetected; }
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        LookForTarget();
    }

    private void LookForTarget()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(TargetDetectionSource.position.x, TargetDetectionSource.position.y), TargetDetectionRadius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            var johnny = hitColliders[i].gameObject.GetComponent<Johnny>();
            if (johnny != null) {
                Console.WriteLine("Target detected");
                _targetDetected = true;
                return;
            }
            i++;
        }
        _targetDetected = false;
    }

}
