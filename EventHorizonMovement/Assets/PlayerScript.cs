using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float spd = 150f;
    public float slipperyFactor = 0.1f;
    public float jumpSpd = 300f;
    public float maxSpd = 2f;
    public float gravity = 500f;
    public KeyCode rightKey = KeyCode.RightArrow;
    public KeyCode leftKey = KeyCode.LeftArrow;
    public KeyCode jumpKey = KeyCode.Space;

    public GameObject feet;
    private PlayerFeetScript feetScript;
    private Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
        rigid = this.GetComponent<Rigidbody2D>();
        feetScript = feet.GetComponent<PlayerFeetScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(rightKey))
        {
            if(feetScript.collided && rigid.velocity.x < 0)
            {
                rigid.velocity *= slipperyFactor;
            }
            rigid.AddForce(new Vector2(spd, 0));
            if (rigid.velocity.magnitude > maxSpd)
            {
                rigid.velocity = rigid.velocity.normalized * maxSpd;
            }
        }
        else if (Input.GetKey(leftKey))
        {
            rigid.AddForce(new Vector2(-spd, 0));
            if (rigid.velocity.magnitude > maxSpd)
            {
                rigid.velocity = rigid.velocity.normalized * maxSpd;
            }
        }
        else if (feetScript.collided)
        {
            rigid.velocity *= slipperyFactor;
        }

        rigid.AddForce(new Vector2(0, -gravity * Time.deltaTime));

        if (feetScript.collided)
        {
            if (Input.GetKey(jumpKey))
            {
                rigid.AddForce(new Vector2(0, jumpSpd));
                feetScript.collided = false;
            }
        }   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rigid.velocity *= slipperyFactor;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }
}
