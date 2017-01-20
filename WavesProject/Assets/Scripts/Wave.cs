using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {

    static int size = 200; // Number of vertices
    static float velocityDamping = 0.999999f; // Proprotional velocity damping, must be less than or equal to 1.
    static float timeScale = 50f;

    float[] newHeight = new float[size];
    float[] velocity = new float[size];

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
        for (int i = 1; i < size - 1; i++)
        {
            int j = i - 1;
            int k = i + 1;
            newHeight[i] = (vertex[i].transform.position.y + vertex[j].transform.position.y + vertex[k].transform.position.y) / 3.0f;
        }

        // Velocity and height are updated...
        for (int i = 0; i < size; i++)
        {
            // update velocity and height
            velocity[i] = (velocity[i] + (newHeight[i] - vertex[i].transform.position.y)) * velocityDamping;

            float timeFactor = Time.deltaTime * timeScale;
            if (timeFactor > 1f) timeFactor = 1f;

            newHeight[i] += velocity[i] * timeFactor;

            // update the vertex position
            Vector3 newPosition = new Vector3(vertex[i].transform.position.x, newHeight[i], vertex[i].transform.position.z);
            vertex[i].transform.position = newPosition;
        }
    }
}