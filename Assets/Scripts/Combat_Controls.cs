using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UIElements;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

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

    //variables & objects for attacks
    private RaycastHit2D hit;
    [SerializeField] private float attackBuffer = .6f;
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] private float attackDamage = 1f;
    private Touch activeTouch;
    private bool isBusy;
    private float attackTimer;
    private WaitForSeconds timeToTap;
    private float tapLimit = 0.2f;
    private Animator animator;
    // private Combat_Inputs combatInputs;

    //stores X-axis input value
    private float inputX;

    private void Awake() {
        // combatInputs = new Combat_Inputs();
        EnhancedTouchSupport.Enable();
        timeToTap = new WaitForSeconds(tapLimit);
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate(){
        //Update player's current velocity based on previous inputs, check if the player is touching the ground
        playerBody.velocity = new Vector2(inputX * moveSpeed, playerBody.velocity.y); //moves player based on value returned from Move method
        if(inputX != 0){
            animator.SetBool("isWalking", true);
            isBusy = true;
        }
        else{
            animator.SetBool("isWalking", false);
            isBusy = false;
        }
        touchingGround = Physics2D.OverlapCircle(groundContact.position, .2f, whatIsGround);
    }

    private void Update() {
        //if the player is single-tapping the screen, start coroutine for neutral attack action
        if(Touch.activeFingers.Count == 1 && attackTimer >= attackBuffer){
            activeTouch = Touch.activeFingers[0].currentTouch;
            if(activeTouch.phase == UnityEngine.InputSystem.TouchPhase.Ended && isBusy == false){
                attackTimer = 0;
                StartCoroutine(DelayTouchStart());
                isBusy = true;
            }
            if(activeTouch.phase == UnityEngine.InputSystem.TouchPhase.Ended || activeTouch.phase == UnityEngine.InputSystem.TouchPhase.Canceled){
                StopCoroutine(DelayTouchStart());
                isBusy = false;
            }
        }
        attackTimer += Time.deltaTime;
    }

    private IEnumerator DelayTouchStart(){
        if(activeTouch.phase != UnityEngine.InputSystem.TouchPhase.Moved){
            //reset attack timer and trigger attack Animation & Function
            animator.SetTrigger("NeutralAttack");
            yield return new WaitForSeconds(0.3f);
            NeutralAttack();
        }
        yield break;
    }

    //Handles movement inputs
    public void Move(InputAction.CallbackContext context){
        //set X-axis movement value based on action value
        inputX = context.ReadValue<Vector2>().x;
        if(inputX<0){
            transform.rotation = new Quaternion(0,180,0,0);
        }
        else if(inputX>0){
            transform.rotation = new Quaternion(0,0,0,0);
        }
    }

    public void JumpAttack(InputAction.CallbackContext context){
        if(touchingGround){
            //apply jumping force and animation
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);
        }
    }

    private void NeutralAttack(){
        // Debug.Log("Attacked");
        hit = Physics2D.CircleCast(attackTransform.position, attackRange, transform.right, 0f, attackLayer);
        EnemyHealth enemyHealth = hit.collider.gameObject.GetComponent<EnemyHealth>();
        //if an enemy is found
        if(enemyHealth != null){
            //apply damage
            enemyHealth.Damage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }
}
