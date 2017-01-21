using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryText : MonoBehaviour
{

    public Transform canvas;

    public Text[] Storytexts;
    public Text storyText;
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
            storyText.text = Storytexts[index].text;
            index++;
            timer = 0;
        }

    }
}
