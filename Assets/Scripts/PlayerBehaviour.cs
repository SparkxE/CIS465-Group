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

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = moveInput * speed;
    }
    
    private void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    public void SetSpeed(float inputSpeed)
    {
        speed = inputSpeed;
    }

    public float GetSpeed()
    {
        return speed;
    }
}