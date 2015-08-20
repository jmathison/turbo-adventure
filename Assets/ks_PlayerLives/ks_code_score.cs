using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ks_code_score : MonoBehaviour 
{
	public Text scoreText;
	private float score = 0;
	private string theText;
	
	//A multiplier that increases based on living characters.
	private int multiplier = 10;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		score += Time.deltaTime * multiplier;
		scoreText.text = string.Format("Score: {0}000 \n x{1}", Mathf.Floor(score), multiplier);
		
		//Also increase score if an obstacle is dodged.
		//Increase score if the player collects a gem/monies.
	}
	
	//None of the stuff below here is necessary, can be cut if need be.
	//Animate text going from the thing that's hit, to the score, then score jumping up.
	private void ScoreEventPosition(Transform scoreEventPosition, int scoreBonus)
	{
		//Here we need a position of the thing that got hit/added/whatever.
		//Then we need the amount it's going to improve the score.
		//Then we need the function to animate, update, and fun times.
	}
	
	public void PlayerDeath()
	{
		if(multiplier >= 5)
		{
			multiplier -= 5;
		}
	}
	
	public void PlayerCreated()
	{
		multiplier += 5;
	}
}
