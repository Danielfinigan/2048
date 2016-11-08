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

    public void MergeTiles(Tile tile1, Tile tile2)
    {

    }

    public void MoveVertical(string direction)
    {
        //set default direction to "up"
        int offset = 1;
        int startY = 0;

        //check if direction is "down"
        if (direction == "down")
        {
            offset = -1;
            startY = gridSize - 1;
        }

        for(int x = 0; x < gridSize; x++)
        {
            for(int y = startY; y < gridSize; y++)
            {
                Tile tile1 = grid[x, y];
                Tile tile2 = grid[x, y + offset];
                //if current tile is empty and next tile is not empty, move tile
                if (tile1 == null && tile2 != null)
                {
                    grid[x, y] = tile2;
                }                
            }
        }
    }

    public void MoveHorizontal(string direction)
    {
        //set default direction to "right"
        int offset = 1;
        int startX = 0;

        //check if direction is "left"
        if (direction == "down")
        {
            offset = -1;
            startX = gridSize - 1;
        }

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = startX; y < gridSize; y++)
            {
                Tile tile1 = grid[x, y];
                Tile tile2 = grid[x, y + offset];
                //if current tile is empty and next tile is not empty, move tile
                if (tile1 == null && tile2 != null)
                {
                    grid[x, y] = tile2;
                }
            }
        }
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
