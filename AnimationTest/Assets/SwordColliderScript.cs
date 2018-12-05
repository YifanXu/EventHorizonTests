using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordColliderScript : MonoBehaviour, IFlippinScript {

    public GameObject parent;
    public bool Active = true;

    private List<GameObject> collidingObjs;

	// Use this for initialization
	void Start () {
        collidingObjs = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Enter " + collision.name);
        collidingObjs.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Exit " + collision.name);
        if (collidingObjs.Contains(collision.gameObject))
        {
            collidingObjs.Remove(collision.gameObject);
        }
    }

    public List<GameObject> GetAllCollidingObjs(bool garbageCollection = true)
    {
        if(garbageCollection)
        {
            for(int i = collidingObjs.Count - 1; i >= 0; i--)
            {
                if(collidingObjs[i] == null)
                {
                    collidingObjs.RemoveAt(i);
                }
            }
        }
        return new List<GameObject>(collidingObjs);
    }

    public void Flip(bool flipX)
    {
        var currentVector = this.transform.eulerAngles;
        currentVector.y = flipX ? 180 : 0;
        this.transform.eulerAngles = currentVector;
    }
}
