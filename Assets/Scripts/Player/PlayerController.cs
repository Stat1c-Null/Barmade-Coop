using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed, runSpeed;
    private float activeMoveSpeed;
    private Vector3 moveDir, movement;
    public float jumpForce, gravityMod;

    public CharacterController charControl;

    public Transform groundCheck;
    private bool isGrounded;
    public LayerMask groundLayer;
    public float groundCheckDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //* Move direction is determined by mouse rotation
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        // * Sprinting
        if(Input.GetKey(KeyCode.LeftShift))
        {
            activeMoveSpeed = runSpeed;
        } else {
            activeMoveSpeed = moveSpeed;
        }

        float yVelocity = movement.y;
        Vector3 targetMovement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x)).normalized * activeMoveSpeed;//normalized makes this one whole value, so player won't move faster if pressing both forward and side button
        movement = Vector3.Lerp(movement, targetMovement, (moveDir.magnitude > 0 ? 4 : 4) * Time.deltaTime);//After move glide
        //movement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x)).normalized * activeMoveSpeed;

        //* Gravity
        /*if(!charControl.isGrounded)//Reset yvelocity value when player is on the ground
        {
            movement.y = yVelocity;
        } else {
            movement.y = 0f;
        }*/
        movement.y = yVelocity;
        if(charControl.isGrounded) {
            movement.y = 0f;
        }
        movement.y += Physics.gravity.y * Time.deltaTime * gravityMod;//Apply physics to player

        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundCheckDistance, groundLayer);//Check if player is on the ground

        //* Jumping
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            movement.y = jumpForce;
        }

        //* Move Player
        charControl.Move(movement * Time.deltaTime);
    }
}
