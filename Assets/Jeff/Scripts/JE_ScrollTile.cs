using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * For scrolling an individual tile.
 * 'this' should refer to a tile GameObject.
 **/
public class JE_ScrollTile : MonoBehaviour
{
	public int scrollSpeed = 2; // in World Units
	public bool debug = true;

	private float tileWidth;
	private Vector3 tileX1Pos;
	private Vector3 tileX2Pos;

	private Vector3 resetPos;
	// Use this for initialization
	void Start () {
		tileX1Pos = Camera.main.WorldToViewportPoint(this.gameObject.transform.position);
		tileX2Pos = Camera.main.WorldToViewportPoint(new Vector3(this.gameObject.transform.position.x + this.gameObject.GetComponent<Renderer>().bounds.size.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z));
		tileWidth = tileX2Pos.x - tileX1Pos.x;
		resetPos =  Camera.main.ViewportToWorldPoint(new Vector3(1.0f + (tileWidth), .5f, 0)); // 3 tiles to right of viewport
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
		tileX1Pos = Camera.main.WorldToViewportPoint(this.gameObject.transform.position);

		if(tileX1Pos.x < -(tileWidth)) // 3 tiles to the left of the viewport
		{
			Vector3 usefulPos = new Vector3(resetPos.x, this.transform.position.y, this.transform.position.z);
			this.transform.position = usefulPos;
		}
	}
}
