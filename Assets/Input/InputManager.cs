using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;

    public Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;

    public bool sprintPressed;
    public bool jumpPressed;

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
            playerControls.PlayerMovement.Sprint.performed += ctx => sprintPressed = ctx.ReadValueAsButton();
            playerControls.PlayerMovement.Jump.performed += ctx => jumpPressed = ctx.ReadValueAsButton();
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput= movementInput.x;
    }

    private void Update()
    {
        HandleAllInputs();
    }
}
