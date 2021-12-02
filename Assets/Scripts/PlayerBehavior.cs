using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public Route currentRoute;

    public float speed = 50f;

    int routePos;

    int dieOne;
    int dieTwo;
    int stepsToTake;

    bool isMoving;
    //bool isPlayerTurn; // this boolean will be required to seperate the turns appropriately, perhaps change its access modifier to Static...

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            RollDice();             // last index = 143

                                                    // count = 144
            if ((routePos + stepsToTake) < (currentRoute.childObjectList.Count - 1))  // if the amount of steps to take does not overflow, move player piece
            {
                StartCoroutine(Move());
                Debug.Log("Player moving...");
            } 
            else
            {
                StartCoroutine(Move());
                Debug.Log("We have a winner...");

                // announce winner
            }
        }
    }

    /*IEnumerator MoveToFinalNode()
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        while (stepsToTake > 0 && (routePos != (currentRoute.childObjectList.Count - 1)))
        {
            routePos++;

            Vector3 nextPos = currentRoute.childObjectList[routePos].position;

            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }

            //yield return new WaitForSeconds(0.1f);

            stepsToTake--;
        }
    }*/

    IEnumerator Move()
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        while (stepsToTake > 0 && (routePos != (currentRoute.childObjectList.Count - 1)))
        {
            routePos++;

            Vector3 nextPos = currentRoute.childObjectList[routePos].position;

            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }

            //yield return new WaitForSeconds(0.1f);

            stepsToTake--;
        }

        isMoving = false;

        // check if landed on final tile; announce winner

        // check if landed on a special tile; apply special effect / initiate minigame / etc.
    }

    bool MoveToNextNode(Vector3 goalNode)
    {
        return goalNode != (transform.position = Vector3.MoveTowards(transform.position, goalNode, speed * Time.deltaTime));
    }

    void RollDice()
    {
        dieOne = Random.Range(25, 40);
        dieTwo = Random.Range(15, 30);
        stepsToTake = dieOne + dieTwo;

        Debug.Log("Dice rolled: " + stepsToTake);
    }
}
