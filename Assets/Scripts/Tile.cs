using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
    public int tileValue;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void MergeTiles(Tile toBeDeleted)
    {

    }

    public bool Equals(Tile other)
    {
        if (this.tileValue == other.tileValue)
            return true;
        else
            return false;
    }
}
