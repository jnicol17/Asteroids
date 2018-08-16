using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject[] enemies;

    public int lower_limit;
    public int upper_limit;

    // Use this for initialization
    void Start () {
        //Spawn();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Spawn()
    {
        // spawn an enemy at a random rotation
        int angle = Random.Range(lower_limit, upper_limit);
        Debug.Log(angle);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Instantiate(enemies[0], transform.position, rotation);
    }
}
