using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileGenerator : MonoBehaviour {

    private static int gridSize = 4;
    public Tile[,] grid = new Tile[gridSize, gridSize];
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
            if(grid[gridX, gridY] == null)
            {
                grid[gridX,gridY] = Instantiate(TilePrefabs[0]);
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

	void Start ()
    {
        AddTile();
	}
	
	// Update is called once per frame

}
