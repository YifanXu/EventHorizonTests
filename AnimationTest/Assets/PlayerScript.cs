using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public KeyCode moveRight = KeyCode.RightArrow;
    public KeyCode moveLeft = KeyCode.LeftArrow;
    public float spd;

    private PlayerAnimation playerAnimation;
    private SpriteRenderer sprite;
    private List<IFlippinScript> flippingComponents;
    private List<IAbilityScript> abilityComponents;

    private bool Flip
    {
        get
        {
            return sprite.flipX;
        }
        set
        {
            sprite.flipX = value;
            foreach(var compo in flippingComponents)
            {
                compo.Flip(value);
            }
        }
    }

	// Use this for initialization
	void Start () {
        sprite = this.GetComponent<SpriteRenderer>();
        playerAnimation = this.GetComponent<PlayerAnimation>();

        //Get all components that need to be flipped
        flippingComponents = new List<IFlippinScript>();
        abilityComponents = new List<IAbilityScript>();
        var components = GetComponentsInChildren(typeof(Component));
        foreach(var component in components)
        {
            if(component is IFlippinScript)
            {
                flippingComponents.Add((IFlippinScript)component);
            }
            if(component is IAbilityScript)
            {
                abilityComponents.Add((IAbilityScript)component);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!playerAnimation.AnimationLocked)
        {
            //Check if any other component is requesting animation
            foreach (var ability in abilityComponents)
            {
                var state = ability.GetState();
                if(state != PlayerAnimation.PlayerState.None)
                {
                    playerAnimation.State = state;
                    ability.ExceuteAbility();
                    return;
                }
            }
            //Change animations
            Vector3 newPos = this.transform.position;
            bool moved = false;
            if (Input.GetKey(moveRight))
            {
                moved = true;
                newPos.x += spd * Time.deltaTime;
                Flip = false;
            }
            if (Input.GetKey(moveLeft))
            {
                moved = true;
                newPos.x -= spd * Time.deltaTime;
                Flip = true;
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
        else
        {
            Debug.Log("Stuck in State " + playerAnimation.State);

        }
    }
}
