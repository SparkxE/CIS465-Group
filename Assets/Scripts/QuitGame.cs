using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuitGame : MonoBehaviour
{
    [SerializeField] private SceneInfo sceneInfo;
    public void OnQuit()
    {
        sceneInfo.Reset();
        Invoke("Quit", 1f);
    }

    private void Quit(){
        Application.Quit();
    }
}
