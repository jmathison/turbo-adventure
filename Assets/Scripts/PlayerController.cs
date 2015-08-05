using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int startingLane;
	public GameObject[] lanes;

	public Material selectedMat;
	public Material hurtMat;

	bool hurt;
	float hurtTime;

	int currentLane;
	bool laneSwitched;
	bool jumping;

	bool clicked;
	Vector3 mousePos;
	Material standard;

	float dragTime;


	private Animator animator;

	// Use this for initialization
	void Start () {

		standard = this.GetComponent<SpriteRenderer>().material;
		currentLane = startingLane;
		animator = (Animator) this.GetComponent<Animator>();
		snapToLane();
	}

	void OnMouseDown(){
		mousePos = Input.mousePosition;
		clicked = true;
		dragTime = 0;
	
	}

	public void setHurt(){
		hurt = true;
		CameraShakeController camShake = Camera.main.GetComponent<CameraShakeController>();
		if(camShake != null)
			camShake.StartCamShake();
		this.GetComponent<SpriteRenderer>().material = hurtMat;
	}
	
	// Update is called once per frame
	void Update () {

		if(hurt){
			hurtTime += Time.deltaTime;
			if(hurtTime > 0.3f){
				this.GetComponent<SpriteRenderer>().material = standard;
				hurt = false;
				hurtTime = 0;
			}
		}

		if(jumping){
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("player-jump"))
				this.transform.Translate(new Vector3(0,.2f,0));
			else if(animator.GetCurrentAnimatorStateInfo(0).IsName("player-fall"))
				this.transform.Translate(new Vector3(0,-.2f,0));
			else if(animator.GetCurrentAnimatorStateInfo(0).IsName("player-idle")){
				jumping = false;
				snapToLane();
			}
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("player-idle")){
				jumping = false;
			}
		}

		if(clicked){

			if(Input.GetMouseButton(0)){
				this.GetComponent<SpriteRenderer>().material = selectedMat;
				dragTime += Time.deltaTime;
			}
			else {
				clicked = false;
				this.GetComponent<SpriteRenderer>().material = standard;
				float dragDistance = Vector3.Distance(mousePos, Input.mousePosition);
				if(dragTime > 0.05f && dragDistance > 0.1f){
					// Dragged
					if( mousePos.y < Input.mousePosition.y){
						moveUp ();
					}
					else{
						moveDown();
					}
				}
				else if (!jumping){
					// Normal click
					jump ();
				}
			}
		}

		if(laneSwitched){
			snapToLane();
			this.GetComponent<AudioSource>().Play();
			laneSwitched = false;
		}


	
	}

	void jump(){
		animator.SetTrigger("playerJump");
		jumping = true;
	}


	void moveUp(){
		if(currentLane < lanes.Length - 1){
			currentLane++;
		}
		laneSwitched = true;
	}

	void moveDown(){
		if(currentLane > 0){
			currentLane--;
		}
		laneSwitched = true;
	}

	public bool isJumping(){
		return jumping;
	}

	void snapToLane(){
		Vector3 lanePosition = new Vector3(this.transform.position.x, lanes[currentLane].transform.position.y, this.transform.position.z);
		this.GetComponent<SpriteRenderer>().sortingLayerID = lanes[currentLane].GetComponent<SpriteRenderer>().sortingLayerID;
		this.gameObject.layer = lanes[currentLane].layer;
		this.transform.position = lanePosition;
	}
}
