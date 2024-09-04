using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
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

        movement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x)).normalized;//normalized makes this one whole value, so player won't move faster if pressing both forward and side button

        charControl.Move(movement * moveSpeed * Time.deltaTime);
    }
}
