using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI textComponent;
    [SerializeField] protected string[] lines;
    [SerializeField] protected float textSpeed;
    protected GameObject canvas;
    protected GameObject player;
    protected float playerSpeed;
    private int index;
    // Start is called before the first frame update
    private void Start()
    {
        canvas = GameObject.Find("Sign/Canvas");
        canvas.SetActive(false);

        player = GameObject.Find("Player");
        playerSpeed = player.GetComponent<PlayerBehaviour>().GetSpeed();

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
        if (textComponent.text == lines[index])
        {
            NextLine();
        }
        else
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
            player.GetComponent<PlayerBehaviour>().SetSpeed(0);
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
            index = 0;
            canvas.SetActive(false);
            player.GetComponent<PlayerBehaviour>().SetSpeed(playerSpeed);
        }
    }
}
