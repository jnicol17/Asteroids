using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Spawner[] east_side_spawners;
    public Spawner[] west_side_spawners;
    public Spawner[] north_side_spawners;
    public Spawner[] south_side_spawners;

    public static GameController instance;

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

    // Use this for initialization
    void Start () {
        //foreach (Spawner spawner in east_side_spawners)
        //{
        //    spawner.Spawn();
        //}
    }

    // Update is called once per frame
    void Update () {

    }
}
