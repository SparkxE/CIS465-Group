using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private SceneInfo sceneInfo;
    private float currentHealth;
    private Animator animator;
    private GameObject target;
    [SerializeField] private Rigidbody2D enemyBody;
    private float distance;
    private bool isBusy = false;
    private bool takingDamage = false;
    [SerializeField] private float speed;
    [SerializeField] private float attackRange;
    private void Awake() {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        distance = Vector3.Distance(transform.position, target.transform.position);
    }
    
    private void Update() {
        if(distance > attackRange){
            if(transform.position.x < target.transform.position.x){
                enemyBody.velocity = new Vector2(speed, enemyBody.velocity.y);
            }
            else if(transform.position.x > target.transform.position.x){
                enemyBody.velocity = new Vector2(speed * -1, enemyBody.velocity.y);
            }
        }else if(distance < attackRange && isBusy == false){
            isBusy = true;
            Attack();
        }
    }

    private void Attack(){
        Debug.Log("Attacked");
    }

    public void Damage(float damageAmount){
        currentHealth -= damageAmount;
        animator.SetTrigger("Damage");
        if(transform.position.x < target.transform.position.x){
                Vector2 force = new Vector2(speed * -2, 0);
                enemyBody.AddForce(force, ForceMode2D.Impulse);
            }
            else if(transform.position.x > target.transform.position.x){
                Vector2 force = new Vector2(speed * 2, 0);
                enemyBody.AddForce(force, ForceMode2D.Impulse);
            }
        
        if(currentHealth <= 0){
            //die
            animator.SetTrigger("Death");
            Invoke("Die", 0.75f);
        }
    }

    private void Die(){
        Destroy(gameObject);
    }

    private void OnDestroy() {
        SceneManager.LoadScene("Overworld_Demo", LoadSceneMode.Single);
    }
}
