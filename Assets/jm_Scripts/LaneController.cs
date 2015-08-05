using UnityEngine;
using System.Collections;

public class LaneController : MonoBehaviour {

	public GameObject obstaclePrefab;
	//Make a random obstacle selector thing
	public GameObject littleObstacle;

	//Make seconds to spawn not hard coded at the beginning.
	float secondsToSpawn = 3;
	float secondsSinceSpawn = 0;

	// Use this for initialization
	void Start () {
		secondsToSpawn = Random.Range(0, 4);
	}
	
	// Update is called once per frame
	void Update () {
		secondsSinceSpawn += Time.deltaTime;
		if(secondsSinceSpawn > secondsToSpawn){
			int randObj = Random.Range(0, 2);
			if(randObj == 0)
			{
				GameObject newObstacle = GameObject.Instantiate(obstaclePrefab);
				newObstacle.GetComponent<ObstacleController>().currentLane = this.gameObject;
			}
			else{
				GameObject newObstacle = GameObject.Instantiate(littleObstacle);
				newObstacle.GetComponent<ks_SmallObstacleController1>().currentLane = this.gameObject;
			}

			secondsSinceSpawn = 0;
			//Modify max number for the time for spawning more.
			secondsToSpawn = Random.Range(1,10);
		}
	}
}
