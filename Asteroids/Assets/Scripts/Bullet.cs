using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for the bullet prefab
public class Bullet : MonoBehaviour {

    // The speed that the bullet moves at
    public int bulletSpeed;

    private Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.up * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if a bullet hits the meteor, destroy the meteor and the bullet
        if (other.gameObject.CompareTag("Meteor"))
        {
            FireBullet.instance.decrease_bullets();
        }
        else if (other.gameObject.CompareTag("Barrier"))
        {
            FireBullet.instance.decrease_bullets();
        }
    }
}
