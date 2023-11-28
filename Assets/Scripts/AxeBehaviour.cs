using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeBehaviour : MonoBehaviour
{
    private bool axePickedUp;
    // Start is called before the first frame update
    void Awake()
    {
        axePickedUp = false;
    }

    private void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.tag == "TriggerBox" && !axePickedUp)
        {
            col.gameObject.GetComponentInParent<PlayerBehaviour>().PickupAxe();
            axePickedUp = true;

            this.GetComponent<Dialogue>().AlternateTrigger();
        }
    }
}
