using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterCollisionBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if this Monster has collided with Player
        if (other.gameObject.GetComponent<PlayerBehaviour>())
        {
            SceneManager.LoadScene("Combat_Scene", LoadSceneMode.Single);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
