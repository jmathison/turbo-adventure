using UnityEngine;
using System.Collections;

public class LaneController : MonoBehaviour {

	public GameObject obstaclePrefab;

	float secondsToSpawn = 3;
	float secondsSinceSpawn = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		secondsSinceSpawn += Time.deltaTime;

		if(secondsSinceSpawn > secondsToSpawn){
			GameObject newObstacle = GameObject.Instantiate(obstaclePrefab);
			newObstacle.GetComponent<ObstacleController>().currentLane = this.gameObject;
			secondsSinceSpawn = 0;
			secondsToSpawn = Random.Range(1,2);
		}
	
	}
}
