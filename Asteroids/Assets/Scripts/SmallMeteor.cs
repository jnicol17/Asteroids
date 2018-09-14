using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMeteor : MonoBehaviour {

    PolygonCollider2D pc2d;
    Animator deathAnimation;

    private Rigidbody2D rb2d;

    public int speed = 4;

    public int score = 5;

    void Awake()
    {
        
        pc2d = GetComponent<PolygonCollider2D>();
        deathAnimation = GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.up * speed;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if a bullet hits the meteor, destroy the meteor and the bullet
        if (other.gameObject.CompareTag("Bullet"))
        {
            FindObjectOfType<AudioManager>().Play("SmallMeteorDie");
            // remove the bullet
            Destroy(other.gameObject);
            // add score to the total score
            GameController.instance.playerScored(score);
            // disable the collider and then play the smallMeteor die animation
            pc2d.enabled = false;
            // play the death animation
            deathAnimation.SetBool("Alive", false);
            // remove enemy from game
            StartCoroutine(destroyMeteor());
        }

        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("PlayerDie");
            Destroy(other.gameObject);
            GameController.instance.playerDied();
        }
    }

    IEnumerator destroyMeteor()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }
}
