using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGenerator : MonoBehaviour
{

    public List<GameObject> terrainPieces;
    [Space(10)]
    public List<GameObject> cloudObjs;
    GameObject[] clouds;
    [Space(10)]
    public float terrainDis;
    public float destroyNum;

    bool create;
    GameObject[] tracks;
    [Space(10)]
    public float yDis;
    public Transform playerTrans;
    Vector2 playerPos;


    // Use this for initialization
    void Awake()
    {
        playerPos = playerTrans.position;

    }

    // Update is called once per frame
    void Update()
    {
        tracks = GameObject.FindGameObjectsWithTag("Track");
        clouds = GameObject.FindGameObjectsWithTag("Cloud");

        TrackMaintanennce();
    }

    void TrackMaintanennce()
    {
        GameObject[] terrainObjs = GameObject.FindGameObjectsWithTag("Track");

        foreach (GameObject ter in terrainObjs)
        {
            if (ter.transform.position.x < playerTrans.position.x - destroyNum)
            {
                Destroy(ter);
            }

        }

        foreach (GameObject c in clouds)
        {
            if (c.transform.position.x < playerTrans.position.x - destroyNum)
            {
                Destroy(c);
            }
        }

        TrackPositions();

    }

    void TrackPositions()
    {
        Vector2 pos1 = new Vector2(playerTrans.position.x + terrainDis, playerPos.y - yDis);
        Vector2 pos2 = new Vector2(playerTrans.position.x + terrainDis * 2, playerPos.y - yDis);
        Vector2 pos3 = new Vector2(playerTrans.position.x + terrainDis * 3, playerPos.y - yDis);
        Vector2 pos4 = new Vector2(playerTrans.position.x + terrainDis * 4, playerPos.y - yDis);
        Vector2 pos5 = new Vector2(playerTrans.position.x + terrainDis * 5, playerPos.y - yDis);

        TrackCreate(pos1, pos2, pos3, pos4, pos5);

        Vector2 cPos1 = new Vector2(playerTrans.position.x + Random.Range(-10,15), playerPos.y + 5.75f);
        Vector2 cPos2 = new Vector2(playerTrans.position.x + Random.Range(-10, 15), playerPos.y + 5.75f);
        Vector2 cPos3 = new Vector2(playerTrans.position.x + Random.Range(-10, 15), playerPos.y + 5.75f);
        Vector2 cPos4 = new Vector2(playerTrans.position.x + Random.Range(-10, 15), playerPos.y + 5.75f);

        CloudCreate(cPos1, cPos2, cPos3, cPos4);


    }

    void CloudCreate(Vector2 cloudPos1, Vector2 cloudPos2, Vector2 cloudPos3, Vector2 cloudPos4)
    {
        bool createCloud = true;

        foreach (GameObject c in clouds)
        {
            if (cloudPos1.x < c.transform.position.x + 120f)
            {
                createCloud = false;
            }
        }

        if (createCloud)
        {
            Instantiate(cloudObjs[Random.Range(0, terrainPieces.Count)], cloudPos1, Quaternion.identity);
            Instantiate(cloudObjs[Random.Range(0, terrainPieces.Count)], cloudPos2, Quaternion.identity);
            Instantiate(cloudObjs[Random.Range(0, terrainPieces.Count)], cloudPos3, Quaternion.identity);
            Instantiate(cloudObjs[Random.Range(0, terrainPieces.Count)], cloudPos4, Quaternion.identity);
        }
    }

    void TrackCreate(Vector2 trackPos1, Vector2 trackPos2, Vector2 trackPos3, Vector2 trackPos4, Vector2 trackPos5)
    {

        create = true;

        foreach (GameObject t in tracks)
        {
            if (trackPos1.x < t.transform.position.x)
            {
                create = false;
            }
        }

        if (create)
        {
            Instantiate(terrainPieces[Random.Range(0, terrainPieces.Count)], trackPos1, Quaternion.identity);
            Instantiate(terrainPieces[Random.Range(0, terrainPieces.Count)], trackPos2, Quaternion.identity);
            Instantiate(terrainPieces[Random.Range(0, terrainPieces.Count)], trackPos3, Quaternion.identity);
            Instantiate(terrainPieces[Random.Range(0, terrainPieces.Count)], trackPos4, Quaternion.identity);
            Instantiate(terrainPieces[Random.Range(0, terrainPieces.Count)], trackPos5, Quaternion.identity);
        }


    }
}
