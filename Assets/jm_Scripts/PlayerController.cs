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

	public GameObject gameController;
	private GameController controllerScript;

	private bool movingUp = false;
	private bool movingDown = false;
	private bool justHit = false;
	private Vector3 movePos;
	private float translateSpeed = 5;
	private bool moving = false;

	public GameObject scoreText;
	private ks_code_score scoreScript;

	public AudioClip bump;

    bool hurt = false;
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

    private bool invincible = true;

    private int direction = -1;

    //For selecting a character
    private bool going = false;
    private MenuAnimations menuScript;

	void Awake()
	{
        //For character selection
        if (Application.loadedLevelName == "MainMenu_movement 1")
            menuScript = Camera.main.GetComponent<MenuAnimations>();
		scoreScript = scoreText.GetComponent<ks_code_score>();
		portraitSprite = portraitObject.GetComponent<Image> ();
		controllerScript = gameController.GetComponent<GameController>();
        PositionOffScreen();
        standard = this.GetComponent<SpriteRenderer> ().material;
		currentLane = startingLane;
		animator = (Animator)this.GetComponent<Animator> ();
		setupSprite ();
	}

    void PositionOffScreen()
    {
        Vector3 initPos = new Vector3(100, 10, 0);
        this.transform.position = initPos;
    }

	void OnMouseDown ()
	{
        if (Application.loadedLevelName == "MainMenu_movement 1")
        {
            if(!going)
            {
                StartCoroutine(MenuJump());
                going = true;
            }
        }

        mousePos = Input.mousePosition;
		clicked = true;
		dragTime = 0;
	}

    private IEnumerator MenuJump()
    {
        Vector3 initialTrans = this.gameObject.transform.position;
        float yChange = this.transform.position.y;
        while(this.transform.position.y <= initialTrans.y + 0.4)
        {
            yChange += 10 * Time.deltaTime;
            this.transform.position = new Vector3(this.transform.position.x, yChange, this.transform.position.z);
            yield return null;
        }
        yield return new WaitForSeconds(0.01f);
        while (this.transform.position.y >= initialTrans.y)
        {
            yChange -= 10 * Time.deltaTime;
            this.transform.position = new Vector3(this.transform.position.x, yChange, this.transform.position.z);
            yield return null;
        }
        SendChosen();
    }

    private void SendChosen()
    {
        menuScript.CharacterChosen(this.gameObject.name);
    }

	//Translate to a new position. Used for getting hit, or running in.
	void moveToPos()
	{
        if(moving)
        {
            //Moves left
            if (direction == 0 && !invincible)
            {
                if (this.transform.position.x > movePos.x)
                {
                    //Moving left is hit by obstacle, or player. Should be faster than standard speed.
                    transform.Translate(Vector3.left * (translateSpeed + 10) * Time.deltaTime);
                }
                else
                {
                    moving = false;
                }
            }
            //Moves right
            else if (direction == 1)
            {
                if (this.transform.position.x < movePos.x)
                {
                    transform.Translate(Vector3.right * translateSpeed * Time.deltaTime);
                }
                else
                {
                    moving = false;
                    //Only needs to be set false once.
                    if (this.name == "Character001")
                        scoreScript.StartIncreasingScore();
                    invincible = false;
                }
            }
        }
        
	}

    public void HitObstacle(float distance)
    {
        if(!invincible)
        {
            setMovePos(new Vector3(this.transform.position.x + distance, this.transform.position.y, this.transform.position.z));
            setHurt();
        }
            
    }

	private void setMovePos(Vector3 position)
	{
        movePos = position;

        //Determine if point is ahead of or behind current point.
        if (this.transform.position.x > movePos.x)
            direction = 0;
        else
            direction = 1;

        if (!moving)
		{
			moving = true;
		}
	}

	private void setHurt ()
	{
		hurt = true;
		setMaterials(hurtMat);
	}

	private void CheckDead()
    {
        if(!invincible)
        {
            Vector3 checkPosition = Camera.main.WorldToViewportPoint(this.transform.position);
            if (checkPosition.x < .01)
            {
                this.alive = false;
                this.transform.position = new Vector3(100, 0, 0);
                setMaterials(deadMat);
                scoreScript.PlayerDeath();
                //GameController function
                controllerScript.PlayerDeath();
            }
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
					//snapToLane ();
				}
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("player-idle")) {
					jumping = false;
				}
			}

			// Process mouse button holding / touches
			if (clicked && alive) {

				if (Input.GetMouseButton (0)) {
					setMaterials (selectedMat);
					dragTime += Time.deltaTime;
				} else {
					clicked = false;
					setMaterials (standard);
					float dragDistance = Vector3.Distance (mousePos, Input.mousePosition);
					if (dragTime > 0.01f && dragDistance > 0.01f) {
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
//		animator.SetTrigger ("playerJump");
//		jumping = true;
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
        if(!invincible)
        {
            if (other.gameObject.tag == "Player" && !justHit && alive)
            {
                if (movingUp)
                {
                    moveDown();
                }
                else if (movingDown)
                {
                    moveUp();
                }
                else
                {
                    //Only hit the player behind the first.
                    if(other.gameObject.transform.position.x > this.transform.position.x)
                    {
                        setHurt();
                        Vector3 newPos = new Vector3(this.transform.position.x - 1.8f, this.transform.position.y, this.transform.position.z);
                        setMovePos(newPos);
                    }
                }
                justHit = true;
                StartCoroutine(WaitAndNotHurt());
            }
        }
	}

	void OnTriggerStay2D(Collider2D other)
	{
        if(!invincible)
        {
            if (other.gameObject.tag == "Player" && !justHit && alive)
            {
                setHurt();
                Vector3 newPos = new Vector3(this.transform.position.x - 1.8f, this.transform.position.y, this.transform.position.z);
                setMovePos(newPos);
                justHit = true;
                StartCoroutine(WaitAndNotHurt());
            }
        }
	}

	IEnumerator WaitAndNotHurt()
	{
		yield return new WaitForSeconds(0.4f);
		justHit = false;
	}

	void setupSprite ()
	{
		portraitSprite.sprite = mysteryPortrait;
	}

	//Kevin Added 08/04
    public void Spawn()
    {

    }

    //This will set the first player's art assets to the proper
    //character that they picked from the menu.
    public void Spawn(bool firstChar, float xPos)
    {

    }

	public void Spawn(float xPos)
	{
		scoreScript.PlayerCreated();
		alive = true;
		snapToLane();
        this.transform.position = new Vector3(-4, this.transform.position.y, this.transform.position.z);
		setMovePos(new Vector3(xPos, this.transform.position.y, this.transform.position.z));
		portraitSprite.sprite = characterPortrait;

		setMaterials(standard);
	}
}
