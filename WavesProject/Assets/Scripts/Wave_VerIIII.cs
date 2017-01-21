using UnityEngine;
using System.Collections;
using System;

public class Wave_VerIIII : MonoBehaviour {

    [Header("Wave Values")]

    //public int density;
    public float startDelay;
    public float waveHeight;
    public float frequency;
    float width = 0.5f;

    [Space(10)]
    public float xpos;
    public float ypos;
    [Space(10)]
    public float xspeed;
    public float yspeed;
    [Space(10)]
    public float generateDistance; //0f
    public float destroyDistance; //0f

    GameObject player;

    int density = 1;
    int particles;
    int size = 200; // Number of vertices
    int generator;
    int generatorTimer;
    int marker;
    int generateCount = 0;
    int destroyCount = 0;

    static float timeScale = 50f;

    float[] newHeight;
    float[] velocity;

    //David defined
    float time = 0f;
    float totalLength = 0f;
    float startPos;


    float pi = (float)Math.PI;

    GameObject[] vertex;

    [Space(10)]
    public GameObject waveSprite;

    public Transform waveParent;


    void Start()
    {
        size = size * density;
        particles = size;
        generator = size - 1;
        generatorTimer = 0;

        newHeight = new float[size * 4];
        velocity = new float[size * 4];

        vertex = new GameObject[size * 4];

        player = GameObject.FindGameObjectWithTag("Player");
        startPos = player.transform.position.x;

        // we'll use spheres to represent each vertex for demonstration purposes
        for (int i = 0; i < size; i++)
        {
            vertex[i] = Instantiate(waveSprite);
            vertex[i].transform.parent = waveParent;
            vertex[i].transform.position = new Vector2(startPos + ((float)i - (float)particles / 2f) * width / 1f + xpos, 0 + ypos);
        }
        totalLength = vertex[size - 1].transform.position.x - vertex[0].transform.position.x;
    }

    void Update()
    {
        // Water tension is simulated by a simple linear convolution over the height field.
        //this determines the y value and the y value alone.

        time += Time.deltaTime;

        //generate new waves
        if (player.transform.position.x > startPos + 50f + generateDistance + totalLength * generateCount)
        {
            generateCount++;
            float j = 200;
            for (int i = size; i < size + particles; i++)
            {
                vertex[i] = Instantiate(waveSprite);
                vertex[i].transform.parent = waveParent;
                vertex[i].transform.position = new Vector2(startPos + ((float)j + (float)(particles * (generateCount - 1)) - (float)particles / 2f) * width / 1f + xpos, 0 + ypos);
                j += 1f;
            }
            marker = size - 1;
            size += particles;
        }
        //delete old waves
        if (player.transform.position.x > startPos + totalLength + destroyDistance + totalLength * destroyCount)
        {
            destroyCount++;
            for (int i = 0; i < particles; i++)
            {
                Destroy(vertex[i]);
            }
            for (int i = 0; i < particles; i++)
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

            if (i < size - 1)
            {
                newHeight[i] = (vertex[i].transform.position.y + vertex[k].transform.position.y + vertex[k].transform.position.y + vertex[k].transform.position.y) / 4f;
            }
            else if (i > generator)
            {
                newHeight[i] = 0 + ypos;
            }

            if (generator < size - 1)
            {
                generatorTimer++;
                if (generatorTimer > 1500)
                {
                    generator++;
                    generatorTimer = 0;
                }
            }
            else if (generator > size - 1)
            {
                generator = size - 1;
            }

            if (time >= startDelay && i == generator)
            {
                newHeight[i] = waveHeight * (float)Math.Cos((time * frequency / 12) * 180 / pi) + ypos;

            }

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

            // update the vertex position
            Vector2 newPosition = new Vector2(xspeed + vertex[i].transform.position.x, yspeed + newHeight[i]);
            vertex[i].transform.position = newPosition;
        }
    }
}