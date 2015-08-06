using UnityEngine;
using System.Collections;

public class Scrolling : MonoBehaviour {
	public int wrapOnX = -4;
	public int wrapToX = 4;
	public int scrollSpeed = -6;
	public GameObject background;
	public GameObject[] tiles;


	public GameObject tileA;
	public GameObject tileB;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 edge = new Vector3 (tileA.transform.position.x + tileA.transform.localScale.x, 0, 0);
		tileB.transform.position = edge;
		//Vector3 pos = new Vector3 (this.transform.position.x, 0, 0);
		//background.transform.position = pos;

		if (background.transform.position.x < wrapOnX) {
			background.transform.position = new Vector2(wrapToX, 0);
		}

		background.transform.Translate(new Vector2(scrollSpeed * Time.deltaTime, 0));
	}
}
