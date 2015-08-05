using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{

	public List<GameObject> players;
	bool alive = true;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (alive) {
			int playerCount = 0;
			foreach (GameObject player in players) {
				if (player.GetComponent<PlayerController> ().alive) {
					playerCount++;
				}
			}
			if (playerCount <= 0) {
				Debug.Log ("rip");
				alive = false;
			}
		}
		else{

		}
	}

	void SpawnPlayers ()
	{

	}
}
