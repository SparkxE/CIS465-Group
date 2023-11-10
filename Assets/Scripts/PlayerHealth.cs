using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5f;
    [SerializeField] private TMP_Text youDied;
    [SerializeField] private TMP_Text retry;
    [SerializeField] private TMP_Text healthDisplay;
    private float currentHealth;
    private Animator animator;
    private bool isDead = false;

    private void Awake() {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }
    private void Update() {
        healthDisplay.text = "HP: " + currentHealth;
    }
    public void Damage(float damageAmount){
        if(isDead == false){
            currentHealth -= damageAmount;
            animator.SetTrigger("Damage");
            if(currentHealth <= 0){
                //die
                animator.SetTrigger("Death");
                Invoke("Die", 1f);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                isDead = true;
            }
        }
    }

    private void Die(){
        Combat_Controls combatControls = gameObject.GetComponent<Combat_Controls>();
        combatControls.enabled = false;
        Invoke("YouDied", 1f);
    }

    private void YouDied(){
        youDied.text = "YOU DIED";
        retry.text = "Please Quit and Restart to Try Again";
    }
}
