using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] 
    [Range(10, 20)]
    private float speed = 15;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 moveInput;

    //SceneInfo for tracking player's position
    [SerializeField] private SceneInfo sceneInfo;

    // Start is called before the first frame update
    private void Start()
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
    public float GetSpeed(){
        return speed;
    }
}