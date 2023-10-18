using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterCollisionBehaviour : MonoBehaviour
{
    [SerializeField] private SceneInfo sceneInfo;
    [SerializeField] private string enemyName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        sceneInfo.enemyType = enemyName;
        // if this Monster has collided with Player
        if (other.gameObject.GetComponent<PlayerBehaviour>())
        {
            SceneManager.LoadScene("Combat_Scene", LoadSceneMode.Single);
        }
    }
}
