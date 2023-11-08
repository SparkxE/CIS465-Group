using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
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
    private bool isBusy = false;
    [SerializeField] private float speed;
    [SerializeField] private float attackRange;
    [SerializeField] private float rangeBuffer;
    [SerializeField] private float attackBuffer;
    [SerializeField] private float attackSpeed;

    private void Awake() {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void Update() {
        distance = Vector3.Distance(transform.position, target.transform.position);
        if(distance > attackRange + rangeBuffer){
            if(transform.position.x < target.transform.position.x){
                enemyBody.velocity = new Vector2(speed, enemyBody.velocity.y);
                transform.rotation = new Quaternion(0,180,0,0);
                if(gameObject.tag == "TundraBoss" || gameObject.tag == "ForestBoss"){
                    animator.SetBool("isWalking", true);
                }
            }
            else if(transform.position.x > target.transform.position.x){
                enemyBody.velocity = new Vector2(speed * -1, enemyBody.velocity.y);
                transform.rotation = new Quaternion(0,0,0,0);
                if(gameObject.tag == "TundraBoss" || gameObject.tag == "ForestBoss"){
                    animator.SetBool("isWalking", true);
                }
            }
        }else if(distance < (attackRange + rangeBuffer) && isBusy == false){
            isBusy = true;
            if(gameObject.tag == "TundraBoss" || gameObject.tag == "ForestBoss"){
                animator.SetBool("isWalking", false);
            }
            Attack();
        }
    }

    private void Attack(){
        if(gameObject.tag == "TundraBoss" || gameObject.tag == "ForestBoss" || gameObject.tag == "DesertBoss"){
            int attackChoice = Random.Range(0,2);
            if(attackChoice == 0){
                animator.SetTrigger("Attack1");
                Invoke("DamagePlayer", attackSpeed);
                Invoke("ClearBusy", attackBuffer);
            }
            else if(attackChoice == 1){
                animator.SetTrigger("Attack2");
                Invoke("DamagePlayer", attackSpeed);
                Invoke("ClearBusy", attackBuffer);
            }
        }
        else{
            animator.SetTrigger("Attack");
            Invoke("DamagePlayer", attackSpeed);
            Invoke("ClearBusy", attackBuffer);
        }
    }

    private void ClearBusy(){
        isBusy = false;
    }

    private void DamagePlayer(){
        hit = Physics2D.CircleCast(attackTransform.position, attackRange, transform.right, 0f, attackLayer);
        PlayerHealth playerHealth = hit.collider.gameObject.GetComponent<PlayerHealth>();
        if(playerHealth != null){
            playerHealth.Damage(attackDamage);
        }
    }

    public void Damage(float damageAmount){
        currentHealth -= damageAmount;
        animator.SetTrigger("Damage");
        if(transform.position.x < target.transform.position.x){
                Vector2 force = new Vector2(speed * -25, 0);
                enemyBody.AddForce(force, ForceMode2D.Impulse);
            }
            else if(transform.position.x > target.transform.position.x){
                Vector2 force = new Vector2(speed * 25, 0);
                enemyBody.AddForce(force, ForceMode2D.Impulse);
            }
        
        if(currentHealth <= 0){
            //die
            animator.SetTrigger("Death");
            Invoke("Die", 0.75f);
        }
    }

    private void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }

    private void Die(){
        Destroy(gameObject);
    }

    private void OnDestroy() {
        SceneManager.LoadScene("Overworld_Demo", LoadSceneMode.Single);
    }
}
