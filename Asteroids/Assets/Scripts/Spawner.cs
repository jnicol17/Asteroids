using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject[] enemies;

    // Use this for initialization
    void Start () {
        Spawn();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Spawn()
    {
        // spawn an enemy at a random rotation
        Quaternion rotation = Quaternion.AngleAxis(Random.Range(0,360), Vector3.forward);
        Instantiate(enemies[0], transform.position, rotation);
    }
}
