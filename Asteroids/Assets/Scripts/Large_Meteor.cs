using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Large_Meteor : MonoBehaviour
{

    private PolygonCollider2D pc2d;
    private Animator death_animation;
    private SpriteRenderer spr;

    public Sprite damageSprite;

    public int health;

    // Use this for initialization
    void Start()
    {
        pc2d = GetComponent<PolygonCollider2D>();
        death_animation = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if a bullet hits the meteor, destroy the meteor and the bullet
        if (other.gameObject.CompareTag("Bullet"))
        {
            // remove the bullet
            Destroy(other.gameObject);
            health--;
            if (health > 0)
            {
                spr.sprite = damageSprite;
            }
            else
            {
                death_animation.enabled = true;
                // disable the collider and then play the small_meteor die animation
                pc2d.enabled = false;
                // play the death animation
                death_animation.SetBool("Alive", false);
                // remove enemy from game
                StartCoroutine(destroy_meteor());
            }
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
