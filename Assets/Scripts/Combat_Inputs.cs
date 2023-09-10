using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Combat_Inputs : MonoBehaviour
{
    private Rigidbody2D playerBody;
    private Vector2 moveInput;
    //[SerializeField] private float moveSpeed = 1f;

    private void Start(){
        playerBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate(){
        //playerBody.MovePosition(playerBody.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        //^This code was borrowed from a tutorial on Top-down 2D movement, could/should get used in RPG movement script,
        // and likely needs editing in order to apply correctly for Combat system
    }

    public void Jump(){
        Debug.Log("Jump");
    }

}
