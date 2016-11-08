using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileGenerator : MonoBehaviour {

    private static int gridSize = 4;
    public Tile[,] grid = new Tile[gridSize, gridSize];
    public Vector2[,] position = new Vector2[gridSize, gridSize];
    public List<Tile> TilePrefabs = new List<Tile>();
    public TileGenerator instance;

    void Awake()
    {
        instance = this;
    }
	// Use this for initialization

    public void AddTile()
    {
        bool isEmpty = true;
        while(isEmpty)
        {
            int gridX = Random.Range(0, gridSize);
            int gridY = Random.Range(0, gridSize);
            Tile newTile = grid[gridX, gridY];
            if(newTile == null)
            {
                newTile = Instantiate(TilePrefabs[0]);
                newTile.transform.position = position[gridX, gridY];
                grid[gridX, gridY] = newTile;
                isEmpty = false;
            }
        }
    }

    public void RemoveTile()
    {

    }

    public void Equals(Tile other)
    {

    }

    public void SetDefaultTilePositions()
    {
        int y = 3;
        for (int i = 0; i < gridSize; i++)
        {
            int x = -3;
            for (int j = 0; j < gridSize; j++)
            {
                position[i, j] = new Vector2(x, y);
                x += 2;
            }
            y -= 2;
        }
    }

	void Start ()
    {
        SetDefaultTilePositions();
        AddTile();
	}
	
	// Update is called once per frame

}
