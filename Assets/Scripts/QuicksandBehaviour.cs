using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuicksandBehaviour : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    private Vector3 spawn;
    // Start is called before the first frame update
    void Awake()
    {
        spawn = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if this Object has collided with Player
        if (other.gameObject.tag == "TriggerBox")
        {
            Debug.Log("quicksand encountered");
            player.GetComponent<PlayerBehaviour>().SetPosition(spawn);

            this.GetComponent<Dialogue>().AlternateTrigger();
        }
    }
}
