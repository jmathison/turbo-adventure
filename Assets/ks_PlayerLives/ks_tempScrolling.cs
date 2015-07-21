using UnityEngine;
using System.Collections;

public class ks_tempScrolling : MonoBehaviour 
{

	private Vector3 moveToPos;
	public GameObject otherObject;
	public GameObject widthReference;
	private Renderer test;
	private float width;
	// Use this for initialization
	void Start () 
	{
		test = widthReference.GetComponent<Renderer>();
		width = test.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(new Vector3(-1 * 5 * Time.deltaTime, 0, 0));
		moveToPos = new Vector3(width + otherObject.transform.position.x - .3f, 0, 0);

		if(this.transform.position.x <= -6)
		{
			transform.position = moveToPos;
		}
	}
}
