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
    private bool increasing = false;

	// Use this for initialization
	void Start () 
	{
		
	}

    public void StartIncreasingScore()
    {
        increasing = true;
    }

    public void StopIncreasingScore()
    {
        increasing = false;
    }
	
	// Update is called once per frame
	void Update () 
	{
        if(increasing)
            score += Time.deltaTime * multiplier;
		scoreText.text = string.Format("Score: {0}000 \n x{1}", Mathf.Floor(score), multiplier);
		
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
