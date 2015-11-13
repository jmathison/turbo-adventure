using UnityEngine;
using System.Collections;

public class RevisedScrolling : MonoBehaviour {


	// The speed that the tiles scroll on screen
	public float scrollSpeed = 3;

	// Collection of ground tiles to build the map from
	public GameObject[] groundTiles;

	// Collection of edge tiles for the edge of the map
	public GameObject[] edgeTiles;

	// Tile Scale - don't change Z value.
	public Vector3 scale = new Vector3(1,1,1);

	// Next Sheet
	public GameObject nextObject;

	// Width / height of tile grid
	private int width = 20;
	private int height = 7;

	// Array storing generated tiles
	private GameObject[,] tiles;
	private float tileWidth;

	// Width of generated tile map
	private float sectionWidth;

	// Reset position
	private Vector3 moveToPos;

	// Use this for initialization
	void Start () {
		tiles = new GameObject[height, width];
		generateTiles();
		if(this.name != "PanelA"){
			moveToPos = new Vector3 (tileWidth * width + nextObject.transform.localPosition.x - (scrollSpeed * Time.deltaTime), this.transform.localPosition.y, 0);
		}

		transform.localPosition = moveToPos;
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (this.name == "PanelC") {
			moveToPos = new Vector3 (tileWidth * width + nextObject.transform.position.x - (scrollSpeed * Time.deltaTime), this.transform.position.y, 0);
		} else {
			moveToPos = new Vector3 (tileWidth * width + nextObject.transform.position.x, this.transform.position.y, 0);
		}

		transform.Translate(new Vector3( -scrollSpeed * Time.deltaTime, 0, 0));


		
		if(this.transform.position.x <= -tileWidth * (width + 6))
		{
			Debug.Log(this.transform.position.x);
			transform.position = moveToPos;
		}

	}

	// Called in start to make a ground pattern
	void generateTiles(){
		GameObject currentTile = null;
		for (int row = 0; row < height; row++) {
			for(int col = 0; col < width; col++){

				if( row == 0 || row == height-1){
					// Generate screen edges
					currentTile = Instantiate<GameObject>(edgeTiles[Random.Range(0,edgeTiles.Length-1)]);
				}
				else{
					// Generate ground
					currentTile = Instantiate<GameObject>(groundTiles[Random.Range(0,groundTiles.Length-1)]);
				}

				currentTile.transform.parent = this.transform;

				currentTile.transform.localPosition = new Vector3(col * currentTile.GetComponent<SpriteRenderer>().bounds.size.x,row * currentTile.GetComponent<SpriteRenderer>().bounds.size.y,0);

				tiles[row,col] = currentTile;
			}
		}
		this.transform.localScale = scale;
		tileWidth = currentTile.GetComponent<SpriteRenderer> ().bounds.size.x;
	}

}
