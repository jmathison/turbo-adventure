using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void LoadLevel()
	{
		Application.LoadLevel (1);
	}

	public void LoadOptions()
	{
		Application.LoadLevel ("OptionsMenu");
	}

	public void LoadMenu ()
	{
		Application.LoadLevel (0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
