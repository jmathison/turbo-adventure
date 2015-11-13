using UnityEngine;
using System.Collections;

/**
 * 
 **/
public class JE_InitTiles : MonoBehaviour
{
	public float scrollSpeed = 5.0f; // in World Units
	public int rows = 8;
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
		referenceTile = tile1;
		lastColumn = columns - 1;
		updateLastColumn = false;

		tiles = new GameObject[columns, rows];
		tileWidthWorldUnits = referenceTile.GetComponent<Renderer>().bounds.size.x;

		Vector3 tileX1Point = Camera.main.WorldToViewportPoint(referenceTile.transform.position);
		Vector3 tileX2Point = Camera.main.WorldToViewportPoint(new Vector3(referenceTile.transform.position.x + referenceTile.GetComponent<Renderer>().bounds.size.x, referenceTile.transform.position.y, referenceTile.transform.position.z));
		tileWidthViewportUnits = tileX2Point.x - tileX1Point.x; // measure once on initialization

		tiles = new GameObject[columns, rows];
		for (int i = 0; i < columns; ++i) {
			for (int j = 0; j < rows; j++) {
				// alternating tiles
				var tile = tile1;
				if ((j % 2) == 0) {
					tile = tile2;
				}
				
				tiles[i, j] = (GameObject)Instantiate(tile, new Vector3(i * tileWidthWorldUnits, j * tileWidthWorldUnits, 0), Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < columns; i++) {
			for (int j = 0; j < rows; j++) {
				tiles[i, j].transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime); // move the tile

				// check to see if the tile has reached a threshold
				Vector3 tilePoint = Camera.main.WorldToViewportPoint(tiles[i, j].transform.position);
				// if current tile (given it's width) completely falls past viewport
				if (tilePoint.x < -tileWidthViewportUnits) {
					updateLastColumn = true; // at the end of this row, update the lastColumn
					float resetX = tiles[lastColumn, j].transform.position.x + tiles[lastColumn, j].GetComponent<Renderer>().bounds.size.x;
					// then reset it's x position (maintain current y and z positions)
					tiles[i, j].transform.position = new Vector3(resetX, tiles[i, j].transform.position.y, tiles[i, j].transform.position.z);
				}
			}
		}

		if (updateLastColumn) {
			if (lastColumn >= (columns - 1)) {
				lastColumn = 0;
			} else {
				lastColumn += 1;
			}
			updateLastColumn = false;
		}
	}
}
