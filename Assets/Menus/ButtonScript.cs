﻿using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void LoadLevel()
	{
		print("I tried");
		Application.LoadLevel ("turbo-adventure");
	}

	public void LoadOptions()
	{
		Application.LoadLevel ("OptionsMenu");
	}

	public void LoadMenu ()
	{
		Application.LoadLevel ("MainMenu");
	}

	/* Turn off the music
	public void MusicOff() 
	{
		AudioClip.
	}
	*/
	
	// Update is called once per frame
	void Update () {
	
	}
}
