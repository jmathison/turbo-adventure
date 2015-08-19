using UnityEngine;
using System.Collections;

public class WanderScript : MonoBehaviour {
	
	private int currentDestination = 0;
	public GameObject[] destinations;
	private float wanderSpeed = 0.5f;

	private Vector3 destinationLocation;
	private bool reachedDestination = false;
	private float timeWalking = 0;
	private float timeToWalk = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!reachedDestination && timeWalking < timeToWalk){
			this.transform.position = Vector3.MoveTowards(this.transform.position, destinations[currentDestination].transform.position, wanderSpeed * Time.deltaTime);
			if (this.transform.position == destinations[currentDestination].transform.position){
				reachedDestination = true;
			}
			timeWalking += Time.deltaTime;
		}
		else{
			ToggleDestination();
			reachedDestination = false;
		}
	}

	void ToggleDestination(){
		wanderSpeed = Random.Range(0, 3) / 1.5f;
		timeWalking = 0;
		timeToWalk = Random.Range(3, 6);

		currentDestination++;
		
		if(currentDestination >= destinations.Length){
			currentDestination = 0;
		}
	}
}
