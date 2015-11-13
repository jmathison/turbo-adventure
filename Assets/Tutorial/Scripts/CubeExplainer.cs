using UnityEngine;
using System.Collections;

public class CubeExplainer : ExplanationManager 
{
	private TutorialManager tutorialScript;

	//Special for the For Looper project.
	public int myNumber;
	public Texture zero;
	public Texture one;
	public Texture blank;

	protected override void Awake()
	{
		explanation = "I'm a cube! I don't do anything!";
		tutorialScript = Camera.main.GetComponent<TutorialManager>();
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

    //Everything below this is program specific.
	public void SetMyNumber(int textureNum)
	{
		StartCoroutine(OscillateNumber(textureNum));
	}

	public void SetNumberDirect(int num)
	{
		myNumber = num;
		this.Unhighlight();
	}

    public int GetNumber()
    {
        return myNumber;
    }

    IEnumerator OscillateNumber(int finalNum)
	{
		float yieldTime = Random.Range(0.0f, 0.2f);
		for(int i = 0; i < 25; i++)
		{
			float temp = Random.Range(0.0f, 1.0f);
			if(temp > 0.5f)
			{
				SetTexture(1);
			}
			else
			{
				SetTexture(0);
			}
			yield return new WaitForSeconds(yieldTime);
		}

		SetTexture(finalNum);
	}

    //This removes the number texture.
	public void ChangeToSolidColor()
	{
		this.gameObject.GetComponent<Renderer>().material.mainTexture = blank;
	}

	public void Highlight()
	{
		this.gameObject.GetComponent<Renderer>().material.color = Color.red;
	}

	public void Unhighlight()
	{
		if(myNumber == 0)
			this.gameObject.GetComponent<Renderer>().material.color = Color.white;
		else{
			this.gameObject.GetComponent<Renderer>().material.color = Color.black;
		}
	}

    //Sets the texture to a number
	private void SetTexture(int num)
	{
		Renderer myRenderer = this.gameObject.GetComponent<Renderer>();
		myRenderer.material.color = Color.black;
		if(num == 0)
		{
			myRenderer.material.mainTexture = zero;
		}
		if(num == 1)
		{
			myRenderer.material.mainTexture = one;
		}
		myNumber = num;
	}
}
