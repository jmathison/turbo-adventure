using UnityEngine;
using System.Collections;

/**
 * Generates an ArrayList of GameObjects using the default tile background.
 * TODO: add everything else
 **/
public class ScrollingTiles : MonoBehaviour {
	public float wrapOnX = -4.0f;
	public float wrapToX = 4.0f;
	public int scrollSpeed = -6;
	public GameObject anchor;
	public GameObject[,] tiles;
	public int rows = 8;
	public int columns = 36;
	public GameObject tileA;
	public GameObject tileB;
	public GameObject tileC;
	public float tileWidth;
	public float tileHeight;
	public float tileLayer = 1.0f;
	public float cameraScale = 3.0f;
	public float scale;

	// Use this for initialization
	void Start () {
		Renderer tileRenderer = tileA.GetComponent<Renderer>();
		tileWidth = tileRenderer.bounds.size.x;
		tileHeight = tileRenderer.bounds.size.y;
		float anchorX = anchor.transform.position.x;
		float anchorY = anchor.transform.position.y;
		Debug.Log (anchorX);
		Debug.Log (anchorY);
		
		tiles = new GameObject[columns, rows];
		for (int i = 0; i < columns; ++i) {
			for (int j = 0; j < rows; j++) {
				float x = ((tileWidth) * i) - (anchorX / 2);
				float y = ((tileHeight) * j) - (anchorY / 2);

				if (i == 0 && j == 0) {
					//Debug.Log (bgX);
					//Debug.Log (bgY);
					Debug.Log (tileWidth);
					Debug.Log (tileHeight);
					//Debug.Log (x);
					//Debug.Log (y);
				}

				// alternating tiles
				var tile = tileA;
				if ((j % 2) == 0) {
					tile = tileB;
				}

				tiles[i, j] = (GameObject)Instantiate(tile, new Vector3(x, y, tileLayer), Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Vector3 width = new Vector2(tileA.transform.position.x + tileA.transform.localScale.x, 0);
		//tileB.transform.position = edge;

		// if past swap distance
		if (tiles[0, 0].transform.position.x < anchor.transform.position.x) {
			// reset position for tiles
			for (int i = 0; i < columns; ++i) {
				for (int j = 0; j < rows; j++) {
					tiles[i, j].transform.position = new Vector3(tiles[i, j].transform.position.x + (wrapToX * 2.0f), tiles[i, j].transform.position.y, tileLayer);

					if (i == 0 && j == 0) {
						Debug.Log (tiles[i, j].transform.position);
					}
				}
			}
		}

		// translate the tiles
		for (int i = 0; i < columns; ++i) {
			for (int j = 0; j < rows; j++) {
				tiles[i, j].transform.Translate(new Vector3(scrollSpeed * Time.deltaTime, 0, tileLayer));
			}
		}
	}
}

