using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeetScript : MonoBehaviour {

    public bool collided = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Feet Collider Entered");
        collided = true;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        collided = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log("Feet Collider Exited");
        collided = false;
    }
}
