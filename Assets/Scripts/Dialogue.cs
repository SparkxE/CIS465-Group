using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;
public class Dialogue : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI textComponent;
    [SerializeField] protected string[] lines;

    [Tooltip("How many seconds between each character")]
    [SerializeField] protected float textSpeed;
    [SerializeField] protected GameObject canvas;
    [SerializeField] protected GameObject player;
    private TouchManager inputManager;
    private CanvasGroup canvasGroup;
    protected float playerSpeed;
    private int index;
    private void Awake()
    {
        inputManager = gameObject.AddComponent<TouchManager>();
        canvasGroup = canvas.GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0; // hide UI

        playerSpeed = player.GetComponent<PlayerBehaviour>().GetSpeed(); // save current speed for later

        textComponent.text = string.Empty;
    }

    private void OnEnable() {
        inputManager.OnStartTouch += TapDialogue;
    }
    private void OnDisable() {
        inputManager.OnStartTouch -= TapDialogue;
    }
    private void TapDialogue(Vector2 position, float time){ //only the delegate needs these parameters
        if(canvas.activeInHierarchy == true){
            OnInteract();
        }
    }

    private void OnInteract()
    {
        StopAllCoroutines();
        if (textComponent.text.Length < lines[index].Length) // if line is not finished
        {
            textComponent.text = lines[index];
        }
        else // if line is finished
        {
            NextLine();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if this Object has collided with Player
        if (other.gameObject.GetComponent<PlayerBehaviour>())
        {
            Debug.Log("trigger entered");
            // set Player speed to 0
            player.GetComponent<PlayerBehaviour>().SetSpeed(0); // freeze player
            // set Canvas active
            textComponent.text = string.Empty; // clear text, just in case
            canvasGroup.alpha = 1; // show ui

            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void NextLine()
    {
        textComponent.text = string.Empty;
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else // if no more lines to output, close dialogue
        {
            index = 0;
            canvasGroup.alpha = 0; // hide UI
            player.GetComponent<PlayerBehaviour>().SetSpeed(playerSpeed); // unfreeze player
        }
    }
}
