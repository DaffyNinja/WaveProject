using UnityEngine;
using System;
using System.Collections;

public class Wave_Ver2 : MonoBehaviour {

    static int size = 200; // Number of vertices
    static float velocityDamping = 1f; // Proprotional velocity damping, must be less than or equal to 1.
    static float timeScale = 50f;

    float[] newHeight = new float[size];
    float[] velocity = new float[size];

    //David defined
    float time = 0f;
    float time2 = 0f;
    bool triggered = false;
    bool triggered2 = false;

    float pi = (float)Math.PI;

    GameObject[] vertex = new GameObject[size];

    public GameObject waveSprite;


    void Start()
    {
        // we'll use spheres to represent each vertex for demonstration purposes
        for (int i = 0; i < size; i++)
        {
            vertex[i] = Instantiate(waveSprite);
            vertex[i].transform.position = new Vector2(i - size / 2, 0);
        }
    }

    void Update()
    {
        // Water tension is simulated by a simple linear convolution over the height field.
        //this determines the y value and the y value alone.

        time += Time.deltaTime;

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
            if (time >= 3f && i == size - 1 /*&& triggered == false*/)
            {
                newHeight[i] = 30f * (float)Math.Cos((time / 12) * 180 / pi);
                triggered = true;
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
            Vector3 newPosition = new Vector3(vertex[i].transform.position.x, newHeight[i], vertex[i].transform.position.z);
            vertex[i].transform.position = newPosition;
        }
    }
}
