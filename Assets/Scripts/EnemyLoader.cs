using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoader : MonoBehaviour
{
    [SerializeField] private SceneInfo sceneInfo;
    private string enemyName;

    private void Awake() {
        enemyName = sceneInfo.enemyType;
        switch (enemyName)
        {
            case "BlueSlime":
                GameObject.FindGameObjectWithTag("RedSlime").SetActive(false);
                GameObject.FindGameObjectWithTag("GreenSlime").SetActive(false);
                GameObject.FindGameObjectWithTag("DesertBoss").SetActive(false);
                GameObject.FindGameObjectWithTag("TundraBoss").SetActive(false);
                GameObject.FindGameObjectWithTag("ForestBoss").SetActive(false);
                break;
            case "RedSlime": 
                GameObject.FindGameObjectWithTag("GreenSlime").SetActive(false);
                GameObject.FindGameObjectWithTag("BlueSlime").SetActive(false);
                GameObject.FindGameObjectWithTag("DesertBoss").SetActive(false);
                GameObject.FindGameObjectWithTag("TundraBoss").SetActive(false);
                GameObject.FindGameObjectWithTag("ForestBoss").SetActive(false);
                break;
            case "GreenSlime":
                GameObject.FindGameObjectWithTag("BlueSlime").SetActive(false);
                GameObject.FindGameObjectWithTag("RedSlime").SetActive(false);
                GameObject.FindGameObjectWithTag("DesertBoss").SetActive(false);
                GameObject.FindGameObjectWithTag("TundraBoss").SetActive(false);
                GameObject.FindGameObjectWithTag("ForestBoss").SetActive(false);
                break;
            case "ForestBoss":
                GameObject.FindGameObjectWithTag("BlueSlime").SetActive(false);
                GameObject.FindGameObjectWithTag("RedSlime").SetActive(false);
                GameObject.FindGameObjectWithTag("GreenSlime").SetActive(false);
                GameObject.FindGameObjectWithTag("DesertBoss").SetActive(false);
                GameObject.FindGameObjectWithTag("TundraBoss").SetActive(false);
                break;
            case "DesertBoss":
                GameObject.FindGameObjectWithTag("BlueSlime").SetActive(false);
                GameObject.FindGameObjectWithTag("RedSlime").SetActive(false);
                GameObject.FindGameObjectWithTag("GreenSlime").SetActive(false);
                GameObject.FindGameObjectWithTag("ForestBoss").SetActive(false);
                GameObject.FindGameObjectWithTag("TundraBoss").SetActive(false);
                break;
            case "TundraBoss":
                GameObject.FindGameObjectWithTag("BlueSlime").SetActive(false);
                GameObject.FindGameObjectWithTag("RedSlime").SetActive(false);
                GameObject.FindGameObjectWithTag("GreenSlime").SetActive(false);
                GameObject.FindGameObjectWithTag("DesertBoss").SetActive(false);
                GameObject.FindGameObjectWithTag("ForestBoss").SetActive(false);
                break;
        }
    }
}
