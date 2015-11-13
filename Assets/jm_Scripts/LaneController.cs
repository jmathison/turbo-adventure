using UnityEngine;
using System.Collections;

public class LaneController : MonoBehaviour {

	public GameObject obstaclePrefab;
	//Make a random obstacle selector thing
	public GameObject littleObstacle;

	//Make seconds to spawn not hard coded at the beginning.
	float secondsToSpawn;
	float secondsSinceSpawn = 0;

	//KS Added 08/19
	private float levelStartTime;
	private float increaseDifficultyTime = 30;
	private float difficultyTimer = 0f;
	private float spawnNextTime = 10;

	// Use this for initialization
	void Start () {
		levelStartTime = Time.time;
		secondsToSpawn = Random.Range(3, 10);
	}
	
	// Update is called once per frame
	void Update () {
		difficultyTimer += Time.deltaTime;
		if(difficultyTimer > increaseDifficultyTime)
		{
			if(spawnNextTime > 4){
				spawnNextTime -= 2.5f;
			}
			difficultyTimer = 0;
			Debug.Log("Harder");

		}

		secondsSinceSpawn += Time.deltaTime;
		if(secondsSinceSpawn > secondsToSpawn){
			secondsSinceSpawn = 0;
			//Modify max number for the time for spawning more.
			//if(PlayerPrefs.HasKey("Viewed"))
			{
				//if(PlayerPrefs.GetInt("Viewed") == 1)
				{
					int randObj = Random.Range(0, 2);
					if(randObj == 0)
					{
						GameObject newObstacle = GameObject.Instantiate(obstaclePrefab);
						//newObstacle.GetComponent<ObstacleController>().currentLane = this.gameObject;
					}
					else
					{
                        //10/16 - Introduced bug with this code.
						//GameObject newObstacle = GameObject.Instantiate(littleObstacle);
						//newObstacle.GetComponent<ObstacleController>().currentLane = this.gameObject;
					}
					secondsToSpawn = Random.Range(1,spawnNextTime);
				}
				//else if(PlayerPrefs.GetInt("Viewed") == 0)
				//{
				//		GameObject newObstacle = GameObject.Instantiate(littleObstacle);
				//		newObstacle.GetComponent<ks_SmallObstacleController1>().currentLane = this.gameObject;
				//		secondsToSpawn = Random.Range(5,20);
				//}
			}
		}
	}
}
