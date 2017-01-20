using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGenerator : MonoBehaviour
{

    public List<GameObject> terrainPieces;

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
