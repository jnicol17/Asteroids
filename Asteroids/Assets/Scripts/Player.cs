using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// player behaviour script

public class Player : MonoBehaviour
{
    // rotationSpeed controls the speed that the player rotates towards the mouse
    public float rotationSpeed;
    // moveSpeed controls how fast the player moves towards the mouse
    public float moveSpeed;

    private Rigidbody2D rb2d;

    //public static Player instance;

    private void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    //// this function ensures that there is only ever one game controller
    //void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else if (instance != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    // called once per frame
    void FixedUpdate()
    {
        // Controls player rotation towards the mouse

        // Set direction to the mouse pointer on the screen
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        // calculate the angle to rotate
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // rotate the player
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);


        // Controls player movement towards the mouse

        // set the target position (the location of the mouse)
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // z value is constant
        targetPos.z = transform.position.z;
        // move the player towards the mouse at moveSpeed
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);


        // we want the player to keep moving in the current direction even if the mouse stops moving so we need to check targetPos and current position
        //Debug.Log("Transform Position: " + transform.position);
        //Debug.Log("Mouse Position: " + targetPos);
    }
}