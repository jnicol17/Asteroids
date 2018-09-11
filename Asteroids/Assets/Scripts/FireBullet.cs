using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script is linked to the blaster attached to player prefab
public class FireBullet : MonoBehaviour {

    // The bullet that we are firing
    public GameObject bullet;

    public float fire_delay;

    public int max_bullets = 5;

    private int curr_bullets = 0;

    // trigger_reset used to limit blaster to firing once per second
    private bool trigger_reset = true;

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
        // trigger_reset allows at most 1 bullet per second
        if (Input.GetMouseButton(0) && trigger_reset && curr_bullets < max_bullets)
        {
            fire_bullet();
            // Coroutine will yield for 1 second before resetting trigger_reset
            StartCoroutine(reset_trigger());
        }
    }

    // Creates a new bullet prefab and fires it in the direction that the player is facing
    void fire_bullet()
    {
        FindObjectOfType<AudioManager>().Play("FireBullet");
        Instantiate(bullet, transform.position, transform.rotation);
        trigger_reset = false;
        curr_bullets++;
    }

    public void decrease_bullets()
    {
        curr_bullets--;
    }

    // wait for 1 second and then reset the trigger so that player can fire again
    IEnumerator reset_trigger()
    {
        yield return new WaitForSeconds(fire_delay);
        trigger_reset = true;
    }
}
