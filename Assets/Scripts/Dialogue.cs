using System.Collections;
using UnityEngine;
using TMPro;
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
    private float lastTouchTime;
    private float delayThreshold;
    private bool isBusy;
    private bool thisDialogueActive;
    private void Awake()
    {
        inputManager = gameObject.AddComponent<TouchManager>();
        canvasGroup = canvas.GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0; // hide UI

        playerSpeed = player.GetComponent<PlayerBehaviour>().GetSpeed(); // save current speed for later

        textComponent.text = string.Empty;

        lastTouchTime = -1.0f;
        delayThreshold = 0.25f;
        isBusy = false;
        thisDialogueActive = false;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += TapDialogue;
    }
    private void OnDisable()
    {
        inputManager.OnStartTouch -= TapDialogue;
    }
    private void TapDialogue(Vector2 position, float time)
    { //only the delegate needs these parameters

        if(thisDialogueActive == true && time - lastTouchTime >= delayThreshold)
        {
            OnInteract();
        }
        lastTouchTime = time;
    }

    private void OnInteract()
    {
        StopAllCoroutines();
        if (textComponent.text.Length < lines[index].Length && isBusy == false) // if line is not finished
        {
            Debug.Log("line not finished, complete line");
            textComponent.text = lines[index];
            isBusy = true;
        }
        else if (textComponent.text.Length == lines[index].Length || isBusy == true) // if line is finished
        {
            Debug.Log("line finished, enter NextLine");
            NextLine();
            isBusy = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if this Object has collided with Player
        if (other.gameObject.tag == "CollisionBox")
        {
            Debug.Log("trigger entered");
            // set Player speed to 0
            player.GetComponent<PlayerBehaviour>().SetSpeed(0); // freeze player
            // set Canvas active
            thisDialogueActive = true;
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
        textComponent.text = string.Empty;
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
            thisDialogueActive = false;
            index = 0;
            canvasGroup.alpha = 0; // hide UI
            player.GetComponent<PlayerBehaviour>().SetSpeed(playerSpeed); // unfreeze player
        }
    }
}
