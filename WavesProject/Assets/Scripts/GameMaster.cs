using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    [Header("Dificulty")]
    [Header("Easy")]
    public float easyDebriSpwnTime;
    public float easyDebriSpeed;

    public float easyWaveHeight;
    public float easyWaveFrequency;

    float easyTime;
    bool isEasy;
    [Header("Medium")]
    public float mediumTime;
    public bool isMedium;

    public float medDebriSpwnTime;
    public float medDebriSpeed;

    public float medWaveHeight;
    public float medWaveFrequency;
    [Header("Hard")]
    public float hardTime;
    public bool isHard;

    public float hardDebriSpwnTime;
    public float hardDebriSpeed;

    public float hardWaveHeight;
    public float hardWaveFrequency;

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

        if (difficultyTimer < easyTime) //Easy
        {

            debrisSpwn.spawnTime = easyDebriSpwnTime;
            debrisSpwn.speed = easyDebriSpeed;

            waveManager.waveHeight = easyWaveHeight;
            waveManager.frequency = easyWaveFrequency;

        }
        else if (difficultyTimer >= mediumTime) //Medium
        {

            isEasy = false;
            isMedium = true;


            debrisSpwn.spawnTime = medDebriSpwnTime;
            debrisSpwn.speed = medDebriSpeed;

            waveManager.waveHeight = medWaveHeight;
            waveManager.frequency = medWaveFrequency;
        }
        if (difficultyTimer >= hardTime) // Hard
        {

            isMedium = false;
            isHard = true;

            debrisSpwn.spawnTime = hardDebriSpwnTime;
            debrisSpwn.speed = hardDebriSpeed;

            waveManager.waveHeight = hardWaveHeight;
            waveManager.frequency = hardWaveFrequency;
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
