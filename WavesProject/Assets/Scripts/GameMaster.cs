using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    [Header("Dificulty")]
    [Header("Easy")]
    public bool isEasy;
    public float easyTime;

    public float easyDebriSpwnTime;
    public float easyDebriSpeed;

    public float easyWaveHeight;
    public float easyWaveFrequency;

    public float easyCameraSpeed;


    [Header("Medium")]
    public float mediumTime;
    public bool isMedium;

    public float medDebriSpwnTime;
    public float medDebriSpeed;

    public float medWaveHeight;
    public float medWaveFrequency;

    public float mediumCameraSpeed;
    [Header("Hard")]
    public float hardTime;
    public bool isHard;

    public float hardDebriSpwnTime;
    public float hardDebriSpeed;

    public float hardWaveHeight;
    public float hardWaveFrequency;

    public float hardCameraSpeed;

    float difficultyTimer;

    [FMODUnity.EventRefAttribute]
    FMOD.Studio.EventInstance musicSfx;

    public bool pause;

    public CameraMoveII camMove;
    public DebrisSpawn debrisSpwn;
    public Wave_VerIIII waveManager;

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
            isEasy = true; 

            debrisSpwn.spawnTime = easyDebriSpwnTime;
            debrisSpwn.speed = easyDebriSpeed;

            waveManager.waveHeight = easyWaveHeight;
            waveManager.frequency = easyWaveFrequency;

            camMove.speed = easyCameraSpeed;

        }
        else if (difficultyTimer >= mediumTime) //Medium
        {

            isEasy = false;
            isMedium = true;

            camMove.speed = mediumCameraSpeed;

            debrisSpwn.spawnTime = medDebriSpwnTime;
            debrisSpwn.speed = medDebriSpeed;

            waveManager.waveHeight = medWaveHeight;
            waveManager.frequency = medWaveFrequency;
        }
        if (difficultyTimer >= hardTime) // Hard
        {

            isMedium = false;
            isHard = true;

            camMove.speed = hardCameraSpeed;

            debrisSpwn.spawnTime = hardDebriSpwnTime;
            debrisSpwn.speed = hardDebriSpeed;

            waveManager.waveHeight = hardWaveHeight;
            waveManager.frequency = hardWaveFrequency;
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
        }

    }

    public void PlayAgainButton()
    {

        FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/sfx_UI_Play", transform.position);
        SceneManager.LoadScene(1);
    }

    public void MainMenuButton()
    {

        FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/sfx_UI_Play", transform.position);
        SceneManager.LoadScene(0);
    }

    public void QuitButton()
    {

        FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/sfx_UI_Play", transform.position);       
        Application.Quit();
    }
}
