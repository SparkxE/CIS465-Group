using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeDetection : MonoBehaviour
{
    //Variables for determining swipe direction and timing
    [SerializeField] private float minimumDistance = 0.2f;
    [SerializeField] private float maximumTime = 1f;
    [SerializeField, Range(0f, 1f)] private float directionThreshold = 0.7f;
    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    //GameObject & Coroutine for rendering Swipe Trail
    [SerializeField] private GameObject trail;
    private Coroutine coroutine;
    private float swipeLayer = -5;

    //TouchManager reference to recognize touch events
    private TouchManager inputManager;

    //Combat_Controls reference for calling swipe-based attack actions
    [SerializeField] private GameObject player;
    private Combat_Controls combatControls;

    //Setup functions for activating/disabling swipe detection
    private void Awake() {
        inputManager = gameObject.GetComponent<TouchManager>();
        combatControls = player.GetComponent<Combat_Controls>();
    }
    private void OnEnable() {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }
    private void OnDisable() {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    //Detects start of swipe and begins trail renderer
    private void SwipeStart(Vector2 position, float time){
        trail.SetActive(true);
        trail.transform.position = position;
        startPosition = position;
        startTime = time;
        coroutine = StartCoroutine(Trail());
    }

    //Coroutine for TrailRenderer
    private IEnumerator Trail(){
        while(true){
            trail.transform.position = new Vector3(inputManager.PrimaryPosition().x, inputManager.PrimaryPosition().y, swipeLayer);
            yield return null;
        }
    }

    //Detects end of swipe and ends trail renderer
    private void SwipeEnd(Vector2 position, float time){
        trail.SetActive(false);
        StopCoroutine(coroutine);
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    //Detects swipe direction and draws debug line between start & end points
    private void DetectSwipe(){
        if(Vector3.Distance(startPosition, endPosition) >= minimumDistance &&
            (endTime-startTime)<= maximumTime){
                Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
                Vector3 direction = endPosition - startPosition;
                Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
                SwipeDirection(direction2D);
            }
    }

    // swipe direction detection for swipe-based inputs
    private void SwipeDirection(Vector2 direction){
        //InputAction.CallbackContext context;
        if(Vector2.Dot(Vector2.up, direction) > directionThreshold){
            combatControls.JumpAttack();
        }
        if(Vector2.Dot(Vector2.down, direction) > directionThreshold){
            combatControls.SlideAttack();
        }
        if(Vector2.Dot(Vector2.left, direction) > directionThreshold){
            combatControls.DashAttack(true);
        }
        if(Vector2.Dot(Vector2.right, direction) > directionThreshold){
            combatControls.DashAttack(false);
        }
    }
}
