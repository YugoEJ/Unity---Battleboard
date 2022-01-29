using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;

    public static int landmineCounter = 15;

    void Start()
    {
        StartCoroutine("Reset");
    }

    IEnumerator Reset()
    {
        while (landmineCounter > 0)
        {

            yield return new WaitForSeconds(1.5f);

            int randEnemy = Random.Range(0, enemyPrefabs.Length);
            int randSpawPoint = Random.Range(0, spawnPoints.Length);

            Instantiate(enemyPrefabs[0], spawnPoints[randSpawPoint].position, transform.rotation);

            landmineCounter--;
        }

        //You can put more yield return new WaitForSeconds(1); in one coroutine

        //StartCoroutine("Reset");
    }

    void Update()
    {

    }



}
