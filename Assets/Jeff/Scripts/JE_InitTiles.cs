	using UnityEngine;
using System.Collections;

/**
 * 
 **/
public class JE_InitTiles : MonoBehaviour
{
	public float scrollSpeed = 5.0f; // in World Units
	public int rows = 7;
	public int columns = 24;

	// used as base Tile textures in a Scene
	public GameObject tile1;
	public GameObject tile2;
	public GameObject tile3;

	// used for measuring width relative to World and Viewport units
	private GameObject referenceTile;

	// width of a single tile in World and Viewport units
	private float tileWidthWorldUnits; 
	private float tileWidthViewportUnits;

	private Vector3 resetPoint; // the point at which tiles reset their position (only x is relevant)
	
	private GameObject[,] tiles;

	private int lastColumn; // always set to the current last tiles column (24, 23, 22...)
	private bool updateLastColumn;

	// Use this for initialization
	void Start () {
		referenceTile = tile2;
		lastColumn = columns - 1;
		updateLastColumn = false;

		tiles = new GameObject[columns, rows];
		tileWidthWorldUnits = referenceTile.GetComponent<Renderer>().bounds.size.x;

		tiles = new GameObject[columns, rows];
		for (int i = 0; i < columns; ++i) {
			for (int j = 0; j < rows; j++) {
				// alternating tiles
				GameObject tile = tile3;
				if ((j % 2) == 0) {
					tile = tile2;
				}
				
				tiles[i, j] = (GameObject)Instantiate(tile, new Vector3(i * tileWidthWorldUnits, j * tileWidthWorldUnits, 0), Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
