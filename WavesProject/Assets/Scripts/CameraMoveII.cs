using UnityEngine;
using System.Collections;

public class CameraMoveII : MonoBehaviour {

    public float speed;
    public float speedIncrease;
    public float screenDeathDis;
    public float rightScreenDis;
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
       

        pos = Camera.main.WorldToViewportPoint(playerTrans.position);

        if (pos.x < screenDeathDis)
        {
            gameOver = true;
            transform.Translate(0, 0, 0);
        }
        else if (pos.x > 1f - rightScreenDis)
        {
            transform.Translate(speed * speedIncrease, 0, 0);

            //transform.position = new Vector3(playerTrans.position.x + 5f, transform.position.y, transform.position.z);
        }
        else
        {
            transform.Translate(speed, 0, 0);
        }

        if (gameOver)
        {
            ggPanel.SetActive(true);

        }
        else
        {
            ggPanel.SetActive(false);

        }
	
	}
}
