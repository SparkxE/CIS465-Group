using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRPGMove : MonoBehaviour
{
    private float moveSpeed = 8f;
    [SerializeField] protected Rigidbody2D rb;
    private Vector2 moveDirection;
    
    // Update is called once per frame
    void Update(){
        //Input Processing
        ProcessInputs();

    }

    //This function is called at a fixed framerate, 
    void FixedUpdate(){
        //Physics Calculations
        Move();
    }

    void ProcessInputs(){
        //Input Handling and Processing
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move(){
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    void OnAnimatorMove(){
        
    }
}
