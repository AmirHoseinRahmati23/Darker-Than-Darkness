using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [SerializeField]
    public LogicManager Logic;
    public bool isRight = true;
    // Start is called before the first frame update
    void Start()
    {
        Logic = GameObject.FindGameObjectWithTag(nameof(Logic)).GetComponent<LogicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Logic.gameIsActive)
        {
            Move(isRight);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
        {
            bool fromXdir = false;
            foreach(var contacts in collision.contacts)
            {
                fromXdir = contacts.normal.y == 0;
            }
            isRight = !isRight;
        }
    }

    private void Move(bool right)
    {
        Vector2 direction = 
            (right) ? new Vector2(transform.position.x + 1, transform.position.y) 
            : new Vector2(transform.position.x - 1, transform.position.y);
        transform.rotation = (right)? Quaternion.Euler(0, 180, 0) : transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = Vector2.MoveTowards(transform.position, direction, 5 * Time.deltaTime);
    }
}
