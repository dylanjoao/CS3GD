using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public Transform oreintation;

    Vector3 moveDirection;

    Rigidbody rigidBody;

    InputManager inputManager;

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        moveDirection = oreintation.forward * inputManager.verticalInput + oreintation.right * inputManager.horizontalInput;
        rigidBody.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }

}
