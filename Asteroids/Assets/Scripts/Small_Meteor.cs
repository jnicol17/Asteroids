using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small_Meteor : MonoBehaviour {

    PolygonCollider2D pc2d;
    Animator death_animation;

	// Use this for initialization
	void Start () {
        pc2d = GetComponent<PolygonCollider2D>();
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
            pc2d.enabled = false;
            // play the death animation
            death_animation.SetBool("Alive", false);
            // remove enemy from game
            StartCoroutine(destroy_meteor());
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }

    IEnumerator destroy_meteor()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }

}
