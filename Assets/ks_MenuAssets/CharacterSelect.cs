using UnityEngine;
using System.Collections;

public class CharacterSelect : MonoBehaviour
{
    public GameObject realCharacter;

    private int price = 100;

	// Use this for initialization
	void Start ()
    {
	    
	}

    void OnMouseDown()
    {
        print("This is happening.");
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnMouseExit()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }


    // Update is called once per frame
    void Update ()
    {
	
	}
}
