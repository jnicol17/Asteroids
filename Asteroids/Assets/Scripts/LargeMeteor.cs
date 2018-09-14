using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeMeteor : MonoBehaviour
{

    private PolygonCollider2D pc2d;
    private Animator deathAnimation;
    private SpriteRenderer spr;
    private Rigidbody2D rb2d;

    public Sprite damageSprite;
    public GameObject smallMeteor;

    public int health;

    public int speed = 3;

    public int score = 25;

    // Use this for initialization
    void Awake()
    {
        pc2d = GetComponent<PolygonCollider2D>();
        deathAnimation = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.up * speed;

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
                FindObjectOfType<AudioManager>().Play("LargeMeteorDamage");
            }
            else
            {
                // add score to the total score
                FindObjectOfType<AudioManager>().Play("LargeMeteorDie");
                GameController.instance.playerScored(score);
                deathAnimation.enabled = true;
                // disable the collider and then play the smallMeteor die animation
                pc2d.enabled = false;
                // play the death animation
                deathAnimation.SetBool("Alive", false);
                // spawn two smaller meteors
                float angle = transform.eulerAngles.z;
                //spawn(transform.up.z);
                spawn(angle);
                // remove enemy from game
                StartCoroutine(destroyMeteor());
            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("PlayerDie");
            Destroy(other.gameObject);
            GameController.instance.playerDied();
        }
    }

    // wait 0.3 seconds to play the death animation
    IEnumerator destroyMeteor()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }

    // spawn 2 small meteors based on the current direction of the large meteor
    public void spawn(float angle)
    {
        Quaternion meteor1Rotation = Quaternion.AngleAxis(angle + 30, Vector3.forward);
        Instantiate(smallMeteor, transform.position, meteor1Rotation);

        Quaternion meteor2Rotation = Quaternion.AngleAxis(angle + 330, Vector3.forward);
        Instantiate(smallMeteor, transform.position, meteor2Rotation);
    }

}
