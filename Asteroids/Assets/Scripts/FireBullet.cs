using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script is linked to the blaster attached to player prefab
public class FireBullet : MonoBehaviour {

    // The bullet that we are firing
    public GameObject bullet;

    public float fireDelay;

    public int maxBullets = 5;

    private int currBullets = 0;

    // triggerReset used to limit blaster to firing once per second
    private bool triggerReset = true;

    public static FireBullet instance;

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

    // Update is called once per frame
    void Update () {
        // The player can hold the mouse button down or just click it once
        // triggerReset allows at most 1 bullet per second
        if (Input.GetMouseButton(0) && triggerReset && currBullets < maxBullets)
        {
            fireBullet();
            // Coroutine will yield for 1 second before resetting triggerReset
            StartCoroutine(resetTrigger());
        }
    }

    // Creates a new bullet prefab and fires it in the direction that the player is facing
    void fireBullet()
    {
        FindObjectOfType<AudioManager>().Play("FireBullet");
        Instantiate(bullet, transform.position, transform.rotation);
        triggerReset = false;
        currBullets++;
    }

    public void decreaseBullets()
    {
        currBullets--;
    }

    // wait for 1 second and then reset the trigger so that player can fire again
    IEnumerator resetTrigger()
    {
        yield return new WaitForSeconds(fireDelay);
        triggerReset = true;
    }
}
