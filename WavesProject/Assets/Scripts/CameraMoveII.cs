using UnityEngine;
using System.Collections;

public class CameraMoveII : MonoBehaviour {

    public float speed;

    public float screenDeathDis;
    Vector3 pos;

    public GameObject ggPanel;
    public bool gameOver;

    public Transform playerTrans;




	// Use this for initialization
	void Start ()
    {

      //  pos = Camera.main.WorldToViewportPoint(playerTrans.position);

       

	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(speed, 0, 0);

        pos = Camera.main.WorldToViewportPoint(playerTrans.position);

        if (pos.x < screenDeathDis)
        {
            gameOver = true;
        }

        if (gameOver)
        {
            ggPanel.SetActive(true);

            //Time.timeScale = 0;
        }
        else
        {
            ggPanel.SetActive(false);

            //Time.timeScale = 0;
        }
	
	}
}
