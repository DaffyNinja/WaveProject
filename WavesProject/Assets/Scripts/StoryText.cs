using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class StoryText : MonoBehaviour
{
    [TextArea]
    public string[] Storytexts;

    [Space(5)]
    public Text storyTextUI;
    Color texta;
    int index = 0;

    public float textTime;
    public float textStayTime;
    float stayTimer;
    float timer;

    bool textStayonScreen;

    // Use this for initialization
    void Start()
    {
        timer = 0f;
        stayTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= textTime)
        {
            storyTextUI.enabled = true;

           // storyTextUI.text = Storytexts[index];
            index++;

            textStayonScreen = true;

            timer = 0;
        }



        if (textStayonScreen)
        {
            stayTimer += Time.deltaTime;

            storyTextUI.text = Storytexts[index];

            if (stayTimer >= textStayTime)
            {


               

                textStayonScreen = false;
                stayTimer = 0;
            }

        }
        else
        {
            storyTextUI.enabled = false;

        }


    }

}
