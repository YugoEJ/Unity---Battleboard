using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    public Route currentRoute;

    public float speed = 50f;
    int routePos;

    int dieOne;
    int dieTwo;
    public int steps;

    bool isMoving;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            dieOne = Random.Range(1, 7);
            dieTwo = Random.Range(1, 7);
            steps = dieOne + dieTwo;

            Debug.Log("Dice rolled: " + steps);

            StartCoroutine(Move());

            /*if ((routePos + steps) < currentRoute.childObjectList.Count) 
            {
                StartCoroutine(Move());
            }
            else
            {
                Debug.Log("Rolled a number that is too high");
            }*/
        }
    }

    IEnumerator Move()
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        while (steps > 0)
        {
            routePos++;
            routePos %= currentRoute.childObjectList.Count;

            if ((routePos + 1) > currentRoute.childObjectList.Count)
            {

            } 

            Vector3 nextPos = currentRoute.childObjectList[routePos].position;

            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);

            steps--;
            //routePos++;
        }

        isMoving = false;
    }

    bool MoveToNextNode(Vector3 goalNode)
    {
        return goalNode != (transform.position = Vector3.MoveTowards(transform.position, goalNode, speed * Time.deltaTime));
        
    }
}
