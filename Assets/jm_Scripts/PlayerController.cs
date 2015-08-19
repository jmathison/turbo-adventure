using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public int startingLane;
	public GameObject[] lanes;
	public GameObject portraitObject;
	public Sprite characterPortrait;
	public Sprite mysteryPortrait;
	public Material selectedMat;
	public Material hurtMat;
	public Material deadMat;

	//Kevin added 08/04
	public GameObject gameController;
	private GameController controllerScript;

	//Kevin added 08/18
	private bool movingUp = false;
	private bool movingDown = false;
	private bool justHit = false;
	private Vector3 movePos;
	private float knockBackSpeed = 15;
	private bool fallingBack = false;

	public GameObject scoreText;
//	private ks_code_score scoreScript;

	public AudioClip bump;

	bool hurt;
	float hurtTime;
	int currentLane;
	bool laneSwitched;
	bool jumping;
	bool clicked;
	Vector3 mousePos;
	Material standard;
	Image portraitSprite;
	float dragTime;
	[HideInInspector]
	private bool alive = false;
	private Animator animator;

	void Awake()
	{
		portraitSprite = portraitObject.GetComponent<Image> ();
		hurt = false;
		//Kevin added 08/04
		controllerScript = gameController.GetComponent<GameController>();
		//Make it start not alive.
		Vector3 initPos = new Vector3(100, 10, 0);
		this.transform.position = initPos;
		
		standard = this.GetComponent<SpriteRenderer> ().material;
		currentLane = startingLane;
		animator = (Animator)this.GetComponent<Animator> ();
		//		portraitSprite = portraitObject.GetComponent<Image> ();
		
		setupSprite ();
	}
	void OnMouseDown ()
	{
		mousePos = Input.mousePosition;
		clicked = true;
		dragTime = 0;
	}

	//Make getting hit not teleport
	void moveToPos()
	{
		if(this.transform.position.x > movePos.x)
		{
			transform.Translate(Vector3.right * -knockBackSpeed * Time.deltaTime);
		}
		else{
			fallingBack = false;
		}
	}

	public void setMovePos(Vector3 position)
	{
		if(!fallingBack)
		{
			fallingBack = true;
			movePos = position;
		}
	}

	public void setHurt ()
	{
		hurt = true;
//		Vector3 checkPosition = Camera.main.WorldToViewportPoint (this.transform.position);
//		if (checkPosition.x < .01) {
//			this.alive = false;
//			this.transform.position = new Vector3(100, 0, 0);
//			setMaterials (deadMat);
//			//GameController function
//			controllerScript.PlayerDied();
//		}
//		else{
			setMaterials(hurtMat);
//		}
	}

	private void CheckDead(){
		Vector3 checkPosition = Camera.main.WorldToViewportPoint (this.transform.position);
		if (checkPosition.x < .01) {
			this.alive = false;
			this.transform.position = new Vector3(100, 0, 0);
			setMaterials (deadMat);
			//GameController function
			controllerScript.PlayerDied();
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (alive) 
		{
			moveToPos();
			CheckDead();

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
							moveUp ();
						} else {
							moveDown ();
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
				StartCoroutine(WaitAndReset());
			}
		}
	}

	void jump ()
	{
		animator.SetTrigger ("playerJump");
		jumping = true;
	}

	void moveUp ()
	{
		movingUp = true;
		movingDown = false;
		if (currentLane < lanes.Length - 1) {
			currentLane++;
		}
		laneSwitched = true;
	}

	void moveDown ()
	{
		movingUp = false;
		movingDown = true;
		if (currentLane > 0) {
			currentLane--;
		}
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

	IEnumerator WaitAndReset()
	{
		yield return new WaitForSeconds(0.2f);
		movingUp = false;
		movingDown = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player" && !justHit && alive)
		{
			if(movingUp)
			{
				moveDown();
			}
			else if(movingDown)
			{
				moveUp();
			}
			else 
			{
				setHurt();
				Vector3 newPos = new Vector3(this.transform.position.x - 1.5f, this.transform.position.y, this.transform.position.z);
				setMovePos(newPos);
			}
			justHit = true;
			StartCoroutine(WaitAndNotHurt());
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player" && !justHit && alive)
		{
			setHurt();
			Vector3 newPos = new Vector3(this.transform.position.x - 1.5f, this.transform.position.y, this.transform.position.z);
			setMovePos(newPos);
			justHit = true;
			StartCoroutine(WaitAndNotHurt());
		}
	}

	IEnumerator WaitAndNotHurt()
	{
		yield return new WaitForSeconds(0.3f);
		justHit = false;
	}

	void setupSprite ()
	{
		portraitSprite.sprite = mysteryPortrait;
	}

	//Kevin Added 08/04
	public void SpawnPlayer(float xPos)
	{
		//xPos increases as spawns go on, so they spawn colliding with each other. Also challenge.
		alive = true;
		snapToLane();
		movePos = this.transform.position;
		//Should not be hard coded.
		this.transform.position = new Vector3(xPos, this.transform.position.y, this.transform.position.z);
		portraitSprite.sprite = characterPortrait;

		setMaterials(standard);
	}
}
