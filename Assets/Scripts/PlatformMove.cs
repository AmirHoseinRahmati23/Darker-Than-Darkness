using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            bool isFromBelow = default;
            foreach (ContactPoint2D contactPoint in collision.contacts)
            {
                isFromBelow = contactPoint.normal.y > 0;
            }
            Physics2D.IgnoreLayerCollision(6, 7, isFromBelow);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6) 
        {
            Physics2D.IgnoreLayerCollision(6, 7, false);
        }
    }
}
