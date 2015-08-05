using UnityEngine;
using System.Collections;

public class ks_SmallObstacleController1 : MonoBehaviour {

	[HideInInspector] public GameObject currentLane;

	// Use this for initialization
	void Start () {
		setSpawnPos();
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate(new Vector3(-1 * 3 * Time.deltaTime,0,0));
		float dist = Camera.main.transform.position.z - this.transform.position.z;
		if(this.transform.position.x < Camera.main.ScreenToWorldPoint(new Vector3(0,0,dist)).x ){
			GameObject.Destroy(this.gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		if(coll.gameObject.tag == "Player"){
			PlayerController playerController = coll.gameObject.GetComponent<PlayerController>();
			if(playerController != null && !playerController.isJumping()){
				// Knockback and set hurt
				this.GetComponent<AudioSource>().Play();
				coll.gameObject.transform.Translate(new Vector3(-.25f, 0, 0));
				//Hacked out of the player by Kevin 05/04
				CameraShakeController camShake = Camera.main.GetComponent<CameraShakeController> ();
				//Pass in obstacle type for camshake amount.
				if (camShake != null)
					camShake.StartCamShake(.2f);
				playerController.setHurt();
			}
			this.GetComponent<SpriteRenderer>().enabled = false;
			this.GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	void setSpawnPos(){
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1,0,0));
		this.transform.position = new Vector3(rightEdge.x, 0, 0);
		snapToLane();
	}

	void snapToLane(){
		Vector3 lanePosition = new Vector3(this.transform.position.x, currentLane.transform.position.y - .5f, this.transform.position.z);
		this.GetComponent<SpriteRenderer>().sortingLayerID = currentLane.GetComponent<SpriteRenderer>().sortingLayerID;
		this.gameObject.layer = currentLane.layer;
		this.transform.position = lanePosition;
	}
}
