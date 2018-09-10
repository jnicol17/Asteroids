﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public Spawner[] east_side_spawners;
    public Spawner[] west_side_spawners;
    public Spawner[] north_side_spawners;
    public Spawner[] south_side_spawners;

    public static GameController instance;

    private bool gameOver = false;

    public GameObject gameOverText;

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
        StartCoroutine(spawn_routine());
    }

    // Update is called once per frame
    void Update () {

    }

    private void FixedUpdate()
    {
        // if the player has died
        if (gameOver)
        {
            // set the game over text, which tells the player how to restart, active
            gameOverText.SetActive(true);
            // if the player left clicks, reload the scene
            if (Input.GetMouseButton(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    int getRandomNum(int min, int max)
    {
        return Random.Range(min, max);
    }

    void spawn(char nsew, int index)
    {
        if (nsew == 'n')
            north_side_spawners[index].Spawn();
        else if (nsew == 's')
            south_side_spawners[index].Spawn();
        else if (nsew == 'e')
            east_side_spawners[index].Spawn();
        else if (nsew == 'w')
            west_side_spawners[index].Spawn();
        else
            Debug.Log("SPAWN ERROR");
    }

    IEnumerator spawn_routine()
    {
        yield return new WaitForSeconds(1f);
        spawn('n', getRandomNum(0, north_side_spawners.Length));
        spawn('s', getRandomNum(0, south_side_spawners.Length));
        spawn('e', getRandomNum(0, east_side_spawners.Length));
        spawn('w', getRandomNum(0, west_side_spawners.Length));
        StartCoroutine(spawn_routine());
    }

    public void playerDied()
    {
        gameOver = true;
    }
}
