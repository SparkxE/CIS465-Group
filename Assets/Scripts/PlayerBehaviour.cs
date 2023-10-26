using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] 
    [Range(1, 10)]
    protected float speed = 5;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Vector2 moveInput;
    [SerializeField] private SceneInfo sceneInfo;
    private Animator animator;
    protected bool hasAxe = false;

    [SerializeField] protected GameObject darkForestPuzzle;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        animator = GetComponent<Animator>();
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if(scene.name == "Overworld_Demo"){
            if(transform.position != sceneInfo.playerPosition && sceneInfo.playerPosition != null){
                transform.position = sceneInfo.playerPosition;
            }
            else sceneInfo.playerPosition = transform.position;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * speed, moveInput.y * speed);
        if(moveInput != new Vector2(0,0)){
            animator.SetBool("isWalking", true);
        }
        else{
            animator.SetBool("isWalking", false);
        }
        sceneInfo.playerPosition = transform.position;
    }
    
    public void Move(InputAction.CallbackContext inputValue)
    {
        moveInput = inputValue.ReadValue<Vector2>().normalized;
        if(moveInput.x < 0){
            transform.rotation = new Quaternion(0,180,0,0);
        }
        else if(moveInput.x > 0){
            transform.rotation = new Quaternion(0,0,0,0);
        }
    }

    public void SetSpeed(float newSpeed){
        speed = newSpeed;
    }
    public float GetSpeed()
    {
        return speed;
    }

    public void PickupAxe()
    {
        darkForestPuzzle.GetComponent<ForestPuzzleBehaviour>().DisableDialogue();
        hasAxe = true;
    }

    public bool GetAxeStatus()
    {
        return hasAxe;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}