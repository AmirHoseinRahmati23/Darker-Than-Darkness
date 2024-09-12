using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove_ : MonoBehaviour
{
    public Vector3 playerYposition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.y < playerYposition.y)
            {

                this.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            if (collision.gameObject.transform.position.y > playerYposition.y)
            {

                this.GetComponent<BoxCollider2D>().isTrigger = false;
            } 
        }
    }
}
