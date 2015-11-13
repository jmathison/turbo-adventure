using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

	[HideInInspector] public GameObject laneObject;

	// Obstacle Types
	[HideInInspector] public enum ObstacleType{
		obstacle,
		pickup
	};

	[HideInInspector] public enum ObstacleSize{
		large,
		small
	};
	
	public ObstacleType type = ObstacleType.obstacle;
	public ObstacleSize size = ObstacleSize.large;
	public int lane;

	private float camShakeAmount;
	private float knockbackAmount;
	private float scoreValue;

	// Use this for initialization
	void Start () {
		switch(size){
		case ObstacleSize.large:
			camShakeAmount = .5f;
			knockbackAmount = 1.25f;
			scoreValue = 1000;
			break;
		case ObstacleSize.small:
			camShakeAmount = .2f;
			knockbackAmount = .25f;
			scoreValue = 200;
			break;
		}
	}

	
	// Update is called once per frame
	void Update () {
		// Movement added to parent game object
		float dist = Camera.main.transform.position.z - this.transform.position.z;
		if(this.transform.position.x < Camera.main.ScreenToWorldPoint(new Vector3(0,0,dist)).x ){
			//GameObject.Destroy(this.gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		if(coll.gameObject.tag == "Player"){
			PlayerController playerController = coll.gameObject.GetComponent<PlayerController>();
			if(playerController != null)
			{	
				// Play collision noise
				this.GetComponent<AudioSource>().Play();

				// Code for negative obstacles:
				if(this.type == ObstacleType.obstacle){
					// Knockback and set hurt

					Vector3 hitAmount = new Vector3(coll.transform.position.x - knockbackAmount, coll.transform.position.y, coll.transform.position.z);
					//playerController.setMovePos(hitAmount);
					//Hacked out of the player by Kevin 05/04
					CameraShakeController camShake = Camera.main.GetComponent<CameraShakeController> ();
					//Pass in obstacle type for camshake amount.
					if (camShake != null)
						camShake.StartCamShake(camShakeAmount);
					//playerController.setHurt();
				}
				else if(this.type == ObstacleType.pickup){
					// TODO: Connect to kevin's score code

				}

			}
			//disable obstacle
			this.GetComponent<SpriteRenderer>().enabled = false;
			this.GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	public void setSpawnPos(){
		//Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1,0,0));
		//this.transform.position = new Vector3(rightEdge.x, 0, 0);
		snapToLane();
	}

	void snapToLane(){
		Vector3 lanePosition = new Vector3(this.transform.position.x, laneObject.transform.position.y, this.transform.position.z);
		this.GetComponent<SpriteRenderer>().sortingLayerID = laneObject.GetComponent<SpriteRenderer>().sortingLayerID;
		this.gameObject.layer = laneObject.layer;
		this.transform.position = lanePosition;
	}
}
