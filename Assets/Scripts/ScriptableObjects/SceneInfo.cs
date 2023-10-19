using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneInfo", menuName = "Persistence")]
public class SceneInfo : ScriptableObject
{
    public string enemyType = "";
    public Transform playerPosition;
    public Transform lastTouched;
    public List<Transform> isDead = new List<Transform>();
}
