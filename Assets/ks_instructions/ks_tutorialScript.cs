using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ks_tutorialScript : MonoBehaviour 
{
	public Text tutorialText;
	public Image tapAndDrag;
	public Image tapToJump;
	public Text charaSpawn;
	public Text charaSpawnChild;

	public GameObject tutorialPanel;

	private string moveText;
	private string obstacleText;
	private string dodgeText;
	private string deadText;
//	private string jumpText;
	private string newPlayerText;
	private string goodLuck;

	private float instructionDisplayTime = 4.0f;

	// Use this for initialization
	void Start ()
	{
		//For testing tutorial repeatedly.
		PlayerPrefs.DeleteKey("Viewed");
		charaSpawn.enabled = false;
		tapToJump.enabled = false;
		tapAndDrag.enabled = false;
		moveText = "To move up or down a lane:\ntap and drag one of\nyour players.";
		obstacleText = "Move out of the way\nof oncoming obstacles.";
		dodgeText = "Hitting objects knocks\ncharacters backwards.";
		deadText = "If a character gets knocked\n off screen, that character dies.";
//		jumpText = "Tap your player to\njump small obstacles.";
		newPlayerText = "You get a new character\nevery 30 seconds, up\nto four characters!";
		goodLuck = "Stay Alive! Good Luck!";
		if(!PlayerPrefs.HasKey("Viewed"))
		{
			PlayerPrefs.SetInt("Viewed", 0);
			StartCoroutine(TutorialSteps()); 
		}
		//Adds UI timer if tutorial has been seen. 
		else{
			charaSpawn.enabled = true;
			charaSpawnChild.enabled = true;
			tutorialPanel.GetComponent<Image>().enabled = false;
		}
	}

	IEnumerator TutorialSteps()
	{
		tutorialPanel.GetComponent<Image>().enabled = false;
		yield return new WaitForSeconds(1.0f);//+1

		tapAndDrag.enabled = true;
		tutorialText.text = moveText;
		tutorialPanel.GetComponent<Image>().enabled = true;
		yield return new WaitForSeconds(instructionDisplayTime); //+5

		tapAndDrag.enabled = false;
		tutorialText.enabled = false;
		tutorialPanel.GetComponent<Image>().enabled = false;
		yield return new WaitForSeconds(1.0f);//+6

		tutorialText.enabled = true;
		tutorialPanel.GetComponent<Image>().enabled = true;
		tutorialText.text = obstacleText;
		yield return new WaitForSeconds(instructionDisplayTime);//+10

		tutorialText.enabled = false;
		tutorialPanel.GetComponent<Image>().enabled = false;
		yield return new WaitForSeconds(1.0f);//+11
		
		tutorialText.enabled = true;
		tutorialPanel.GetComponent<Image>().enabled = true;
		tutorialText.text = dodgeText;
		yield return new WaitForSeconds(instructionDisplayTime);//+15

		tutorialText.enabled = false;
		tutorialPanel.GetComponent<Image>().enabled = false;
		yield return new WaitForSeconds(1.0f);//+16

		tutorialText.enabled = true;
		tutorialPanel.GetComponent<Image>().enabled = true;
		tutorialText.text = deadText;
		yield return new WaitForSeconds(instructionDisplayTime);
		
		tutorialText.enabled = false;
		tutorialPanel.GetComponent<Image>().enabled = false;
		yield return new WaitForSeconds(1.0f);

//		tutorialText.enabled = true;
//		tutorialPanel.GetComponent<Image>().enabled = true;
//		tutorialText.text = jumpText;
//		tapToJump.enabled = true;
//		yield return new WaitForSeconds(instructionDisplayTime);//+20
//
//		tapToJump.enabled = false;
//		tutorialText.enabled = false;
//		tutorialPanel.GetComponent<Image>().enabled = false;
//		yield return new WaitForSeconds(1.0f);//+21

		tutorialText.enabled = true;
		tutorialPanel.GetComponent<Image>().enabled = true;
		tutorialText.text = newPlayerText;
		charaSpawn.enabled = true;
		charaSpawnChild.enabled = true;
		yield return new WaitForSeconds(instructionDisplayTime);//+25

		tutorialText.enabled = false;
		tutorialPanel.GetComponent<Image>().enabled = false;
		yield return new WaitForSeconds(1.0f);//+26

		tutorialText.enabled = true;
		tutorialPanel.GetComponent<Image>().enabled = true;
		tutorialText.text = goodLuck;
		yield return new WaitForSeconds(instructionDisplayTime);//+30

		tutorialText.enabled = false;
		tutorialPanel.GetComponent<Image>().enabled = false;
		PlayerPrefs.SetInt("Viewed", 1);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
