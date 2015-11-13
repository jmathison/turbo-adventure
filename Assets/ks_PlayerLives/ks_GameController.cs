using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ks_GameController : MonoBehaviour
{
	public List<GameObject> players;
	private float creationCounter = 0;
	private int creationTimeNext = 30;
	private int playerCount = 0;
	private int playerSpawnCount = 0;
	private int maxPlayers;
	private bool oneShot = false;

	private float initXPos = 2.5f;
	// Use this for initialization
	void Start ()
	{
		playerCount = 0;
		playerSpawnCount = 0;

		maxPlayers = players.Count;
		//SpawnPlayers(0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		creationCounter += Time.deltaTime;
		if(playerSpawnCount < maxPlayers && creationCounter > creationTimeNext)
		{
			SpawnPlayers(playerSpawnCount);
			creationCounter = 0;
		}
		else if(playerCount >= maxPlayers){
//			oneShot = true;
		}
		if (playerCount <= 0) {
			Application.LoadLevel(2);
		}
	}

    private void NextLevel()
    {
        playerCount = 0;
        playerSpawnCount = 0;
        maxPlayers = players.Count;
    }

	public void PlayerDied()
	{
		playerCount --;
	}

	void SpawnPlayers(int playerToSpawn)
	{
		players[playerToSpawn].GetComponent<ks_PlayerController>().SpawnPlayer(initXPos);
		playerCount++;
		playerSpawnCount++;
		initXPos += 1.5f;
	}
}
