using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestPuzzleBehaviour : MonoBehaviour
{
    private Behaviour dialogueScript;
    void Awake()
    {
        dialogueScript = GetComponent<Dialogue>();
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
