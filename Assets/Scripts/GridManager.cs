using UnityEngine;
using System.Collections.Generic;

public class Tile
{
    public Vector2Int gridPos;
    public Vector3 worldPos;
    public GameObject unitOnTile;
    public bool isOccupied => unitOnTile != null;
}

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [Header("Grid Settings")]
    public int width = 7;
    public int height = 4;
    public GameObject tilePrefab; // 타일 프리팹 여기에다가 넣는거

    private Tile[,] grid;

    void Awake()
    {
        if (Instance == null) Instance = this;
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Tile[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 spawnPos = new Vector3(x, 0, z);
                
                if (tilePrefab != null)
                {
                    Instantiate(tilePrefab, spawnPos, Quaternion.identity, transform);
                }

                grid[x, z] = new Tile
                {
                    gridPos = new Vector2Int(x, z),
                    worldPos = spawnPos
                };
            }
        }
    }

    public Tile GetNearestTile(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt(worldPos.x);
        int z = Mathf.RoundToInt(worldPos.z);

        if (x >= 0 && x < width && z >= 0 && z < height)
            return grid[x, z];
        
        return null;
    }
}