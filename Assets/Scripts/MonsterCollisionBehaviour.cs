using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterCollisionBehaviour : MonoBehaviour
{
    [SerializeField] private SceneInfo sceneInfo;
    [SerializeField] private string enemyName;
    [SerializeField] protected Animator transition;

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if(scene.name == "Overworld_Demo"){
            if(sceneInfo.lastTouched == transform.position){
                sceneInfo.isDead.Add(transform.position);
            }
            if(sceneInfo.isDead.Contains(transform.position)){
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        sceneInfo.lastTouched = transform.position;
        sceneInfo.enemyType = enemyName;
        // if this Monster has collided with Player
        if (other.gameObject.tag == "CollisionBox")
        {
            StartCoroutine(LoadLevel(1));
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelIndex);
    }
}
