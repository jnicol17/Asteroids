using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// player behaviour script

public class Player : MonoBehaviour
{

    public float rotationSpeed;
    public float moveSpeed;

    // x value can not be larger that |clampX|
    public static Player instance;

    // this function ensures that there is only ever one game controller
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // called once per frame
    void Update()
    {
        // Controls player rotation towards the mouse
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);


        // Controls player movement towards the mouse
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = transform.position.z;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
}