using UnityEngine;
using System.Collections;

public class PatternSpawner : MonoBehaviour {

	// Patterns to spawn
	public GameObject[] patterns;
	public GameObject[] lanes = new GameObject[5];
	

	// Use this for initialization
	void Start () {
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1,0,0));
		float nextPlacementX = rightEdge.x;
		for(int i = 0; i < patterns.Length; i++){
			if (patterns[i] != null){
				Debug.Log(nextPlacementX);
				GameObject newPattern = (GameObject)Instantiate(patterns[i], new Vector3(nextPlacementX, 0, 0), Quaternion.identity) ;
				PatternController pc = patterns[i].GetComponent<PatternController>();
				if(pc != null){
					pc.lanes = this.lanes;
					pc.calcWidth();
					Debug.Log (pc.width);
					nextPlacementX += pc.width;
				}
				else{
					nextPlacementX += 10;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}

