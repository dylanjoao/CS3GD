using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    #region References
    [Header("References")]
    CharacterController controller;
    InputManager inputManager;
    #endregion

    #region Camera
    public Cinemachine.AxisState xAxis, yAxis;
    [SerializeField] Transform camFollowPos;
    #endregion

    [Header("Movement")]
    Vector2 movement;
    public float walkSpeed;
    public float sprintSpeed;
    public Transform cam;
    float turnSmoothTime = .1f;
    float turnSmoothVel;
    float trueSpeed;

    [Header("Jump")]
    public float jumpHeight;
    public float gravity;
    bool isGrounded;
    Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = GetComponent<InputManager>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);

        isGrounded = Physics.CheckSphere(transform.position, .1f, 1);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -1;
        }

        if (inputManager.sprintPressed) {
            trueSpeed = sprintSpeed;
        }
        else {
            trueSpeed = walkSpeed;
        }

        movement = new Vector2(inputManager.horizontalInput, inputManager.verticalInput);
        Vector3 direction = new Vector3(movement.x, 0, movement.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * trueSpeed * Time.deltaTime);
        }

        if (inputManager.jumpPressed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (velocity.y > -20)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        
        controller.Move(velocity * Time.deltaTime);
    }

}
