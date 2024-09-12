using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private Player player;
    private PlayerControls controls;
    private Rigidbody2D rigidbody2d;
    private Animator animator;

    [Header("Movement Info")]
    [SerializeField] private Vector2 moveInfo;
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float climbSpeed = 2f;
    [SerializeField] private int jumpForce;
    private bool canGoUp = false;
    int countJump = 2;
    void Start()
    {
        InitiateFields();
        ManageInput();
    }

    void Update()
    {
        ApplyMovement();
        HandleAnimation();
        PlayerFlip();
    }
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") //Jump Reset
        {
            countJump = 2;
        }
    }
    public void SetCanGoUp(bool canGoUp)
    {
        this.canGoUp = canGoUp;
    }
    private void ManageInput()
    {
        controls.Movement.Walking.performed += ctx => moveInfo = ctx.ReadValue<Vector2>();
        controls.Movement.Walking.canceled += ctx => moveInfo = Vector2.zero;

        controls.Movement.Jumping.performed += ctx => Jump();
    }

    private void InitiateFields()
    {
        player = GetComponent<Player>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

    private void Jump()
    {
        if (countJump <= 2 && countJump != 0)
        {
            rigidbody2d.velocity = jumpForce * Vector2.up;
            countJump--;
        }
    }

    private void PlayerFlip()
    {
        if(moveInfo.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveInfo.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void HandleAnimation()
    {
        animator.SetBool("IsRunning", moveInfo.x != 0); // Set to walking animation
    }
}
