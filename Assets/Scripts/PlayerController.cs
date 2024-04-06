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
    private float p_jumpHeight = 22f;
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
        input.Player.Movement.performed += MovementPerformed;
        input.Player.Jump.performed += Jumping;
        input.Player.Jump.performed += JumpingCancelled;
        input.Player.Movement.canceled += MovementCancelled;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Debug.Log("jumped");
        //    p_rb2D.AddForce(Vector2.up * p_jumpHeight);
        //    p_isJumping = true;
        //}
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(moveVector.x * p_speed, p_rb2D.velocity.y);

        //movement.y = p_rb2D.velocity.y;
   
        p_rb2D.velocity = movement;
    
        //Debug.Log(p_rb2D.velocity.ToString());
    
    }



    private void MovementPerformed(InputAction.CallbackContext value)
    {
        moveVector.x = value.ReadValue<float>();
    }

    private void Jumping(InputAction.CallbackContext context)
    {
        Debug.Log("jump pressed");
        //p_rb2D.AddForce(Vector2.up * p_jumpHeight);
        p_rb2D.velocity = Vector2.up * p_jumpHeight;
        
    }

    private void JumpingCancelled(InputAction.CallbackContext context) 
    {
        moveVector.y = p_rb2D.velocity.y;
    }

    private void MovementCancelled(InputAction.CallbackContext value)
    {
        moveVector.x = 0.0f;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= MovementPerformed;
        input.Player.Jump.performed -= Jumping;
        input.Player.Jump.performed -= JumpingCancelled;
        input.Player.Movement.canceled -= MovementCancelled;

    }

}
