using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Rigidbody2D rb2d;

    public int bulletSpeed;

    void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //rb2d.AddForce(transform.up * bulletSpeed);
        rb2d.velocity = transform.up * bulletSpeed;
    }
}
