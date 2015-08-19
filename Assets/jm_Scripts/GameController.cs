using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public List<GameObject> players;
	public Text characterTimer;
	public Text nextCharacter;
	//Kevin adding player stuff 08/04
	private float creationCounter = 0;
	private int creationTimeNext = 30;
	private int playerCount = 0;
	private int playerSpawnCount = 0;
	private int maxPlayers;
	private bool oneShot = false;

	//Kevin added 08/18
	private float initXPos = 2.5f;
	// Use this for initialization
	void Start ()
	{
		playerCount = 0;
		playerSpawnCount = 0;

		maxPlayers = players.Count;
		SpawnPlayers(0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(characterTimer.enabled)
		{
			nextCharacter.enabled = true;
			characterTimer.text = Mathf.Floor(creationTimeNext - creationCounter).ToString();
		}
		//Original script.
		creationCounter += Time.deltaTime;
		if(playerSpawnCount < maxPlayers && creationCounter > creationTimeNext)
		{
			SpawnPlayers(playerSpawnCount);
			creationCounter = 0;
		}
		if(playerSpawnCount >= maxPlayers)
		{
			characterTimer.enabled = false;
			nextCharacter.enabled = true;
		}
		else if(playerCount >= maxPlayers){
//			oneShot = true;
		}
		if (playerCount <= 0) {
			Application.LoadLevel(2);
		}
	}

	public void PlayerDied()
	{
		playerCount --;
	}

	void SpawnPlayers(int playerToSpawn)
	{
		players[playerToSpawn].GetComponent<PlayerController>().SpawnPlayer(initXPos);
		playerCount++;
		playerSpawnCount++;
//		oneShot = true;
		initXPos += 1.5f;
	}
}
