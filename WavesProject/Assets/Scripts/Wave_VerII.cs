using UnityEngine;
using System;
using System.Collections;

public class Wave_VerII : MonoBehaviour {

    [Header("Wave Values")]
    public float width;
    //public int density;
    public float startDelay;
    public float waveHeight;
    public float frequency;

    [Space(10)]
    public float xpos;
    public float ypos;
    [Space(10)]
    public float xspeed;
    public float yspeed;

    int density = 1;
    int particles;
    int size = 200; // Number of vertices
    //static float velocityDamping = 1f; // Proprotional velocity damping, must be less than or equal to 1.
    static float timeScale = 50f;

    //float[] newHeight = new float[size];
    //float[] velocity = new float[size];
    float[] newHeight;
    float[] velocity;

    //David defined
    float time = 0f;
    float totalLength = 0f;
    //float time2 = 0f;
    //bool triggered = false;
    //bool triggered2 = false;

    float pi = (float)Math.PI;

    //GameObject[] vertex = new GameObject[size];
    GameObject[] vertex;

    [Space(10)]
    public GameObject waveSprite;


    void Start()
    {
        size = size * density;
        particles = size;

        newHeight = new float[size * 4];
        velocity = new float[size * 4];

        vertex = new GameObject[size * 4];

        // we'll use spheres to represent each vertex for demonstration purposes
        for (int i = 0; i < size; i++)
        {
            vertex[i] = Instantiate(waveSprite);
            vertex[i].transform.position = new Vector2(((float)i - (float)particles / 2f) * width / 1f + xpos, 0 + ypos);
        }
        totalLength = vertex[size - 1].transform.position.x - vertex[0].transform.position.x;
    }

    void Update()
    {
        // Water tension is simulated by a simple linear convolution over the height field.
        //this determines the y value and the y value alone.

        time += Time.deltaTime;

        //generate new waves
        if(vertex[size-1].transform.position.x < 80f)
        {
            float j = 200;
            for (int i = size; i < size + particles; i++)
            {
                vertex[i] = Instantiate(waveSprite);
                vertex[i].transform.position = new Vector2(( (float)j - (float)particles / 2f) * width / 1f + xpos -4.5f, 0 + ypos);
                j += 1f;
            }
            size += particles;
        }
        //delete old waves
        if (vertex[particles - 1].transform.position.x < -10f)
        {
            for (int i = 0; i < particles; i++)
            {
                Destroy(vertex[i]);
            }
            for(int i = 0; i < particles; i++)
            {
                vertex[i] = vertex[i + particles];
            }
            size -= particles;
        }


        for (int i = 1; i <= size - 1; i++)
        {
            // int j = i - 1;

            int k = i + 1;
            //the new height is the average between the previous particle, the current particle and next particle

            //newHeight[i] = (vertex[i].transform.position.y + vertex[j].transform.position.y + vertex[k].transform.position.y) / 3.0f;
            if (i < size - 1)
            {
                newHeight[i] = (vertex[i].transform.position.y + vertex[k].transform.position.y + vertex[k].transform.position.y + vertex[k].transform.position.y) / 4f;
            }
            //David's code
            if (time >= startDelay && i == size - 1 /*&& triggered == false*/)
            {
                newHeight[i] = waveHeight * (float)Math.Cos((time * frequency / 12) * 180 / pi);
                //triggered = true;
            }
            //if (time >= 3.2f && i == size - 1 /*&& triggered == false*/)
            //{
             //   newHeight[i] = 0f; //* (float)Math.Cos();
              //  triggered2 = true;
            //}
        }

        // Velocity and height are updated...
        for (int i = 0; i < size; i++)
        {
            // update velocity and height
            // in essence this only modifies the y value, the difference between newheight and vertex y(which is 0 at the start)
            velocity[i] = (velocity[i] + (newHeight[i] - vertex[i].transform.position.y));//* velocityDamping;

            //frame time (no larger than 1) * 50
            float timeFactor = Time.deltaTime * timeScale;
            if (timeFactor > 2f) timeFactor = 2f;

            //newHeight[i] = velocity[i];// * timeFactor;

            // update the vertex position
            Vector2 newPosition = new Vector2(xspeed + vertex[i].transform.position.x, yspeed + newHeight[i]);
            vertex[i].transform.position = newPosition;
        }
    }
}
