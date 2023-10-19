using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class TouchManager : MonoBehaviour
{
    #region Events
        public delegate void StartTouch(Vector2 position, float time);
        public event StartTouch OnStartTouch;
        
        public delegate void EndTouch(Vector2 position, float time);
        public event EndTouch OnEndTouch;
    #endregion

    //Reference to ActionMap objects for swipe controls
    private Combat_Inputs combatInputs;
    private RPGInput rpgInput;
    private Camera mainCamera;

    //Awake and Enable/Disable functions for Touchscreen Inputs
    private void Awake() {
        combatInputs = new Combat_Inputs();
        rpgInput = new RPGInput();
        mainCamera = Camera.main;
    }

    private void OnEnable() {
        if(SceneManager.GetActiveScene().name == "Overworld_Demo"){
            rpgInput.Enable();
        }
        else if(SceneManager.GetActiveScene().name == "Combat_Scene"){
            combatInputs.Enable();
        }
    }
    private void OnDisable() {
        if(SceneManager.GetActiveScene().name == "Overworld_Demo"){
            rpgInput.Disable();
        }
        else if(SceneManager.GetActiveScene().name == "Combat_Scene"){
            combatInputs.Disable();
        }
    }

    //Runs on program start
    private void Start() {
        if(SceneManager.GetActiveScene().name == "Overworld_Demo"){
            rpgInput.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx); //format of ActionMap reference is "actionInput.ActionMap.Action.state"
            rpgInput.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
        }
        else if(SceneManager.GetActiveScene().name == "Combat_Scene"){
            combatInputs.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx); //format of ActionMap reference is "actionInput.ActionMap.Action.state"
            combatInputs.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
        }
    }

    //Detecting when Touch Input Starts
    private void StartTouchPrimary(InputAction.CallbackContext context){
        if(SceneManager.GetActiveScene().name == "Overworld_Demo"){
            // Debug.Log("Touch Started " + rpgInput.Touch.PrimaryPosition.ReadValue<Vector2>());
            if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(mainCamera, rpgInput.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
        }
        else if(SceneManager.GetActiveScene().name == "Combat_Scene"){
            // Debug.Log("Touch Started " + combatInputs.Touch.PrimaryPosition.ReadValue<Vector2>());
            if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(mainCamera, combatInputs.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
        }
    }
    
    //Detecting when Touch Input Ends
    private void EndTouchPrimary(InputAction.CallbackContext context){
        if(SceneManager.GetActiveScene().name == "Overworld_Demo"){
            // Debug.Log("Touch ended.");
            if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(mainCamera, rpgInput.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
        }
        else if(SceneManager.GetActiveScene().name == "Combat_Scene"){
            // Debug.Log("Touch ended.");
            if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(mainCamera, combatInputs.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
        }
    }

    //Returns current Touch position for trail rendering
    public Vector2 PrimaryPosition(){
        //double check if this is proper or if this can be done better
        Vector2 touchPos = new Vector2(0,0);

        if(SceneManager.GetActiveScene().name == "Overworld_Demo"){
            touchPos = Utils.ScreenToWorld(mainCamera, rpgInput.Touch.PrimaryPosition.ReadValue<Vector2>());
        }
        else if(SceneManager.GetActiveScene().name == "Combat_Scene"){
            touchPos = Utils.ScreenToWorld(mainCamera, combatInputs.Touch.PrimaryPosition.ReadValue<Vector2>());
        }
        return touchPos;
    }
}
