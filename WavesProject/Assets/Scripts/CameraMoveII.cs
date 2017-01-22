using UnityEngine;
using System.Collections;

public class CameraMoveII : MonoBehaviour {

    public float speed;
    public float screenDeathDis;
    public float rightScreenDis;
    Vector3 pos;

    [Space(5)]
    public float xDis;
    public float lerpSpeed;

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
        else if (pos.x > 1f - rightScreenDis)  // Right screen
        {
            //transform.Translate(speed * speedIncrease, 0, 0);

            transform.position = Vector3.Lerp(new Vector3(playerTrans.position.x + xDis, transform.position.y, transform.position.z),new Vector3(transform.position.x,transform.position.y,transform.position.z),lerpSpeed);
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
