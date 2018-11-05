using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponBehavior : MonoBehaviour {


    public float appearTime;
    private float countDown = -100;




	// Use this for initialization
	void Start () {
        this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        countDown -= Time.deltaTime;
        //Appearing Timer
        if (countDown <= -100)
        {
            countDown = appearTime;
        }
        else if(countDown <= 0)
        {
            countDown = -100;
            this.gameObject.SetActive(false);
            return;
        }


	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(countDown == appearTime)
        {
            Debug.Log(string.Format("Hit {0}", collision.name));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (countDown == appearTime)
        {
            Debug.Log(string.Format("Hit {0}", collision.name));
        }
    }
}
