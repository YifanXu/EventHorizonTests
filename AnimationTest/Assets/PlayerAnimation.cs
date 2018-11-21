using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    public enum PlayerState
    {
        Standing,
        Running,
        Melee
    }

    public PlayerState State {
        get
        {
            return (PlayerState)(animator.GetInteger("State"));
        }
        set 
        {
            animator.SetInteger("State", (int)value);
        }
    }


    private Animator animator; 

	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
        State = PlayerState.Standing;
	}
    
    public bool AnimationLocked
    {
        get
        {
            var anim = animator.GetCurrentAnimatorStateInfo(0);
            if (anim.loop)
            {
                return false;
            }
            return anim.normalizedTime < 1f;
        }
    }
}
