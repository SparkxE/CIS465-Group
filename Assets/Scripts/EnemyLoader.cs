using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoader : MonoBehaviour
{
    [SerializeField] private SceneInfo sceneInfo;
    private string enemyName;

    private void Awake() {
        enemyName = sceneInfo.enemyType;
        GameObject.FindGameObjectWithTag(enemyName);
    }
}
