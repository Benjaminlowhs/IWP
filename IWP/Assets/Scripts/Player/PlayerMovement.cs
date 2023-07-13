using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    
    public float rotationSpeed = 720f;
    private float jumpSpeed = 5f;

    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;

    private float jumpButtonGracePeriod = 0.2f;
    private float? lastGroundTime;
    private float? jumpButtonPressedTime;

    PlayerStats playerstats;

    PlayerAttack playerAttack;

    private Animator animator;

    bool canJump;
    bool canMove;

    private bool isSliding;
    private Vector3 slopeSlideVelocity;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
        playerstats = GetComponent<PlayerStats>();
        canJump = true;
        playerAttack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerAttack.isAttacking)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }


        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * playerstats.speed;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        SetSlideVelocity();

        if (slopeSlideVelocity == Vector3.zero)
        {
            isSliding = false;
        }

        if (characterController.isGrounded)
        {
            lastGroundTime = Time.time;
        }

        if (Input.GetButtonDown("Jump")/* && canJump*/)
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundTime <= jumpButtonGracePeriod)
        {
            if (slopeSlideVelocity != Vector3.zero)
            {
                isSliding = true;
            }
            characterController.stepOffset = originalStepOffset;

            if (!isSliding)
            {
                ySpeed = -0.5f;
            }
            animator.SetBool("isJumping", false);

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod && !isSliding)
            {
                ySpeed = jumpSpeed;
                animator.SetBool("isJumping", true);
                jumpButtonPressedTime = null;
                lastGroundTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }

        //Vector3 velocity = movementDirection * magnitude;
        //velocity.y = ySpeed;
        Physics.SyncTransforms();
        if (canMove)
        {
            Vector3 velocity = movementDirection * magnitude;
            velocity.y = ySpeed;
            characterController.Move(velocity * Time.deltaTime);
        }

        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            if (canMove)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }


        // Test
        //if (!isSliding)
        //{
        //    Vector3 velocity = movementDirection * magnitude;
        //    velocity.y = ySpeed;

        //    characterController.Move(velocity * Time.deltaTime);
        //}

        if (isSliding)
        {
            Vector3 velocity = slopeSlideVelocity;
            velocity.y = ySpeed;

            characterController.Move(velocity * Time.deltaTime);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.normal.y <= 0.9)
        {
            slopeSlideVelocity = Vector3.ProjectOnPlane(new Vector3(0, ySpeed, 0), hit.normal);
            return;
        }

        slopeSlideVelocity = Vector3.zero;
    }

    private void SetSlideVelocity()
    {

    }
}
