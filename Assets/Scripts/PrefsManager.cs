using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PrefsManager : MonoBehaviour
{
    [SerializeField] protected SceneInfo sceneInfo;
    private int index;
    // Start is called before the first frame update
    public void SaveAll()
    {
        PlayerPrefs.SetString("EnemyType", sceneInfo.enemyType);
        PlayerPrefs.SetFloat("PosX", sceneInfo.playerPosition.x);
        PlayerPrefs.SetFloat("PosY", sceneInfo.playerPosition.y);
        PlayerPrefs.SetFloat("PosZ", sceneInfo.playerPosition.z);

        index = 0;
        foreach (var mob in sceneInfo.isDead)
        {
            PlayerPrefs.SetFloat("IsDeadX" + index.ToString(), mob.x);
            PlayerPrefs.SetFloat("IsDeadY" + index.ToString(), mob.y);
            PlayerPrefs.SetFloat("IsDeadZ" + index.ToString(), mob.z);
            index++;
        }
    }

    // Update is called once per frame
    public void LoadAll()
    {
        sceneInfo.enemyType = PlayerPrefs.GetString("EnemyType");
        sceneInfo.playerPosition = new Vector3(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"), PlayerPrefs.GetFloat("PosZ"));
        index--;
        while (index > -1)
        {
            sceneInfo.isDead.Add(new Vector3(PlayerPrefs.GetFloat("IsDeadX" + index.ToString()), PlayerPrefs.GetFloat("IsDeadY" + index.ToString()), PlayerPrefs.GetFloat("IsDeadZ" + index.ToString())));
            index--;
        }
    }

    public void DeleteAll()
    {
        sceneInfo.playerPosition = new Vector3(0,0,0);
        sceneInfo.lastTouched = new Vector3(0,0,0);
        sceneInfo.isDead.Clear();
        PlayerPrefs.DeleteAll();
    }
}
