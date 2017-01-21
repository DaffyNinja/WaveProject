using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebrisSpawn : MonoBehaviour {

    public List<GameObject> debrisObj;
    [Space(10)]
    public float spawnTime;
    float timer;
    [Space(5)]
    public float speed;

    bool spawn;
    bool create;

    // Use this for initialization
    void Start ()
    {
        spawn = true;
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(speed, 0, 0);

        if (spawn == true)
        {
            timer += Time.deltaTime;

            if (timer >= spawnTime)
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
                Instantiate(debrisObj[Random.Range(0, debrisObj.Count)], new Vector3(transform.position.x,transform.position.y,1), Quaternion.identity);
                create = false;
            }

        }
	
	}
}
