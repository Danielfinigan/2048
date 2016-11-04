using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    [SerializeField] private float x;
    [SerializeField] private float y;

	// Use this for initialization
	void Start () {
        this.transform.position = new Vector2(x, y);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
