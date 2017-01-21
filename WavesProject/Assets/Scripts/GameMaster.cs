using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    [Header("Diificulty")]
    public float easyTime;
    public float mediumTime;
    public float hardTime;

    public bool isEasy;
    public bool isMedium;
    public bool isHard;

    float difficultyTimer;

    public bool pause;

	// Use this for initialization
	void Start ()
    {
        pause = false;

        difficultyTimer = 0;
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        difficultyTimer += Time.deltaTime;

        if (difficultyTimer >= easyTime)
        {
            //Easy
        }
        else if (difficultyTimer >= mediumTime)
        {
            //Medium
        }
        else if (difficultyTimer >= hardTime)
        {
            // Hard
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
