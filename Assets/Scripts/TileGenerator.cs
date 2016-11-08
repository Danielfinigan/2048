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

    public void RemoveTile(Tile tile, int x, int y)
    {
        Destroy(tile.gameObject);
        grid[x, y] = null;
    }

    public void MergeTiles(Tile tile1, Tile tile2)
    {

    }

    public void Move()
    {

    }

    public void MoveTiles(string direction)
    {
        //check if direction is "right"
        if (direction == "right")
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 3; y > 0; y--)
                {
                    Tile tile1 = grid[x, y];
                    Tile tile2 = grid[x, y - 1];
                    //if current tile is empty and next tile is not empty, move tile
                    if (tile1 == null && tile2 != null)
                    {
                        grid[x, y] = tile2;
                        RemoveTile(tile2, x, y - 1);
                    }
                }
            }
        }
        //check if direction is "left"
        else if (direction == "left")
        {
            
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize - 1; y++)
                {
                    Tile tile1 = grid[x, y];
                    Tile tile2 = grid[x, y + 1];
                    //if current tile is empty and next tile is not empty, move tile
                    if (tile1 == null && tile2 != null)
                    {
                        grid[x, y] = tile2;
                        RemoveTile(x, y + 1);
                    }
                }
            }
        }
        //check if direction is "down"
        else if (direction == "down")
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = gridSize - 1; x > 0; x--)
                {
                    Tile tile1 = grid[x, y];
                    Tile tile2 = grid[x - 1, y];
                    //if current tile is empty and next tile is not empty, move tile
                    if (tile1 == null && tile2 != null)
                    {
                        grid[x, y] = tile2;
                        RemoveTile(x - 1, y);
                    }
                }
            }
        }
        //check if direction is "up"
        else if(direction == "up")
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize - 1; x++)
                {
                    Tile tile1 = grid[x, y];
                    Tile tile2 = grid[x + 1, y];
                    //if current tile is empty and next tile is not empty, move tile
                    if (tile1 == null && tile2 != null)
                    {
                        grid[x, y] = tile2;
                        RemoveTile(x + 1, y);
                    }
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

    void Update()
    {
        float verticalIsPressed = Input.GetAxisRaw("Vertical");
        float horizontalIsPressed = Input.GetAxisRaw("Horizontal");
        //if vertical direction is pressed
        if (verticalIsPressed == -1)
        {
            MoveTiles("down");
            //AddTile();
        }
        else if( verticalIsPressed == 1)
        {
            MoveTiles("up");
           // AddTile();
        }
        //if horizontal direction is pressed
        if (horizontalIsPressed == -1)
        {
            MoveTiles("left");
           // AddTile();
        }
        else if (horizontalIsPressed == 1)
        {
            MoveTiles("right");
           // AddTile();
        }
    }
	
	// Update is called once per frame

}
