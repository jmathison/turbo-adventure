using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{

	public List<GameObject> players;
	bool alive = true;

	//Kevin adding player stuff 08/04
	private float creationCounter = 0;
	private int creationTimeNext = 20;
	private int playerCount = 0;
	private int maxPlayers;
	private bool oneShot = false;
	// Use this for initialization
	void Start ()
	{
		maxPlayers = players.Count;
		SpawnPlayers(0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (alive) {
//			int playerCount = 0;
			creationCounter += Time.deltaTime;
			if(playerCount < maxPlayers && creationCounter > creationTimeNext && !oneShot)
			{
				SpawnPlayers(playerCount);
				creationCounter = 0;
			}
			else if(playerCount >= maxPlayers){
				oneShot = true;
			}
//			foreach (GameObject player in players) {
//				if (player.GetComponent<PlayerController>().alive) {
//					playerCount++;
//				}
//			}
			if (playerCount <= 0 && oneShot) {
				alive = false;
				//Might lead to Git version issues.
				Application.LoadLevel(4);
			}
		}
		else{

		}
	}

	public void PlayerDied()
	{
		playerCount --;
	}

	void SpawnPlayers(int playerToSpawn)
	{
		players[playerToSpawn].GetComponent<PlayerController>().SpawnPlayer();
		playerCount++;
	}
}
