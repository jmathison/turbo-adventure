using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

	public List<GameObject> players;
	bool alive = true;

	//Kevin adding player stuff 08/04
	private float creationCounter = 0;
	private int creationTimeNext = 10;
	private int playerCount = 0;
	private int playerSpawnCount = 0;
	private int maxPlayers;
	private bool oneShot = false;
	// Use this for initialization
	void Start ()
	{
		playerCount = 0;
		playerSpawnCount = 0;

		maxPlayers = players.Count;
		Debug.Log("player count is " + playerCount);
		SpawnPlayers(0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (alive) {
			//Original script.
			creationCounter += Time.deltaTime;
			if(playerSpawnCount < maxPlayers && creationCounter > creationTimeNext)
			{
				SpawnPlayers(playerSpawnCount);
				creationCounter = 0;
			}
			else if(playerCount >= maxPlayers){
				oneShot = true;
			}

			if (playerCount <= 0) {
				Application.LoadLevel(2);
			}
		}
	}

	public void PlayerDied()
	{
		playerCount --;
	}

	void SpawnPlayers(int playerToSpawn)
	{
		Debug.Log("I spawned " + playerToSpawn + ", " + players[playerToSpawn].name);
		players[playerToSpawn].GetComponent<PlayerController>().SpawnPlayer();
		playerCount++;
		playerSpawnCount++;
		oneShot = true;
	}
}
