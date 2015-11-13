using UnityEngine;
using System.Collections;

public class PatternController : MonoBehaviour {

	public GameObject[] lanes = new GameObject[5];

	[HideInInspector] public float width;

	// Use this for initialization
	void Start () {

	}

	public void calcWidth(){
		foreach(Transform child in this.transform){
			if(child.CompareTag("end"))
			{
				Debug.Log("Found end");
				//Should work 
				width = child.gameObject.transform.localPosition.x;
				return;
				//Do the thing.
			}
			else{
				if(width < child.gameObject.transform.localPosition.x){
					width = child.gameObject.transform.localPosition.x;
				}
			}


		}
	}

	void Awake() {
		ObstacleController[] obstacleScripts = this.GetComponentsInChildren<ObstacleController>();
		
		foreach(ObstacleController ob in obstacleScripts){
			ob.laneObject = lanes[ob.lane];
			ob.setSpawnPos();
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate(new Vector3(-1 * 3 * Time.deltaTime,0,0));
		float dist = Camera.main.transform.position.z - this.transform.position.z;
		if(this.transform.position.x < Camera.main.ScreenToWorldPoint(new Vector3(0,0,dist)).x ){
			//GameObject.Destroy(this.gameObject);
		}
	}
}
