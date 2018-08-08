using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Large_Meteor : MonoBehaviour
{

    private PolygonCollider2D pc2d;
    private Animator death_animation;
    private SpriteRenderer spr;
    private Rigidbody2D rb2d;

    public Sprite damageSprite;
    public GameObject small_meteor;

    public int health;

    private IEnumerator coroutine;

    // Use this for initialization
    void Awake()
    {
        pc2d = GetComponent<PolygonCollider2D>();
        death_animation = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        //rb2d.velocity = transform.up * 5;

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
                // spawn two smaller meteors
                coroutine = Spawn(30);
                StartCoroutine(coroutine);
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

    IEnumerator Spawn(int angle)
    {
        Quaternion meteor1_rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Instantiate(small_meteor, transform.position, meteor1_rotation);
        yield return new WaitForSecondsRealtime(0.2f);
        Debug.Log("HI");
        Quaternion meteor2_rotation = Quaternion.AngleAxis(360 - angle, Vector3.forward);
        Instantiate(small_meteor, transform.position, meteor2_rotation);
    }

    //public void spawn(int angle)
    //{
    //    // spawn an enemy at a random rotation
    //    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    //    Instantiate(small_meteor, transform.position, rotation);
    //}

}
