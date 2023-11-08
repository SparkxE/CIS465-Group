using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestPuzzleBehaviour : MonoBehaviour
{
    private Behaviour dialogueScript;
    // Start is called before the first frame update
    void Awake()
    {
        dialogueScript = GetComponent<Dialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    public void DisableDialogue()
    {
        Destroy(dialogueScript);
    }

    private void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.tag == "CollisionBox")
        {
            if (col.gameObject.GetComponentInParent<PlayerBehaviour>().GetAxeStatus())
            {
                Destroy(this.gameObject);
            }
        }
    }
}
