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
    float timer;

    // Use this for initialization
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= textTime)
        {
            storyTextUI.enabled = true;

            storyTextUI.text = Storytexts[index];
            index++;
            timer = 0;

        }
        else
        {
            storyTextUI.enabled = false;
        }

    }
}
