using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed, runSpeed;
    private float activeMoveSpeed;
    private Vector3 moveDir, movement;

    public CharacterController charControl;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        if(Input.GetKey(KeyCode.LeftShift))
        {
            activeMoveSpeed = runSpeed;
        } else {
            activeMoveSpeed = moveSpeed;
        }

        float yVelocity = movement.y;
        movement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x)).normalized * activeMoveSpeed;//normalized makes this one whole value, so player won't move faster if pressing both forward and side button

        if(!charControl.isGrounded)//Reset yvelocity value when player is on the ground
        {
            movement.y = yVelocity;
        } else {
            movement.y = 0f;
        }

        movement.y += Physics.gravity.y * Time.deltaTime;//Apply physics to player

        charControl.Move(movement  * Time.deltaTime);
    }
}
