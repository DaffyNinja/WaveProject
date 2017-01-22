using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebrisSpawn : MonoBehaviour {

    public List<GameObject> debrisObj;
    [Space(10)]
    public float spawnTime;
    float timer;
    [Space(10)]
    public Transform debrisParent;
    public float destroyDistance;

    GameObject debris;
    GameObject player;
    GameObject[] tracker = new GameObject[100];
    int counter;

    float rng;

    bool spawn;
    bool create;

    // Use this for initialization
    void Start ()
    {
        spawn = true;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        //transform.Translate(speed, 0, 0);
        
        if (spawn == true)
        {
            rng = Random.Range(spawnTime / 4f, -spawnTime / 2f);

            timer += Time.deltaTime;

            if (timer >= spawnTime + rng)
            {
                create = true;
                timer = 0;
            }
            else
            {
                create = false;
            }

            if (create == true)
            {
                Quaternion rotation = Quaternion.identity;
                rng = Random.Range(0f, 360f);
                rotation.eulerAngles = new Vector3(0, 0, rng);
                debris = (GameObject)Instantiate(debrisObj[Random.Range(0, debrisObj.Count)], new Vector3(transform.position.x, transform.position.y, 1), rotation);

                debris.transform.parent = debrisParent;
                tracker[counter] = debris;
                counter++;
                if(counter >= 100)
                {
                    counter = 0;
                }

                create = false;
            }

            for(int i = 0; i <= counter; i++)
            {
                if (tracker[i] != null && tracker[i].transform.position.x < player.transform.position.x - 20f - destroyDistance)
                {
                    Destroy(tracker[i]);
                }
            }

        }
	
	}
}
