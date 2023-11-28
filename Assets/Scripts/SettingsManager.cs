using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] protected GameObject menu;
    private CanvasGroup group;

    public void Awake()
    {
        group = menu.GetComponent<CanvasGroup>();
        group.alpha = 0;
    }
    public void OpenSettings()
    {
        group.alpha = 1;
    }

    public void OnDone()
    {
        group.alpha = 0;
    }
}
