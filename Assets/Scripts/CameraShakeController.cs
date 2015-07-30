using UnityEngine;
using System.Collections;

public class CameraShakeController : MonoBehaviour {

	[HideInInspector] public bool shaking = false;
	public float magnitude = 0.5f;
	public float shakeTime = 0.1f;

	void Start(){

	}

	void Update(){

	}

	public void StartCamShake(){
		if (!shaking){
			shaking = true;
			StartCoroutine("CameraShake");
		}
	}
	
	IEnumerator CameraShake() {
		shaking = true;
		float timeElapsed = 0f;
		
		Vector3 origin = this.transform.position;
		
		while(timeElapsed < shakeTime){
			timeElapsed += Time.deltaTime;
			float damper = 1.0f - Mathf.Clamp(4.0f * (timeElapsed / shakeTime) - 3.0f, 0.0f, 1.0f);
			float x = 2.0f * Mathf.PerlinNoise( Time.time, 0.0f) - 1.0f;
			float y = 2.0f * Mathf.PerlinNoise( 0.0f, Time.time) - 1.0f;
			
			x *= magnitude * damper;
			y *= magnitude * damper;
			
			this.transform.position = new Vector3(origin.x + x, origin.y + y, origin.z);
			
			yield return null;
		}
		
		this.transform.position = origin;
		shaking = false;
	}
}
