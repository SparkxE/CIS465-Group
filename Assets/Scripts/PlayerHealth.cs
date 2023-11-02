using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5f;
    private float currentHealth;
    private Animator animator;
    private bool isDead = false;

    private void Awake() {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }
    public void Damage(float damageAmount){
        if(isDead == false){
            currentHealth -= damageAmount;
            animator.SetTrigger("Damage");
            if(currentHealth <= 0){
                //die
                animator.SetTrigger("Death");
                Invoke("Die", 1f);
                isDead = true;
            }
        }
    }

    private void Die(){

    }
}
