using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D p_rb2D;
    private CustomPlayerInput input;
    private CapsuleCollider2D capsuleCollider;
    private Vector2 moveVector;

    [SerializeField]
    private LayerMask groundLayerMask;

    private float p_speed = 15f;
    private float p_jumpHeight = 22f;
    private bool p_canJump = true;
    private bool p_isJumping = false;
    private int p_jumpCount;

    private void Awake()
    {
        p_rb2D = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
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
        if (p_jumpCount > 0)
        {
            p_canJump = true;
        }
        else
        {
            p_canJump = false;
        }

        GroundCheck();
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(moveVector.x * p_speed, p_rb2D.velocity.y);
  
        p_rb2D.velocity = movement;
       
    }



    private void MovementPerformed(InputAction.CallbackContext value)
    {
        moveVector.x = value.ReadValue<float>();
    }

    private void Jumping(InputAction.CallbackContext context)
    {
        Debug.Log("jump pressed");
        if (p_canJump)
        {
            p_rb2D.velocity = Vector2.up * p_jumpHeight;
            p_jumpCount--;
        }
        
    }

    private void GroundCheck()
    {
        float tolerance = 0.1f;
        RaycastHit2D boxCast = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size,0.0f, Vector2.down,tolerance,groundLayerMask);
        Color rayColour;
        if (boxCast.collider != null)
        {
            p_jumpCount = 1;
            //rayColour = Color.green;
        }
        else {
            //rayColour = Color.red;
        }

        //Debug.DrawRay(capsuleCollider.bounds.center + new Vector3(capsuleCollider.bounds.extents.x, 0), Vector2.down * (capsuleCollider.bounds.extents.y + tolerance), rayColour);
        //Debug.DrawRay(capsuleCollider.bounds.center - new Vector3(capsuleCollider.bounds.extents.x, 0), Vector2.down * (capsuleCollider.bounds.extents.y + tolerance), rayColour);
        //Debug.DrawRay(capsuleCollider.bounds.center - new Vector3(capsuleCollider.bounds.extents.x, capsuleCollider.bounds.extents.y), Vector2.right * (capsuleCollider.bounds.extents.x), rayColour);
        //Debug.Log(boxCast.collider);

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
