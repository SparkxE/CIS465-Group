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
    protected float playerSpeed;
    private int index;
    private bool dialogueActive;
    // Start is called before the first frame update
    private void Awake()
    {
        inputManager = gameObject.AddComponent<TouchManager>();
        dialogueActive = false;
        // canvas = GameObject.Find("Canvas"); // unsure how this interacts with multiple canvi
        canvas.SetActive(false);

        // player = GameObject.Find("Player");
        playerSpeed = player.GetComponent<PlayerBehaviour>().GetSpeed(); // save current speed for later

        textComponent.text = string.Empty;
        // StartDialogue(); //should probably delete this line if it's not being used
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

    // // Update is called once per frame
    // private void Update()
    // {
    //     if (Mouse.current.leftButton.wasPressedThisFrame) // change for global input (to work for tap)
    //     {
    //         OnInteract();
    //     }
    // }    //this function shouldn't be necessary (at least for now) since it's only used for text box progress

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
            // set Player speed to 0
            player.GetComponent<PlayerBehaviour>().SetSpeed(0); // freeze player
            // set Canvas active
            canvas.SetActive(true);
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        dialogueActive = true;
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
            dialogueActive = false;
            index = 0;
            canvas.SetActive(false);
            player.GetComponent<PlayerBehaviour>().SetSpeed(playerSpeed); // unfreeze player
        }
    }
}
