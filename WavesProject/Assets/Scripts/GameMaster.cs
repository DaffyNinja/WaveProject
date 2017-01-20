using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    public bool pause;

	// Use this for initialization
	void Start ()
    {
        pause = false;
	
	}
	
	// Update is called once per frame
	void Update ()
    {


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
