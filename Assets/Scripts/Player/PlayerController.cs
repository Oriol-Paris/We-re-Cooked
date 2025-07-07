using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // ---------------------------------------------- Variables ---------------------------------------------- \\

    [Header("References")]
    private CharacterController controller;
    private InputSystem_Actions playerControls;

    [Header("Layer")]
    private int playerLayerMask;

    private enum state { IDLE, MOVING, SPRINTING, CROUCHING, AIR };
    [Header("State")]
    [SerializeField] private state currentState = state.IDLE;
     
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    private float currentSpeed = 0f;
    [SerializeField] private float moveAcceleration = 5f;
    [SerializeField] private float moveDeceleration = 5f;
    private Vector3 lastDirection = Vector3.zero;
    [SerializeField] private float moveChangeDirectionDeceleration = 5f;
    [SerializeField] private float movementVectorVelocity = 5f;

    [Header("Sprint Settings")]
    [SerializeField] private float sprintSpeed = 7f;
    private bool sprinting = false;
    [SerializeField] private float sprintAcceleration = 5f;
    [SerializeField] private float sprintDeceleration = 5f;
    [SerializeField] private float sprintChangeDirectionDeceleration = 5f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    private bool jumping = false;
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    [SerializeField] private float jumpBufferTime = 0.5f;
    private float jumpBufferCounter;

    [Header("Crouch Settings")]
    [SerializeField] private float crouchSpeed = 5f;
    private bool crouching = false;
    [SerializeField] private float crouchAcceleration = 5f;
    [SerializeField] private float crouchDeceleration = 5f;
    [SerializeField] private float crouchChangeDirectionDeceleration = 5f;
    [SerializeField] private float crouchHeight = 1f;
    [SerializeField] private float usualHeight = 2f;
    [SerializeField] private float crouchingSpeed = 5f;
    [SerializeField] private float uncrouchSpeed = 5f;
    private bool canGetUp = true;

    [Header("Input")]
    private float forwardInput;
    private float sidewaysInput;

    [Header("Camera")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform head;

    [Header("Gravity")]
    [SerializeField] private float gravity = 9.81f;
    private float verticalVelocity;

    // ---------------------------------------------- Awake & Start ---------------------------------------------- \\

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        playerControls = new InputSystem_Actions();
        playerControls.Player.Enable();

        playerLayerMask = ~LayerMask.GetMask("Player");
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // ---------------------------------------------- Enable ---------------------------------------------- \\

    private void OnEnable()
    {
        // Jump
        playerControls.Player.Jump.started += OnJumpStarted;

        // Sprint
        playerControls.Player.Sprint.started += OnSprintStarted;
        playerControls.Player.Sprint.canceled += OnSprintCancelled;

        // Crouch
        playerControls.Player.Crouch.started += OnCrouchStarted;
        playerControls.Player.Crouch.canceled += OnCrouchCancelled;
    }

    private void OnDisable()
    {
        // Jump
        playerControls.Player.Jump.started -= OnJumpStarted;

        // Sprint
        playerControls.Player.Sprint.started -= OnSprintStarted;
        playerControls.Player.Sprint.canceled -= OnSprintCancelled;

        // Crouch
        playerControls.Player.Crouch.started -= OnCrouchStarted;
        playerControls.Player.Crouch.canceled -= OnCrouchCancelled;
    }

    // ---------------------------------------------- Updates ---------------------------------------------- \\

    private void Update()
    {
        InputManagement();
        Move();
    }

    // ---------------------------------------------- Movement ---------------------------------------------- \\

    private void Move()
    {
        // Calculate Speed
        Vector3 inputDirection = new Vector3(sidewaysInput, 0, forwardInput).normalized;
        inputDirection = playerCamera.transform.rotation * inputDirection;

        float speed;
        float acceleration;
        float deceleration;
        float changeDirectionDeceleration;
        if (currentState == state.CROUCHING) {
            if (!crouching || sprinting) { canGetUp = CanGetUp(); } }

        if (sprinting && currentSpeed > 0 && canGetUp)
        {
            speed = sprintSpeed;
            acceleration = sprintAcceleration;
            deceleration = sprintDeceleration;
            changeDirectionDeceleration = sprintChangeDirectionDeceleration;
        }
        else if (crouching || !canGetUp)
        {
            speed = crouchSpeed;
            acceleration = crouchAcceleration;
            deceleration = crouchDeceleration;
            changeDirectionDeceleration = crouchChangeDirectionDeceleration;
        }
        else
        {
            speed = moveSpeed;
            acceleration = moveAcceleration;
            deceleration = moveDeceleration;
            changeDirectionDeceleration = moveChangeDirectionDeceleration;
        }

        if (inputDirection.magnitude > 0)
        {
            float dot = Vector3.Dot(lastDirection, inputDirection);

            if (dot < -0.1f)
            {
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0, changeDirectionDeceleration * 1.5f * Time.deltaTime);
                if (currentSpeed == 0)
                {
                    lastDirection = inputDirection;
                }
            }
            else
            {
                lastDirection = Vector3.MoveTowards(lastDirection, inputDirection, movementVectorVelocity * 1.5f * Time.deltaTime);
                currentSpeed = Mathf.MoveTowards(currentSpeed, speed, acceleration * Time.deltaTime);
            }
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
        }

        // Movement
        Vector3 movementDirection = lastDirection * currentSpeed;

        if (jumpBufferCounter > 0) { jumpBufferCounter -= Time.deltaTime; }
        CoyoteTime();
        if (jumping || jumpBufferCounter > 0) { Jump(); }
        movementDirection.y = VerticalVelocity() * jumpSpeed;

        controller.Move(movementDirection * Time.deltaTime);

        // State
        if (controller.isGrounded)
        {
            if (currentSpeed > 0)
            {
                if (sprinting && canGetUp) { currentState = state.SPRINTING; }
                else if (crouching || !canGetUp) { currentState = state.CROUCHING; }
                else { currentState = state.MOVING; }
            }
            else
            {
                if (crouching || !canGetUp) { currentState = state.CROUCHING; }
                else { currentState = state.IDLE; }
            }
        }
        else
        {
            currentState = state.AIR;
        }

        // Crouch
        if (currentState == state.CROUCHING) { Crouch(); }
        else { Uncrouch(); }
    }

    private float VerticalVelocity()
    {
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -0.5f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        return verticalVelocity;
    }

    private void InputManagement()
    {
        forwardInput = playerControls.Player.Move.ReadValue<Vector2>().y;
        sidewaysInput = playerControls.Player.Move.ReadValue<Vector2>().x;
    }

    // ---------------------------------------------- Sprint ---------------------------------------------- \\

    private void OnSprintStarted(InputAction.CallbackContext context)
    {
        sprinting = true;
    }

    private void OnSprintCancelled(InputAction.CallbackContext context)
    {
        sprinting = false;
    }

    // ---------------------------------------------- Jump ---------------------------------------------- \\

    private void Jump()
    {
        /*if (jumping)
        {
            jumpBufferCounter = jumpBufferTime;
            jumping = false;
        }

        if (coyoteTimeCounter > 0)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * gravity * 2);

            coyoteTimeCounter = 0f;
        }*/

    }

    private void OnJumpStarted(InputAction.CallbackContext context)
    {
        jumping = true;
    }

    private void CoyoteTime()
    {
        if (controller.isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    // ---------------------------------------------- Crouch ---------------------------------------------- \\

    private void Crouch()
    {
        controller.height = Mathf.MoveTowards(controller.height, crouchHeight, crouchingSpeed * 1.5f * Time.deltaTime);
        controller.center = new Vector3(0, controller.height / 2f, 0);
        head.localPosition = new Vector3(0, controller.height / 2f + 0.7f, 0);
    }

    private void Uncrouch()
    {
        controller.height = Mathf.MoveTowards(controller.height, usualHeight, uncrouchSpeed * 1.5f * Time.deltaTime);
        controller.center = new Vector3(0, controller.height / 2f, 0);
        head.localPosition = new Vector3(0, controller.height / 2f + 0.7f, 0);
    }

    private bool CanGetUp()
    {
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, Vector3.up, out hit, 2.0f, playerLayerMask)) { return true; }
        else { return false; }
    }

    private void OnCrouchStarted(InputAction.CallbackContext context)
    {
        crouching = true;
    }

    private void OnCrouchCancelled(InputAction.CallbackContext context)
    {
        crouching = false;
    }
}
