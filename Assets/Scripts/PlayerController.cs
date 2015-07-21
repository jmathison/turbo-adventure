using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject[] lanes;
	int currentLane;
	bool laneSwitched;
	bool jumping;

	private Animator a;

	// Use this for initialization
	void Start () {
		currentLane = 0;
		a = (Animator) this.GetComponent<Animator>();
		snapToLane();
	}
	
	// Update is called once per frame
	void Update () {
		if(jumping){
			if(a.GetCurrentAnimatorStateInfo(0).IsName("player-jump"))
				this.transform.Translate(new Vector3(0,.1f,0));
			else if(a.GetCurrentAnimatorStateInfo(0).IsName("player-fall"))
				this.transform.Translate(new Vector3(0,-.1f,0));
			else if(a.GetCurrentAnimatorStateInfo(0).IsName("player-idle")){
				jumping = false;
				snapToLane();
			}

		}
		if(Input.GetButtonDown("Up")){
			currentLane++;
			if(currentLane >= lanes.Length){
				currentLane = 0;
			}
			laneSwitched = true;
		}
		else if (Input.GetButtonDown("Down")){
			currentLane--;
			if(currentLane < 0){
				currentLane = lanes.Length-1;
			}
			laneSwitched = true;
		}

		if(jumping && a.GetCurrentAnimatorStateInfo(0).IsName("player-idle")){
			jumping = false;
		}


		if(!jumping && Input.GetButtonDown("Jump")){
			a.SetTrigger("playerJump");
			jumping = true;

		}

		if(laneSwitched){
			snapToLane();
			laneSwitched = false;
		}


	
	}

	void snapToLane(){
		Vector3 lanePosition = new Vector3(this.transform.position.x, lanes[currentLane].transform.position.y, this.transform.position.z);
		this.transform.position = lanePosition;
	}
}
