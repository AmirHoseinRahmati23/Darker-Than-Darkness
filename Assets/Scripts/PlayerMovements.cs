using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private Player player;
    private PlayerControls controls;
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private Transform cameraTransform;

    [SerializeField]
    public LogicManager Logic;
    [Header("Movement Info")]
    [SerializeField] private Vector2 moveInfo;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float climbSpeed = 2f;
    [SerializeField] private float jumpForce;
    private bool canGoUp = false;
    int countJump = 2;
    [Header("Combat")]
    [SerializeField] private GameObject sword;
    private bool isSwinging = false;
    void Start()
    {
        InitiateFields();
        ManageInput();
    }

    void Update()
    {
        if (Logic.gameIsActive)
        {
            ApplyMovement();
            HandleAnimation();
            PlayerFlip();
            RotateSword();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ground") //Jump Reset
        {
            bool isFromBelow = default;
            foreach (ContactPoint2D contactPoint in collision.contacts)
            {
                isFromBelow = contactPoint.normal.y > 0;
            }
            if (isFromBelow)
                ResetJumps();
        }
        if(collision.gameObject.layer == 9 || collision.gameObject.layer == 8)
        {
            GameOver();
        }
        if(collision.gameObject.layer == 10)
        {
            Logic.Winning();
        }
    }

    void ResetJumps()
    {
        countJump = 2;
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

        controls.Combat.Swing.performed += ctx => SwingSword();
        
    }

    private void SwingSword()
    {
        sword.SetActive(true);
        isSwinging = true;
    }
    private void RotateSword()
    {
        if (isSwinging)
        {
            var zAngle = sword.transform.rotation.eulerAngles.z;
            if (zAngle > 260 || zAngle <= 0)
            {
                sword.transform.Rotate(new Vector3(0, 0, -1000) * Time.deltaTime);
            }
            else
            {
                sword.transform.Rotate(0, 0, 100);
                sword.SetActive(false);
                isSwinging = false;
            }
        }
    }
    private void InitiateFields()
    {
        player = GetComponent<Player>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        controls = player.controls;
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        Logic = GameObject.FindGameObjectWithTag(nameof(Logic)).GetComponent<LogicManager>();
    }
    
    private void ApplyMovement()
    {
        if (moveInfo.magnitude > 0)
        {
            // Character Move in X direction
            var targetPosition =
                new Vector2(transform.position.x + moveInfo.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Camera Move with the player

            var cameraTargetX = (targetPosition.x < 0.57f) ? 0.57f : targetPosition.x;
            var cameraTargetPosition = new Vector3(cameraTargetX, cameraTransform.position.y, cameraTransform.position.z);
            cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, cameraTargetPosition, moveSpeed * Time.deltaTime);
            
            // Up and Down Movement(When Near The Rope)
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
        if (countJump <= 2 && countJump != 0 && Logic.gameIsActive)
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

    public void GameOver()
    {
        Logic.GameOver();
        Destroy(gameObject);
    }
}
