using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

    [Header("Dificulty")]
    float easyTime;
    public float mediumTime;
    public float hardTime;

    bool isEasy;
    public bool isMedium;
    public bool isHard;

    float difficultyTimer;

    public bool pause;

    public DebrisSpawn debrisSpwn;
    public Wave_VerII waveManager;

    // Use this for initialization
    void Start()
    {
        pause = false;

        difficultyTimer = 0;

        isEasy = true;

    }

    // Update is called once per frame
    void Update()
    {
        difficultyTimer += Time.deltaTime;


        if (difficultyTimer >= mediumTime)
        {
            //Medium

            isEasy = false;
            isMedium = true;

            debrisSpwn.spawnTime = 2f;
            debrisSpwn.speed = 0.075f;

            waveManager.waveHeight = 1f;
            waveManager.frequency = 0.75f;

        }
        if (difficultyTimer >= hardTime)
        {
            // Hard
          //  print("Hard");

            isMedium = false;
            isHard = true;

            debrisSpwn.spawnTime = 1f;
            debrisSpwn.speed = 0.05f;

            waveManager.waveHeight = 1.75f;
            waveManager.frequency = 1f;
        }

        // Increase dificulty

        if (pause == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //SceneManager.LoadScene(0);

            pause = !pause;
        }

    }
}
