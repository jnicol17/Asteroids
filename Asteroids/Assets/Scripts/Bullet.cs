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
	
	// Update is called once per frame
	void Update () {
        // constant bullet speed is attained by setting the velocity
        
    }
}
