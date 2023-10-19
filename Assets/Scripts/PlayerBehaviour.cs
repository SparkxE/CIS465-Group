using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] 
    [Range(1, 10)]
    protected float speed = 5;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Vector2 moveInput;
    protected bool hasAxe = false;

    [SerializeField] protected GameObject darkForestPuzzle;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if(transform != sceneInfo.playerPosition && sceneInfo.playerPosition != null){
            transform.position = sceneInfo.playerPosition.position;
        }
        else sceneInfo.playerPosition = transform;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * speed, moveInput.y * speed);
        sceneInfo.playerPosition = transform;
    }
    
    public void Move(InputAction.CallbackContext inputValue)
    {
        moveInput = inputValue.ReadValue<Vector2>().normalized;
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

    public void PickupAxe()
    {
        darkForestPuzzle.GetComponent<ForestPuzzleBehaviour>().DisableDialogue();
        hasAxe = true;
    }

    public bool GetAxeStatus()
    {
        return hasAxe;
    }
}