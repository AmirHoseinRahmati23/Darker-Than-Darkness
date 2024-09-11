using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private Player player;
    private PlayerControls controls;
    private Rigidbody2D rigidbody2d;

    [Header("Movement Info")]
    [SerializeField] private Vector2 moveInfo;
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float climbSpeed = 2f;
    private bool canGoUp = false;
    
    void Start()
    {
        InitiateFields();
        ManageInput();
    }

    void Update()
    {
        ApplyMovement();
    }
    public void SetCanGoUp(bool canGoUp)
    {
        this.canGoUp = canGoUp;
    }
    private void ManageInput()
    {
        controls.Movement.Walking.performed += ctx => moveInfo = ctx.ReadValue<Vector2>();
        controls.Movement.Walking.canceled += ctx => moveInfo = Vector2.zero;
    }

    private void InitiateFields()
    {
        player = GetComponent<Player>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        controls = player.controls;
    }

    private void ApplyMovement()
    {
        if (moveInfo.magnitude > 0)
        {
            var targetPosition =
                new Vector2(transform.position.x + moveInfo.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            
            if (canGoUp && moveInfo.y != 0)
            {
                rigidbody2d.gravityScale = 0;
                rigidbody2d.velocity = climbSpeed * ((moveInfo.y > 0)? Vector2.up: Vector2.down);
            }
            else
            {
                rigidbody2d.gravityScale = 1;
            }
        }
    }
}
