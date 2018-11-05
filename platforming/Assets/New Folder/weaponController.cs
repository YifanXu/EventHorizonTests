using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponController : MonoBehaviour {

    public SpriteRenderer spriterend;
    bool attacking;
    public GameObject player;
    public Vector3 offset;
    Vector3 playerPos;
    Quaternion playerRotation;
    public Vector3 targetPosition;
    public float attackSpeed;
    public BoxCollider2D weaponCollider;
    

	// Use this for initialization
	void Awake ()
    {
        
        spriterend.enabled = false;
        weaponCollider.enabled = false;
        playerPos = player.transform.position;
        transform.position = new Vector3(playerPos.x + offset.x, playerPos.y + offset.y);
        transform.rotation = playerRotation;
    }
	
	// Update is called once per frame
	void Update () 
    {
        transform.position = new Vector3(playerPos.x + offset.x, playerPos.y + offset.y);
        transform.rotation = playerRotation;
	}
    
    public void Attack()
    {
        spriterend.enabled = true;
        weaponCollider.enabled = true;
        transform.position = Vector3.Lerp(transform.position, transform.position + targetPosition, attackSpeed);

    }

    
}
