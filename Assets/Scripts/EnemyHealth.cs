using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private SceneInfo sceneInfo;
    private float currentHealth;
    private void Awake() {
        currentHealth = maxHealth;   
    }

    public void Damage(float damageAmount){
        currentHealth -= damageAmount;
        if(currentHealth <= 0){
            //die
            Die();
        }
    }

    private void Die(){
        //Destroy(gameObject);
    }

    private void OnDestroy() {
        SceneManager.LoadScene("Overworld_Demo", LoadSceneMode.Single);
    }
}
