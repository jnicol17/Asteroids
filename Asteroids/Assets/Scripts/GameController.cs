using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour {

    public Spawner[] east_side_spawners;
    public Spawner[] west_side_spawners;
    public Spawner[] north_side_spawners;
    public Spawner[] south_side_spawners;

    public static GameController instance;

    private bool gameOver = false;

    public GameObject gameOverText;
    public GameObject newHighscoreText;

    // player score
    private int score = 0;

    // persistant data will be stored in gd
    GameDetails gd;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    

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

        // load the saved browser data
        GameDetailContainer.LoadedGameDetails = DataAccess.Load();

        // if there is browser data, store it in gd
        // otherwise, create new browser data and store it in gd
        if (GameDetailContainer.LoadedGameDetails != null)
        {
            gd = GameDetailContainer.LoadedGameDetails;
        }
        else
        {
            gd = new GameDetails();
        }

        scoreText.text = "Score: " + score.ToString();
        highscoreText.text = "Highscore: " + gd.highscore.ToString();

        StartCoroutine(spawn_routine());
    }

    // Update is called once per frame
    void Update () {

    }

    private void FixedUpdate()
    {
        // if the player presses m, mute all game sounds
        if (Input.GetKeyDown("m"))
        {
            // save the change
            gd.volumeOn = !gd.volumeOn;
            DataAccess.Save(gd);
            // mute
            AudioManager.instance.muteVolume(gd);
        }
        // if the player has died
        if (gameOver)
        {
            // set the game over text, which tells the player how to restart, active
            gameOverText.SetActive(true);

            if (score > gd.highscore)
            {
                gd.highscore = score;
                newHighscoreText.SetActive(true);
            }
            
            // save the updated gameDetails
            DataAccess.Save(gd);

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

    // Activate a spawner based on the direction (nsew)
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

    // Coroutine runs every second, spawns 4 meteors from random spawners in each direction
    IEnumerator spawn_routine()
    {
        yield return new WaitForSeconds(1f);
        spawn('n', getRandomNum(0, north_side_spawners.Length));
        spawn('s', getRandomNum(0, south_side_spawners.Length));
        spawn('e', getRandomNum(0, east_side_spawners.Length));
        spawn('w', getRandomNum(0, west_side_spawners.Length));
        StartCoroutine(spawn_routine());
    }

    // player has collided with meteor/barrier, end the game in fixedUpdate
    public void playerDied()
    {
        gameOver = true;
    }

    // every time bullet hits a meteor, increment score
    public void playerScored(int scoreAmount)
    {
        score += scoreAmount;
        scoreText.text = "Score: " + score.ToString();
        // if current score is higher than highscore, increment highscore
        if (score > gd.highscore)
        {
            highscoreText.text = "Highscore: " + score.ToString();
        }
        //Debug.Log(score);
        //Debug.Log("Saved High Score: " + gd.highscore);
    }
}
