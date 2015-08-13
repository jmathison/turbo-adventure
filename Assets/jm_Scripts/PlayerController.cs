using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public int startingLane;
	public GameObject[] lanes;
	public GameObject portraitObject;
	public Sprite characterPortrait;
	public Material selectedMat;
	public Material hurtMat;
	public Material deadMat;

	public AudioClip bump;

	bool hurt;
	float hurtTime;
	int currentLane;
	int lastMove = 0;

	bool laneSwitched;
	bool jumping;
	bool clicked;
	Vector3 mousePos;
	Material standard;
	Image portraitSprite;
	float dragTime;
	[HideInInspector]
	public bool alive = true;
	private Animator animator;

	// Use this for initialization
	void Start ()
	{

		standard = this.GetComponent<SpriteRenderer> ().material;
		currentLane = startingLane;
		animator = (Animator)this.GetComponent<Animator> ();
		portraitSprite = portraitObject.GetComponent<Image> ();

		setupSprite ();
		snapToLane ();
	}

	void OnMouseDown ()
	{
		mousePos = Input.mousePosition;
		clicked = true;
		dragTime = 0;
	
	}

	public void setHurt ()
	{
		hurt = true;
		CameraShakeController camShake = Camera.main.GetComponent<CameraShakeController> ();
		if (camShake != null)
			camShake.StartCamShake ();
		Vector3 checkPosition = Camera.main.WorldToViewportPoint (this.transform.position);
		if (checkPosition.x < -.1) {
			this.alive = false;
			setMaterials (deadMat);
		}
		else{
			setMaterials(hurtMat);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (alive) {

			// Check and set hurt sprite materials
			if (hurt) {
				hurtTime += Time.deltaTime;
				if (hurtTime > 0.3f) {
					setMaterials (standard);
					hurt = false;
					hurtTime = 0;
				}
			}

			// Check animation states for jumping effect
			if (jumping) {
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("player-jump"))
					this.transform.Translate (new Vector3 (0, .2f, 0));
				else if (animator.GetCurrentAnimatorStateInfo (0).IsName ("player-fall"))
					this.transform.Translate (new Vector3 (0, -.2f, 0));
				else if (animator.GetCurrentAnimatorStateInfo (0).IsName ("player-idle")) {
					jumping = false;
					// Snap to lane when jump is finished in case of weird jump behavior
					snapToLane ();
				}
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("player-idle")) {
					jumping = false;
				}
			}

			// Process mouse button holding / touches
			if (clicked) {

				if (Input.GetMouseButton (0)) {
					setMaterials (selectedMat);
					dragTime += Time.deltaTime;
				} else {
					clicked = false;
					setMaterials (standard);
					float dragDistance = Vector3.Distance (mousePos, Input.mousePosition);
					if (dragTime > 0.1f && dragDistance > 0.1f) {
						// Dragged
						if (mousePos.y < Input.mousePosition.y) {
							move (-1);
						} else {
							move (1);
						}
					} else if (!jumping) {
						// Normal click
						jump ();
					}
				}
			}

			if (laneSwitched) {
				snapToLane ();
				this.GetComponent<AudioSource> ().Play ();
				laneSwitched = false;
			}


		}
	}

	void jump ()
	{
		animator.SetTrigger ("playerJump");
		jumping = true;
	}

	void move (int dif)
	{
		moveTo = currentLane + dif;
		if (moveTo >= lanes.Length || moveTo < 0) {
			moveTo = currentLane;
			laneSwitched = false;
			return;
		}
		lastMove = dif;
		currentLane = moveTo;
		laneSwitched = true;
	}
	

	public bool isJumping ()
	{
		return jumping;
	}

	void snapToLane ()
	{
		Vector3 lanePosition = new Vector3 (this.transform.position.x, lanes [currentLane].transform.position.y, this.transform.position.z);
		this.GetComponent<SpriteRenderer> ().sortingLayerID = lanes [currentLane].GetComponent<SpriteRenderer> ().sortingLayerID;
		this.gameObject.layer = lanes [currentLane].layer;
		this.transform.position = lanePosition;
	}

	void setMaterials (Material material)
	{
		this.GetComponent<SpriteRenderer> ().material = material;
		portraitSprite.material = material;
	}

	void setupSprite ()
	{
		portraitSprite.sprite = characterPortrait;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		// If we hit a player
		if(coll.gameObject.tag = "Player"){

		}
	}
	
}
