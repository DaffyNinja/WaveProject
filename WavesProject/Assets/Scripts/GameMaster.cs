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

           // print("Medium");

            isEasy = false;


            isMedium = true;

            waveManager.waveHeight = 1.5f;
            waveManager.frequency = 1f;

        }
        if (difficultyTimer >= hardTime)
        {
            // Hard
          //  print("Hard");

            isMedium = false;
            isHard = true;

            waveManager.waveHeight = 2.25f;
            waveManager.frequency = 1.5f;
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
