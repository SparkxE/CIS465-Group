using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : Singleton<TouchManager> //Note: try to un-Singleton this as they make testing harder
{
    #region Events
        public delegate void StartTouch(Vector2 position, float time);
        public event StartTouch OnStartTouch;
        public delegate void EndTouch(Vector2 position, float time);
        public event EndTouch OnEndTouch;
    #endregion

    //Reference to Combat_Inputs ActionMap object for swipe controls
    private Combat_Inputs inputManager;
    private Camera mainCamera;
    //Awake and Enable/Disable functions for Touchscreen Inputs
    private void Awake() {
        inputManager = new Combat_Inputs();
        mainCamera = Camera.main;
    }

    private void OnEnable() {
        inputManager.Enable();
    }
    private void OnDisable() {
        inputManager.Disable();
    }

    //Runs on program start
    private void Start() {
        inputManager.Touch.PrimaryTouch.started += ctx => StartTouchPrimary(ctx); //format of ActionMap reference is "actionInput.ActionMap.Action.state"
        inputManager.Touch.PrimaryTouch.canceled += ctx => EndTouchPrimary(ctx);
    }

    //Detecting when Touch Input Starts
    private void StartTouchPrimary(InputAction.CallbackContext context){
        Debug.Log("Touch Started " + inputManager.Touch.PrimaryPosition.ReadValue<Vector2>());
        if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(mainCamera, inputManager.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
    }
    //Detecting when Touch Input Ends
    private void EndTouchPrimary(InputAction.CallbackContext context){
        Debug.Log("Touch ended.");
        if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(mainCamera, inputManager.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
    }
    //Returns current Touch position for trail rendering
    public Vector2 PrimaryPosition(){
        return Utils.ScreenToWorld(mainCamera, inputManager.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}
