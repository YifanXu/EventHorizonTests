using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public KeyCode moveRight = KeyCode.RightArrow;
    public KeyCode moveLeft = KeyCode.LeftArrow;
    public KeyCode meleeKey = KeyCode.X;
    public float spd;

    private PlayerAnimation playerAnimation;

	// Use this for initialization
	void Start () {
        playerAnimation = this.GetComponent<PlayerAnimation>();
	}
	
	// Update is called once per frame
	void Update () {

        
        

        if (!playerAnimation.AnimationLocked)
        {
            if (Input.GetKeyDown(moveRight)) this.GetComponent<SpriteRenderer>().flipX = false;
            if (Input.GetKeyDown(moveLeft)) this.GetComponent<SpriteRenderer>().flipX = true;
            //Change animations
            if (Input.GetKeyDown(meleeKey))
            {
                Debug.Log("Melee!");
                playerAnimation.State = PlayerAnimation.PlayerState.Melee;
            }
            else
            {
                //Move
                Vector3 newPos = this.transform.position;
                bool moved = false;
                if (Input.GetKey(moveRight))
                {
                    moved = true;
                    newPos.x += spd * Time.deltaTime;
                    this.GetComponent<SpriteRenderer>().flipX = false;
                }
                if (Input.GetKey(moveLeft))
                {
                    moved = true;
                    newPos.x -= spd * Time.deltaTime;
                    this.GetComponent<SpriteRenderer>().flipX = true;
                }

                if (!moved)
                {
                    playerAnimation.State = PlayerAnimation.PlayerState.Standing;
                }
                else
                {
                    playerAnimation.State = PlayerAnimation.PlayerState.Running;
                    this.transform.position = newPos;
                }
            }
        }
        else
        {
            Debug.Log("Stuck in State " + playerAnimation.State);
        }
    }
}
