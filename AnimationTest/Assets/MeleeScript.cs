using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : MonoBehaviour, IAbilityScript {

    public KeyCode meleekey = KeyCode.X;
    public GameObject sword;

    private SwordColliderScript swordCollider;
    private PlayerAnimation anim;

	// Use this for initialization
	void Start () {
        anim = this.GetComponent<PlayerAnimation>();
        swordCollider = sword.GetComponent<SwordColliderScript>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public PlayerAnimation.PlayerState GetState()
    {
        if(Input.GetKeyDown(meleekey))
        {
            return PlayerAnimation.PlayerState.Melee;
        }
        else
        {
            return PlayerAnimation.PlayerState.None;
        }
    }

    public void ExceuteAbility()
    {
        var objs = swordCollider.GetAllCollidingObjs(true);
        foreach (var obj in objs)
        {
            //Replace in real code with Interaction with Entity Script
            Destroy(obj);
        }
    }
}
