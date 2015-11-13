using UnityEngine;
using System.Collections;

public class MenuAnimations : MonoBehaviour
{
    public GameObject MenuBar;
    public GameObject Buttons;
    public GameObject PlayerHolder;
    public GameObject blankExplainer;
    public TutorialManager tutorialManager;
    
    private bool menuAnimateOut = false;
    private bool playersAnimateIn = false;

    private RectTransform menu;
    private RectTransform buttons;
    private float yPos;
    private float xPos;
    private int accel = 1;
    private float speed = 500;

    private int state = -1;

    // Use this for initialization
    void Start ()
    {
	    menu = MenuBar.GetComponent<RectTransform>();
        buttons = Buttons.GetComponent<RectTransform>();
        xPos = menu.anchoredPosition.x; 
        yPos = menu.anchoredPosition.y;
    }

    public void AnimateOutBegin()
    {
        state = 0;
    }

    private void DisplaySelectText()
    {
        tutorialManager.NoNextButton();
        ExplainThing(blankExplainer, "Select a Character!", new Vector2(0, 300));
        Time.timeScale = 1.0f;
    }
    
    public void AnimateIn()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(state == 0)
        {
            if (menu.anchoredPosition.x > -850)
            {
                xPos -= speed * Time.deltaTime;
                speed += accel;
                menu.anchoredPosition = new Vector2(xPos, yPos);
                buttons.anchoredPosition = new Vector2(xPos, buttons.anchoredPosition.y);
            }
            else
            {
                state++;
            }
        }
        else if(state == 1)
        {
            if (PlayerHolder.transform.position.x <= 0)
            {
                PlayerHolder.transform.position = new Vector3(PlayerHolder.transform.position.x + (5 * Time.deltaTime), PlayerHolder.transform.position.y, PlayerHolder.transform.position.z);
            }
            else
            {
                DisplaySelectText();
                state++;
            }
        }
        else if(state == 2)
        {
            //wait for character selection
        }
        else if(state == 3)
        {
            //tutorialManager.EndExplanationNoButton();
            
        }
        else if(state == 4)
        {
            if (PlayerHolder.transform.position.x >= -8)
            {
                PlayerHolder.transform.position = new Vector3(PlayerHolder.transform.position.x - (5 * Time.deltaTime), PlayerHolder.transform.position.y, PlayerHolder.transform.position.z);
            }
        }
	}

    private void ExplainThing(GameObject thing, string explanation, Vector2 offset)
    {
        thing.GetComponent<ExplanationManager>().SetExplanation(explanation);
        tutorialManager.BeginExplanation(thing, offset);
    }

    public void CharacterChosen(string characterName)
    {
        state++;
        print("You selected " + characterName);

        StartCoroutine(WaitAndAnimate(characterName));
    }

    IEnumerator WaitAndAnimate(string name)
    {
        ExplainThing(blankExplainer, "You selected " + name, new Vector2(0, 300));
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(0.75f);
        tutorialManager.EndExplanationNoButton();
        state++;
    }
}
