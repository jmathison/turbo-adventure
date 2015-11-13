using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialManager : MonoBehaviour 
{
	//This is what the tutorial will appear over.
	//Can be players, objects, anything that needs
	//to be introduced, use an invisible game object
    //positioned where you want it on the screen for
    //generic popups unrelated to a specific thing.
	private GameObject target;

	//This is the Canvas element that has the 
	//explanation text, and it's what appears
	//above the thing being explained.
	public GameObject tutorialUI;
	public Text tutorialText;

	//The script that will be explained, it's on the
	//object that's being explained.
    //Any object you want to explain must have this
    //kind of script attached to it.
	private ExplanationManager explainScript;

    //If you need steps to happen in a way that reads
    //the users action, instead of pressing next, set
    //the following to inactive, and use NoNextButton()
    public Button nextButton;

    public void Start()
    {
        //Do this to make the inital canvas not pop up.
        EndExplanation();
    }

    //Deprecated.
    public void SetTarget(GameObject nextTarget)
    {
        //target = nextTarget;
        Debug.Log("Deprecated, use BeginExplanation(GameObject theTarget, Vector2 offset)");
    }

    public void BeginExplanation(GameObject theTarget, Vector2 offset)
	{
        tutorialUI.SetActive(true);
        target = theTarget;
        explainScript = target.GetComponent<ExplanationManager>();
        tutorialText.text = explainScript.GetExplanation();
        PositionExplanation(target, offset);
	}

	public void PositionExplanation(GameObject thing, Vector2 offset)
	{
        Vector2 screenLoc = new Vector2(0, 0);
        if (thing.name == "PlayerNameBar")
        {
            Debug.Log(thing.name + " is at position " + thing.GetComponent<RectTransform>().anchoredPosition);
            screenLoc = new Vector2(thing.GetComponent<RectTransform>().anchoredPosition.x + offset.x, thing.GetComponent<RectTransform>().anchoredPosition.y + offset.y);
        }
        //If it's already on a canvas, and doesn't need to be converted to screen space.
        else if (thing.GetComponent<RectTransform>() != null)
        {
            screenLoc = new Vector2(thing.transform.position.x + offset.x, thing.transform.position.y + offset.y);
        }

        else if (thing.gameObject.transform != null)
        {
            //Convert thing's world position to UI position
            Vector3 screenLocation = Camera.main.WorldToScreenPoint(new Vector3(thing.transform.position.x,
                                    thing.transform.position.y, thing.transform.position.z));
            screenLoc = new Vector2(screenLocation.x + offset.x, screenLocation.y + offset.y);
        }
        else
        {
            Debug.Log("It was an unexpected type. UHOH!");
        }

        //Validate that the position is on the screen
        Vector3 validatePosition = Camera.main.ScreenToViewportPoint(screenLoc);
        if(validatePosition.x < 0 || validatePosition.x > 1 || validatePosition.y < 0 || validatePosition.y > 1)
        {
            //If it's not on the screen, reposition it
            screenLoc = Camera.main.ViewportToScreenPoint(RepositionOnScreen(validatePosition));
        }
        tutorialUI.transform.position = screenLoc;
        Time.timeScale = 0.0f;
    }

    //This gets called if the offset, or object is off screen.
    private Vector2 RepositionOnScreen(Vector2 position)
    {
        float yReset = position.y;
        float xReset = position.x;

        if (position.x < .1f)
        {
            xReset = 0.1f;
        }
        else if (position.x > .9f)
        {
            xReset = 0.8f;
        }
        if (position.y < .1f)
        {
            yReset = 0.1f;
        }
        else if (position.y > .9f)
        {
            yReset = 0.8f;
        }

        return new Vector2(xReset, yReset);
    }

    public void NoNextButton()
    {
        nextButton.gameObject.SetActive(false);
    }

    public void EnableNextButton()
    {
        nextButton.gameObject.SetActive(true);
    }

    public void EndExplanationNoButton()
    {
        Time.timeScale = 1.0f;
        tutorialUI.SetActive(false);
    }

	//Runs when the user clicks the "Acknowledge" button
	private void EndExplanation()
	{
		Time.timeScale = 1.0f;
		tutorialUI.SetActive(false);
	}

    /*
        Code used to test positioning of explanation manager:
        
        //Zero offset test
        ExplainThing(blankExplainer, new Vector2(0, 0));
        //Above Object test
        ExplainThing(test, new Vector2(0, 300));
        //Left of UI Element test
        ExplainThing(inputElements[5].gameObject, new Vector2(-300, 0));
        //Off screen test
        ExplainThing(blankExplainer, new Vector2(-500, 500));
        
        You'll need to create a gameobject called blankExplainer, test, 
        and a UI element named inputElements[5] for the above code to work.
        
        */
}
