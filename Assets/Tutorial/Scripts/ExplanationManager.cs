using UnityEngine;
using System.Collections;

public class ExplanationManager : MonoBehaviour 
{
	protected string explanation;

	protected virtual void Awake()
	{
		explanation = "This is a default explanation.";
	}

	public virtual string GetExplanation()
	{
		return explanation;
	}

	public virtual void SetExplanation(string explanationText)
	{
		explanation = explanationText;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
