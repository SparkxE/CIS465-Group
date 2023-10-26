using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertPuzzleBehaviour : MonoBehaviour
{
    [SerializeField] protected GameObject image;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerBehaviour>()) // You can adjust the condition based on your player's tag
        {
            image.SetActive(true); // show warning
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerBehaviour>()) // You can adjust the condition based on your player's tag
        {
            image.SetActive(false); // hide warning
        }
    }
}
