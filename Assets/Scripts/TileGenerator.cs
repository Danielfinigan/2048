using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileGenerator : MonoBehaviour {

    private static int gridSize = 4;
    public Tile[,] grid = new Tile[gridSize, gridSize];
    public Vector2[,] position = new Vector2[gridSize, gridSize];
    public List<Tile> TilePrefabs = new List<Tile>();
    public TileGenerator instance;

    public bool runOnce = false;

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

    public void AddTile(Tile tile, int gridX, int gridY)
    {
        Tile newTile = grid[gridX, gridY];
        newTile = Instantiate(tile);
        newTile.transform.position = position[gridX, gridY];
        grid[gridX, gridY] = newTile;
        
    }

    public void RemoveTile(Tile tile, int x, int y)
    {
        Destroy(tile.gameObject);
        grid[x, y] = null;
    }

    public void MergeTiles(Tile tile1, Tile tile2)
    {

    }

    public void MoveTiles(string direction)
    {
        //check if direction is "right"
        if (direction == "right")
        {
            for (int x = 0; x < gridSize; x++)
            {
                int emptyTileY = gridSize - 1;
                Tile tile1 = grid[x, emptyTileY];
                for (int y = gridSize - 1; y > 0; y--)
                {
                    Tile tile2 = grid[x, y - 1];
                    //if current tile is empty and next tile is not empty, move tile
                    if (tile1 == null && tile2 != null)
                    {
                        AddTile(tile2, x, emptyTileY);
                        RemoveTile(tile2, x, y - 1);
                        emptyTileY -= 1;
                        tile1 = grid[x, emptyTileY];
                    }
                    else if (tile1 != null)
                    {
                        emptyTileY -= 1;
                        tile1 = grid[x, emptyTileY];
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
                        AddTile(tile2, x, y);
                        RemoveTile(tile2, x, y + 1);
                    }
                }
            }
        }
        //check if direction is "down"
        else if (direction == "down")
        {
            for (int y = 0; y < gridSize; y++)
            {
                int emptyTileX = gridSize - 1;
                Tile tile1 = grid[emptyTileX, y];
                for (int x = gridSize - 1; x > 0; x--)
                {                    
                    Tile tile2 = grid[x - 1, y];
                    //if current tile is empty and next tile is not empty, move tile
                    if (tile1 == null && tile2 != null)
                    {
                        AddTile(tile2, emptyTileX, y);
                        RemoveTile(tile2, x - 1, y);
                        emptyTileX -= 1;
                        tile1 = grid[emptyTileX, y];
                    }
                    else if (tile1 != null)
                    {
                        emptyTileX += 1;
                        tile1 = grid[emptyTileX, y];
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
                        AddTile(tile2, x, y);
                        RemoveTile(tile2, x + 1, y);
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

        if (verticalIsPressed == 0 && horizontalIsPressed == 0)
            runOnce = false;


        //if vertical direction is pressed
        if (verticalIsPressed == -1 && !runOnce)
        {            
            MoveTiles("down");
          //  AddTile();
            runOnce = true;
        }
        else if( verticalIsPressed == 1 && !runOnce)
        {
            MoveTiles("up");
           // AddTile();
            runOnce = true;
        }
        //if horizontal direction is pressed
        if (horizontalIsPressed == -1 && !runOnce)
        {
            MoveTiles("left");
            AddTile();
            runOnce = true;
        }
        else if (horizontalIsPressed == 1 && !runOnce)
        {
            MoveTiles("right");
            //AddTile();
            runOnce = true;
        }
    }
	
	// Update is called once per frame

}
