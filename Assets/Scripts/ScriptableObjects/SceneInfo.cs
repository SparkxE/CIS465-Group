using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneInfo", menuName = "Persistence")]
public class SceneInfo : ScriptableObject
{
    public string enemyType = "";
    public Vector3 playerPosition;
    public Vector3 lastTouched;
    public List<Vector3> isDead = new List<Vector3>();

    public void Reset(){
        playerPosition = new Vector3(0,0,0);
        lastTouched = new Vector3(0,0,0);
        isDead.Clear();
    }
}
