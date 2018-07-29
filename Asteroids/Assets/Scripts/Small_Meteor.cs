using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small_Meteor : MonoBehaviour {

    EdgeCollider2D ec2d;
    Animator death_animation;

	// Use this for initialization
	void Start () {
        ec2d = GetComponent<EdgeCollider2D>();
        death_animation = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if a bullet hits the meteor, destroy the meteor and the bullet
        if (other.gameObject.CompareTag("Bullet"))
        {
            // remove the bullet
            Destroy(other.gameObject);
            // disable the collider and then play the small_meteor die animation
            ec2d.enabled = false;
            // play the death animation
            death_animation.SetBool("Alive", false);
            // remove enemy from game
            StartCoroutine(destroy_meteor());
        }
        
        // if the player hits the meteor, destroy the player
        if (other.gameObject.CompareTag("Player"))
        {
            
        }
    }

    IEnumerator destroy_meteor()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }

}
