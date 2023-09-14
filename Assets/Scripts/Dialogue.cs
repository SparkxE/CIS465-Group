using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI textComponent;
    [SerializeField] protected string[] lines;

    [Tooltip("How many seconds between each character")]
    [SerializeField] protected float textSpeed;
    protected GameObject canvas;
    protected GameObject player;
    protected float playerSpeed;
    private int index;
    // Start is called before the first frame update
    private void Start()
    {
        canvas = GameObject.Find("Canvas"); // unsure how this interacts with multiple canvi
        canvas.SetActive(false);

        player = GameObject.Find("Player");
        playerSpeed = player.GetComponent<PlayerBehaviour>().GetSpeed(); // save current speed for later

        textComponent.text = string.Empty;
        // StartDialogue();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) // change for global input (to work for tap)
        {
            OnInteract();
        }
    }

    private void OnInteract()
    {
        if (textComponent.text == lines[index]) // if more lines to be read
        {
            NextLine();
        }
        else // if no more lines
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if this Object has collided with Player
        if (other.gameObject.GetComponent<PlayerBehaviour>())
        {
            // set Player speed to 0
            player.GetComponent<PlayerBehaviour>().SetSpeed(0); // freeze player
            // set Canvas active
            canvas.SetActive(true);
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
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else // if no more lines to output, close dialogue
        {
            textComponent.text = string.Empty; // clean up text so new dialogue does not open with already populated lines
            index = 0;
            canvas.SetActive(false);
            player.GetComponent<PlayerBehaviour>().SetSpeed(playerSpeed); // unfreeze player
        }
    }
}
