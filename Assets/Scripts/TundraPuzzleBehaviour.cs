using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class TundraPuzzleBehaviour : MonoBehaviour
{
    [SerializeField] protected GameObject firstLayer;
    [SerializeField] protected GameObject secondLayer;
    [SerializeField] protected GameObject player;
    [SerializeField] protected GameObject boss;
    [SerializeField] protected SceneInfo sceneInfo;
    private Vector3Int lastTilePos;
    private Vector3Int tilePos;
    private Tilemap firstTilemap;
    private Tilemap secondTilemap;
    private TileBase[] initialTilemapData;
    private Vector3 offset;
    private BoundsInt bounds;
    // Start is called before the first frame update

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        firstTilemap = firstLayer.GetComponent<Tilemap>();
        bounds = firstTilemap.cellBounds;
        initialTilemapData = firstTilemap.GetTilesBlock(bounds);

        secondTilemap = secondLayer.GetComponent<Tilemap>();

        offset = new Vector3(-0.1f,-1.2f,0f); // magic numbers that make sure we are tracking at player's feet which is approx. where TriggerBox lies
        lastTilePos = firstTilemap.WorldToCell(player.transform.position + offset);
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        tilePos = firstTilemap.WorldToCell(player.transform.position + offset);
        if (lastTilePos != tilePos) // if player has entered new tile
        {
            if (firstTilemap.GetTile(tilePos) == null && secondTilemap.GetTile(tilePos) != null) // player entered tile which is already half broken
            {
                player.GetComponent<PlayerBehaviour>().SetPosition(new Vector3(0,0,0)); // spawn player to center
                this.GetComponent<Dialogue>().AlternateTrigger(); // trigger explanation dialogue
                firstTilemap.SetTilesBlock(firstTilemap.cellBounds, initialTilemapData); // reset ice puzzle
                sceneInfo.icePuzzle.SetTilesBlock(firstTilemap.cellBounds, initialTilemapData); // reset ice puzzle in sceneinfo

            }
            else // tile is not broken at all
            {
                firstTilemap.SetTile(tilePos, null); // half break tile
                sceneInfo.icePuzzle.SetTile(tilePos, null); // update sceneinfo to reflect change
                if (AllTilesNull())
                {
                    Debug.Log("boss spawn");
                    boss.SetActive(true); // spawn boss in
                }
            }
        }
        lastTilePos = tilePos;
    }

    private bool AllTilesNull()
    {
        TileBase[] allTiles = firstTilemap.GetTilesBlock(bounds);

        foreach (var tile in allTiles)
        {
            if (tile != null)
            {
                return false;
            }
        }

        return true; // all tiles have been cleared, so puzzle is complete
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if(scene.name == "Overworld_Demo"){
            if(firstTilemap != sceneInfo.icePuzzle && sceneInfo.icePuzzle != null){
                firstTilemap = Instantiate(sceneInfo.icePuzzle);
            }
            else sceneInfo.icePuzzle = firstTilemap;
        }
    }
}
