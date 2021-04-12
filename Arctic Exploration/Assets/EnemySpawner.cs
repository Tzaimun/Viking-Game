using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Tilemap variables
    public Tilemap baseTilemap;
    public Tilemap objectTilemap;
    private int xmax = 0;
    private int ymax = 0;
    
    // De prefab om de enemy mee te spawnen
    public GameObject enemyPrefab;

    // Deze gebruik ik om een plek te vinden waar een enemy gespawned kan worden.
    public Camera camera;
    public GridLayout gridLayout;

    void Start()
    {
        // Gebruik gemaakt van https://gamedev.stackexchange.com/questions/150917/how-to-get-all-tiles-from-a-tilemap
        
        // Start is called before the first frame update
        BoundsInt bounds = baseTilemap.cellBounds;
        TileBase[] allTiles = baseTilemap.GetTilesBlock(bounds);
        // Wat hier gebeurt is dat alle tiles worden gezocht en dan "vertaald" naar de echte positie
        for (int x = 0; x < bounds.size.x; x++)
        {
            xmax = x;
            for (int y = 0; y < bounds.size.y; y++)
            {
                ymax = y;
                
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                }
                else
                {
                    Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                }
            }
        }
    }
    
    // Dit kan geroepen worden om een nieuwe enemy prefab te maken.
    public void newEnemy()
    {
        // Haal een willekeurige positie op en spawn een prefab.
        Vector3Int pos = new Vector3Int(0, 0, 0);
        // Controleer of de positie op een object staat.
        bool tileOnObject = true;
        while (tileOnObject == true)
        {
            pos = getRandomPos();
            if (objectTilemap.GetTile(pos) == true)
            {
                tileOnObject = true;
            } else
            {
                tileOnObject = false;
            }
        }
        Debug.Log($"Instantiating new one at {pos.x}, {pos.y}, {pos.z}!");
        Instantiate(enemyPrefab, gridLayout.CellToWorld(pos), Quaternion.identity);
    }

    // Willekeurige positie wordt uitgekozen
    private Vector3Int getRandomPos()
    {
        float xbounds = camera.aspect * camera.orthographicSize;
        float ybounds = camera.orthographicSize;
        int xpos = Random.Range(xmax / -2, xmax / 2);
        int ypos = Random.Range(ymax / -2, ymax / 2);
        Debug.Log(xpos);
        Debug.Log(ypos);
        Vector3Int pos = new Vector3Int(xpos, ypos, -1);
        //return gridLayout.WorldToCell(pos);
        Debug.Log(pos);
        return pos;
    }
}
