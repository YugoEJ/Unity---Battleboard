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
    int stepsToTake;

    bool isMoving;
    //bool isPlayerTurn; // this boolean will be required to seperate the turns appropriately, perhaps change its access modifier to Static...

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            RollDice();

            if ((routePos + stepsToTake) < currentRoute.childObjectList.Count)  // if the amount of steps to take does not overflow, move player piece
            {
                StartCoroutine(Move());

            }
            else if ((routePos + stepsToTake) > currentRoute.childObjectList.Count) // if the amount of steps to take overflows, re-roll dice
            {
                do
                {
                    RollDice();
                } while ((routePos + stepsToTake) > currentRoute.childObjectList.Count);

                StartCoroutine(Move());
            } else
            {
                Debug.Log("We have a winner");
                // announce winner etc.
            }
        }
    }

    IEnumerator Move()
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        while (stepsToTake > 0)
        {
            routePos++;

            Vector3 nextPos = currentRoute.childObjectList[routePos].position;

            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);

            stepsToTake--;
        }

        stepsToTake = 9;
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
        dieOne = Random.Range(1, 7);
        dieTwo = Random.Range(1, 7);
        stepsToTake = dieOne + dieTwo;

        Debug.Log("Dice rolled: " + stepsToTake);
    }
}
