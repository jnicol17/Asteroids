using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small_Meteor : MonoBehaviour {

    EdgeCollider2D ec2d;

	// Use this for initialization
	void Start () {
        ec2d = gameObject.GetComponent<EdgeCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if the meteor hits the ground, increase player score and destroy the enemy
        if (other.gameObject.CompareTag("Bullet"))
        {
            // remove the bullet
            Destroy(other.gameObject);
            // disable the collider and then play the small_meteor die animation
            ec2d.enabled = false;
            // remove enemy from game
            //Destroy(this.gameObject);
        }
    }

}
