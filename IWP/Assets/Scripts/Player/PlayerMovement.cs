using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    
    private float rotationSpeed = 720f;
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

        if (characterController.isGrounded)
        {
            lastGroundTime = Time.time;
        }

        if (Input.GetButtonDown("Jump") && canJump)
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            animator.SetBool("isJumping", false);

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
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

        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;
        Physics.SyncTransforms();
        if (canMove)
        {
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
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.normal.y <= 0.6)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }
    }
}
