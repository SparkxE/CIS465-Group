using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Combat_Controls : MonoBehaviour
{
    #region Events
        public delegate void StartTouch(Vector2 position, float time);
        public event StartTouch OnStartTouch;
        public delegate void EndTouch(Vector2 position, float time);
        public event EndTouch OnEndTouch;
    #endregion

    //Rigidbody reference to allow changes to player character based on movement inputs
    [SerializeField] protected Rigidbody2D playerBody;

    //Reference to Combat_Inputs ActionMap object for swipe controls
    private Combat_Inputs combat_Inputs;
    private Camera mainCamera;

    //stores default movement speed
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 3f;
    private bool touchingGround;
    public Transform groundContact;
    public LayerMask whatIsGround;

    //stores X-axis input value
    private float inputX;

    //Awake and Enable/Disable functions for Touchscreen Inputs
    private void Awake() {
        combat_Inputs = new Combat_Inputs();
        mainCamera = Camera.main;
    }

    private void OnEnable() {
        combat_Inputs.Enable();
    }
    private void OnDisable() {
        combat_Inputs.Disable();
    }

    //Runs on program start
    private void Start() {
        combat_Inputs.Touch.PrimaryTouch.started += ctx => StartTouchPrimary(ctx);
        combat_Inputs.Touch.PrimaryTouch.canceled += ctx => EndTouchPrimary(ctx);
    }

    //Detecting when Touch Input Starts
    private void StartTouchPrimary(InputAction.CallbackContext context){
        if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(mainCamera, combat_Inputs.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
    }
    //Detecting when Touch Input Ends
    private void EndTouchPrimary(InputAction.CallbackContext context){
        if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(mainCamera, combat_Inputs.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
    }
    //Returns current Touch position for trail rendering
    public Vector2 PrimaryPosition(){
        return Utils.ScreenToWorld(mainCamera, combat_Inputs.Touch.PrimaryPosition.ReadValue<Vector2>());
    }

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
