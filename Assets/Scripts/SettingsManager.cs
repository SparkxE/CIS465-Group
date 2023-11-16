using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] protected GameObject menu;

    public void OpenSettings()
    {
        menu.SetActive(true);
    }

    public void OnDone()
    {
        menu.SetActive(false);
    }
}
