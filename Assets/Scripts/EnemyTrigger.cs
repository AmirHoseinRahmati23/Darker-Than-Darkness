using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            var script = collision.gameObject.GetComponent<EnemyScript>();
            script.isRight = !script.isRight;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
