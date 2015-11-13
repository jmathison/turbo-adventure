using UnityEngine;
using System.Collections;

public class BlankExplainer : ExplanationManager 
{
	//Attach this to an invisible game object, and use it to
	//explain anything not tied to a physical location already.
	protected override void Awake()
	{
		explanation = "I'm a nothing! I don't do anything!";
	}
	
	//Not sure if correct use of override
	public override string GetExplanation()
	{
		return explanation;
	}
	
	public override void SetExplanation(string explanationText)
	{
		explanation = explanationText;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
