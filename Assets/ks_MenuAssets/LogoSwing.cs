using UnityEngine;
using System.Collections;

public class LogoSwing : MonoBehaviour
{

    float xRot = 0;
    private RectTransform myRect;
    private float speed = 350;

	// Use this for initialization
	void Start ()
    {
        //Set to 'invisible'
        this.gameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(90, 0, 0);
        myRect = this.gameObject.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Time.time < 3.8)
        {
            myRect.localRotation = Quaternion.Euler(Mathf.PingPong(Time.time * speed, 90), 0, 0);
            Debug.Log(Time.time);
        }
        else
        {
            this.gameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
        }
        

        if(speed > 100)
        {
            speed -= 30 * Time.deltaTime;
        }
        else
        {
            speed = 0;
        }

        

    }
}
