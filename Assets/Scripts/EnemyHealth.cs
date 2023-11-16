using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    //setup vars for initial enemy spawn-in
    [SerializeField] private float maxHealth = 3f;
    // [SerializeField] private SceneInfo sceneInfo;    //I forgot what this was for, I'm sure I'll find a use for it later
    [SerializeField] private float attackDamage;
    private float currentHealth;
    private Animator animator;
    private GameObject target;
    private RaycastHit2D hit;
    [SerializeField] private Transform attackTransform;
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] private Rigidbody2D enemyBody;
    private float distance;
    private bool isBusy = false;    // whether an enemy is actively doing something (attacking), prevents spamming
    private bool beenHit = false;     // whether the current enemy has been hit, allows attacks to be interrupted
    
    // attack and movement variables
    [SerializeField] private float speed;
    [SerializeField] private float attackRange;
    [SerializeField] private float rangeBuffer;
    [SerializeField] private float attackBuffer;
    [SerializeField] private float attackSpeed;

    private void Awake() {
        //set health to max, get animator, target the Player
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void Update() {
        distance = Vector3.Distance(transform.position, target.transform.position);
        
        // if the player is OUT OF REACH for attack
        if(distance > attackRange + rangeBuffer){
            // if the player is RIGHT of the current enemy 
            if(transform.position.x < target.transform.position.x){
                enemyBody.velocity = new Vector2(speed, enemyBody.velocity.y);
                transform.rotation = new Quaternion(0,180,0,0);
                //animate walking on enemies that have walking animations
                if(gameObject.tag == "TundraBoss" || gameObject.tag == "ForestBoss"){
                    animator.SetBool("isWalking", true);
                }
            }
            // if the player is LEFT of the current enemy
            else if(transform.position.x > target.transform.position.x){
                enemyBody.velocity = new Vector2(speed * -1, enemyBody.velocity.y);
                transform.rotation = new Quaternion(0,0,0,0);
                //animate walking on enemies that have walking animations
                if(gameObject.tag == "TundraBoss" || gameObject.tag == "ForestBoss"){
                    animator.SetBool("isWalking", true);
                }
            }
        }
        // if the player is WITHIN reach for attack
        else if(distance < (attackRange + rangeBuffer) && isBusy == false){
            isBusy = true;
            // turn off walking animations
            if(gameObject.tag == "TundraBoss" || gameObject.tag == "ForestBoss"){
                animator.SetBool("isWalking", false);
            }
            Attack();
        }
    }

    private void Attack(){
        // bosses randomly choose between their 2 attack animations
        if(gameObject.tag == "TundraBoss" || gameObject.tag == "ForestBoss" || gameObject.tag == "DesertBoss"){
            int attackChoice = Random.Range(0,2);
            if(attackChoice == 0){
                animator.SetTrigger("Attack1");
                Invoke("DamagePlayer", attackSpeed);
            }
            else if(attackChoice == 1){
                animator.SetTrigger("Attack2");
                Invoke("DamagePlayer", attackSpeed);
            }
        }
        else{
            animator.SetTrigger("Attack");
            Invoke("DamagePlayer", attackSpeed);
        }
    }

    // function for resetting isBusy and beenHit back to false to prevent enemies from "locking up"
    private void ClearBusy(){
        isBusy = false;
        beenHit = false;
    }

    private void DamagePlayer(){
        // cast attack at the Player within the attackTransform radius
        hit = Physics2D.CircleCast(attackTransform.position, attackRange, transform.right, 0f, attackLayer);
        PlayerHealth playerHealth = hit.collider.gameObject.GetComponent<PlayerHealth>();
        if(playerHealth != null && beenHit == false){
            playerHealth.Damage(attackDamage);
        }
        Invoke("ClearBusy", attackBuffer);
    }

    public void Damage(float damageAmount){
        // take damage upon being hit by the player
        currentHealth -= damageAmount;
        animator.SetTrigger("Damage");
        beenHit = true;

        // trigger knockback on hit
        if(transform.position.x < target.transform.position.x){
                Vector2 force = new Vector2(speed * -25, 0);
                enemyBody.AddForce(force, ForceMode2D.Impulse);
            }
            else if(transform.position.x > target.transform.position.x){
                Vector2 force = new Vector2(speed * 25, 0);
                enemyBody.AddForce(force, ForceMode2D.Impulse);
            }
        // enemies die when they're killed
        if(currentHealth <= 0){
            //die
            animator.SetTrigger("Death");
            Invoke("Die", 0.75f);
        }
    }

    private void OnDrawGizmosSelected(){
        // draws a circle to ID attackTransform radius in Scene editor
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }

    private void Die(){
        Destroy(gameObject);
    }

    private void OnDestroy() {
        // load back to Overworld upon enemy death
        SceneManager.LoadScene("Overworld_Demo", LoadSceneMode.Single);
    }
}
