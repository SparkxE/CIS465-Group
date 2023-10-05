using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] protected Transform target;
    protected Vector3 offset = new Vector3(0, 0, -10);
    // Start is called before the first frame update

    // Update is called once per frame
    private void Update()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.LookAt(target);
        }
    }
}
