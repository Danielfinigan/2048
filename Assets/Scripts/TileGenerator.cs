using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System;

public class TileGenerator : MonoBehaviour {


    public static TileGenerator instance;
    private static int gridSize = 4;

    public Tile[,] grid = new Tile[gridSize, gridSize];
    public Vector2[,] position = new Vector2[gridSize, gridSize];
    public List<Tile> TilePrefabs = new List<Tile>();

    public bool hasMoved = false;
    public bool runOnce = false;
    public int numOfTiles;

    void Awake()
    {
        instance = this;
    }
	// Use this for initialization

    public void AddTile()
    {
        if (numOfTiles < 16)
        {
            bool isEmpty = true;
            while (isEmpty)
            {
                int gridX = Random.Range(0, gridSize);
                int gridY = Random.Range(0, gridSize);
                Tile newTile = grid[gridX, gridY];
                if (newTile == null)
                {
                    newTile = Instantiate(TilePrefabs[0]);
                    newTile.transform.position = position[gridX, gridY];
                    grid[gridX, gridY] = newTile;
                    isEmpty = false;
                    numOfTiles++;
                }
            }
        }
        else
            GameManager.instance.GameOver();        
    }

    public void AddTile(Tile tile, int gridX, int gridY)
    {
        if (numOfTiles < 16)
        {
            Tile newTile = grid[gridX, gridY];
            newTile = Instantiate(tile);
            newTile.transform.position = position[gridX, gridY];
            grid[gridX, gridY] = newTile;
            numOfTiles++;
        }
        else
            GameManager.instance.GameOver();       
    }

    public void RemoveTile(Tile tile, int x, int y)
    {
        Destroy(tile.gameObject);
        grid[x, y] = null;
        numOfTiles--;
    }

    public void MergeTiles(string direction)
    {
        if (direction == "right")
        {
            for(int x = 0; x < gridSize; x++)
            {
                for(int y = gridSize - 1; y > 0; y--)
                {
                    Tile tile1 = grid[x, y];
                    Tile tile2 = grid[x, y - 1];
                    if(tile1 != null && tile2 != null)
                    {
                        if(tile1.Equals(tile2))
                        {
                            int nextTile = (int) System.Math.Log(tile1.tileValue, 2);
                            GameManager.instance.score += tile1.tileValue;
                            RemoveTile(tile1, x, y);
                            RemoveTile(tile2, x, y - 1);
                            AddTile(TilePrefabs[nextTile], x, y);
                        }
                    }
                }
            }
        }
        else if (direction == "left")
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize - 1; y++)
                {
                    Tile tile1 = grid[x, y];
                    Tile tile2 = grid[x, y + 1];
                    if (tile1 != null && tile2 != null)
                    {
                        if (tile1.Equals(tile2))
                        {
                            int nextTile = (int)System.Math.Log(tile1.tileValue, 2);
                            GameManager.instance.score += tile1.tileValue;
                            RemoveTile(tile1, x, y);
                            RemoveTile(tile2, x, y + 1);
                            AddTile(TilePrefabs[nextTile], x, y);
                        }
                    }
                }
            }
        }
        else if (direction == "down")
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = gridSize - 1; x > 0; x--)
                {
                    Tile tile1 = grid[x, y];
                    Tile tile2 = grid[x - 1, y];
                    if (tile1 != null && tile2 != null)
                    {
                        if (tile1.Equals(tile2))
                        {
                            int nextTile = (int)System.Math.Log(tile1.tileValue, 2);
                            GameManager.instance.score += tile1.tileValue;
                            RemoveTile(tile1, x, y);
                            RemoveTile(tile2, x - 1, y);
                            AddTile(TilePrefabs[nextTile], x, y);
                        }
                    }
                }
            }
        }
        else if (direction == "up")
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize - 1; x++)
                {
                    Tile tile1 = grid[x, y];
                    Tile tile2 = grid[x + 1, y];
                    if (tile1 != null && tile2 != null)
                    {
                        if (tile1.Equals(tile2))
                        {
                            int nextTile = (int)System.Math.Log(tile1.tileValue, 2);
                            GameManager.instance.score += tile1.tileValue;
                            RemoveTile(tile1, x, y);
                            RemoveTile(tile2, x + 1, y);
                            AddTile(TilePrefabs[nextTile], x, y);
                        }
                    }
                }
            }
        }
    }

    public void MoveTiles(string direction)
    {
        if(GameManager.instance.currentGameState == GameState.inGame)
        {
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
                            hasMoved = true;
                        }
                        else if (tile1 != null)
                        {
                            emptyTileY -= 1;
                            tile1 = grid[x, emptyTileY];
                        }
                    }
                }
            }
            else if (direction == "left")
            {
                for (int x = 0; x < gridSize; x++)
                {
                    int emptyTileY = 0;
                    Tile tile1 = grid[x, emptyTileY];
                    for (int y = 0; y < gridSize - 1; y++)
                    {
                        Tile tile2 = grid[x, y + 1];
                        //if current tile is empty and next tile is not empty, move tile
                        if (tile1 == null && tile2 != null)
                        {
                            AddTile(tile2, x, emptyTileY);
                            RemoveTile(tile2, x, y + 1);
                            emptyTileY += 1;
                            tile1 = grid[x, emptyTileY];
                            hasMoved = true;
                        }
                        else if (tile1 != null)
                        {
                            emptyTileY += 1;
                            tile1 = grid[x, emptyTileY];
                        }
                    }
                }
            }
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
                            hasMoved = true;
                        }
                        else if (tile1 != null)
                        {
                            emptyTileX -= 1;
                            tile1 = grid[emptyTileX, y];
                        }
                    }
                }
            }
            else if (direction == "up")
            {
                for (int y = 0; y < gridSize; y++)
                {
                    int emptyTileX = 0;
                    Tile tile1 = grid[emptyTileX, y];
                    for (int x = 0; x < gridSize - 1; x++)
                    {
                        Tile tile2 = grid[x + 1, y];
                        //if current tile is empty and next tile is not empty, move tile
                        if (tile1 == null && tile2 != null)
                        {
                            AddTile(tile2, emptyTileX, y);
                            RemoveTile(tile2, x + 1, y);
                            emptyTileX += 1;
                            tile1 = grid[emptyTileX, y];
                            hasMoved = true;
                        }
                        else if (tile1 != null)
                        {
                            emptyTileX += 1;
                            tile1 = grid[emptyTileX, y];
                        }
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

	public void StartGame ()
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
            MergeTiles("down");
            MoveTiles("down");
            if (hasMoved)
                AddTile();
            hasMoved = false;
            runOnce = true;
        }
        else if( verticalIsPressed == 1 && !runOnce)
        {
            MoveTiles("up");
            MergeTiles("up");
            MoveTiles("up");
            if (hasMoved)
                AddTile();
            hasMoved = false;
            runOnce = true;
        }
        //if horizontal direction is pressed
        if (horizontalIsPressed == -1 && !runOnce)
        {
            MoveTiles("left");
            MergeTiles("left");
            MoveTiles("left");
            if (hasMoved)
                AddTile();
            hasMoved = false;
            runOnce = true;
        }
        else if (horizontalIsPressed == 1 && !runOnce)
        {
            MoveTiles("right");
            MergeTiles("right");
            MoveTiles("right");
            if(hasMoved)
                AddTile();
            hasMoved = false;
            runOnce = true;
        }        
    }
}
