using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private SceneInfo sceneInfo;
    private float currentHealth;
    private Animator animator;
    private void Awake() {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void Damage(float damageAmount){
        currentHealth -= damageAmount;
        animator.SetTrigger("Damage");
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
