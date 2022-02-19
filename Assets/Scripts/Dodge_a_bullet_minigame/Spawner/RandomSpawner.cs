using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameManagerScript board;
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public bool obstaclesSent = false;

    //public static int obstacleCounter = 15;

    void Update()
    {
        if (board.duringMinigame && !obstaclesSent)
        {
            obstaclesSent = true;
            StartCoroutine("Reset");
        }
    }

    /*void Start()
    {
        StartCoroutine("Reset");
    }*/

    IEnumerator Reset()
    {
        while (Timer.timeValue > 0.9)
        {

            yield return new WaitForSeconds(1f);

            //int randEnemy = Random.Range(0, enemyPrefabs.Length);
            int randSpawPoint1 = Random.Range(0, spawnPoints.Length);
            int randSpawPoint2 = Random.Range(0, spawnPoints.Length);

            //spawns 2 obstacles
            Instantiate(enemyPrefabs[0], spawnPoints[randSpawPoint1].position, transform.rotation);
            Instantiate(enemyPrefabs[0], spawnPoints[randSpawPoint2].position, transform.rotation);

            //DeleteObstacleOnTouch.obstacleCounter--;

            //obstacleCounter--;
            //Debug.Log(DeleteObstacleOnTouch.obstacleCounter);
        }

        board.duringMinigame = false;
        obstaclesSent = false;



        //You can put more yield return new WaitForSeconds(1); in one coroutine

        //StartCoroutine("Reset");
    }
}
