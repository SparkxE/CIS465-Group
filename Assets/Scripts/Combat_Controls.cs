using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)] //Sets this script to run before anything else unless specified
public class Combat_Controls : MonoBehaviour
{
    //Rigidbody reference to allow changes to player character based on movement inputs
    [SerializeField] protected Rigidbody2D playerBody;

    //stores default movement speed
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 3f;
    private bool touchingGround;
    public Transform groundContact;
    public LayerMask whatIsGround;

    //stores X-axis input value
    private float inputX;

    private void FixedUpdate(){
        playerBody.velocity = new Vector2(inputX * moveSpeed, playerBody.velocity.y); //moves player based on value returned from Move method

        touchingGround = Physics2D.OverlapCircle(groundContact.position, .2f, whatIsGround);
    }

    //Handles movement inputs
    public void Move(InputAction.CallbackContext context){
        inputX = context.ReadValue<Vector2>().x; //sets X-axis movement value based on action value
    }

    public void Jump(InputAction.CallbackContext context){
        if(touchingGround){
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);
        }
    }

}
