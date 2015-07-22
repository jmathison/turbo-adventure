using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

	[HideInInspector] public GameObject currentLane;

	// Use this for initialization
	void Start () {
		setSpawnPos();
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate(new Vector3(-1 * 3 * Time.deltaTime,0,0));
		if(this.transform.position.x < Camera.main.ViewportToScreenPoint(new Vector3(0,0,0)).x ){
			GameObject.Destroy(this.gameObject);
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log("yes");
		if(coll.gameObject.tag == "Player"){
			Debug.Log("and this");
			PlayerController playerController = coll.gameObject.GetComponent<PlayerController>();
			if(playerController != null && !playerController.isJumping()){
				coll.gameObject.transform.Translate(new Vector3(-.1f, 0, 0));
			}
			GameObject.Destroy(this.gameObject);
		}
	}

	void setSpawnPos(){
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1,0,0));
		this.transform.position = new Vector3(rightEdge.x, 0, 0);
		snapToLane();
	}

	void snapToLane(){
		Vector3 lanePosition = new Vector3(this.transform.position.x, currentLane.transform.position.y, this.transform.position.z);
		this.GetComponent<SpriteRenderer>().sortingLayerID = currentLane.GetComponent<SpriteRenderer>().sortingLayerID;
		this.gameObject.layer = currentLane.layer;
		this.transform.position = lanePosition;
	}
}
