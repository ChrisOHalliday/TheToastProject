using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D p_rb2D;
    private CustomPlayerInput input;
    private Vector2 moveVector;

    private float p_speed = 15f;
    private float p_jumpHeight = 10f;
    private bool p_canJump = false;
    private bool p_isJumping = false;

    private void Awake()
    {
        p_rb2D = GetComponent<Rigidbody2D>();
        input = new CustomPlayerInput();
    }
    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += onMovementKeyPressed;
        input.Player.Movement.canceled += onMovementKeyLifted;
    }
    private void FixedUpdate()
    {
        p_rb2D.velocity = moveVector * p_speed;
        Debug.Log(p_rb2D.velocity.ToString());
    }

    private void onMovementKeyPressed(InputAction.CallbackContext value)
    {
        moveVector.x = value.ReadValue<float>();
    }

    private void onMovementKeyLifted(InputAction.CallbackContext value)
    {
        moveVector.x = 0.0f;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= onMovementKeyPressed;
        input.Player.Movement.performed -= onMovementKeyLifted;
    }

}
