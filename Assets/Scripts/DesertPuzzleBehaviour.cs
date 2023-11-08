using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertPuzzleBehaviour : MonoBehaviour
{
    [SerializeField] protected GameObject canvas;
    private CanvasGroup canvasGroup;
    void Awake()
    {
        canvasGroup = canvas.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "TriggerBox")
        {
            canvasGroup.alpha = 1; // show warning
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "TriggerBox")
        {
            canvasGroup.alpha = 0; // hide warning
        }
    }
}
