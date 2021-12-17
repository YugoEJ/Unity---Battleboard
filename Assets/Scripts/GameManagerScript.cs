using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public Player player;
    public Computer computer;

    private bool playerTurn;
    private bool coinFlip;

    private int dice;
    private int stepsToTake;


    /*private void Awake()
    {
        coinFlip = FlipCoin();

        if (coinFlip == true)
        {
            playerTurn = true;
        }

        GameFlow(player, computer);
    }*/

    private void Update()
    {
        player.Update();
        computer.Update();
    }

    private void GameFlow(Player player, Computer computer)
    {
        while (true)
        {
            
        }
    }



    void RollDice()
    {
        dice = Random.Range(1, 7);
        stepsToTake = dice;

        Debug.Log("Player rolled dice: " + stepsToTake);
    }

    bool FlipCoin()
    {
        int coin = Random.Range(0, 2);
        bool playerBegins = false;

        if (coin == 0)
        {
            playerBegins = true;
        }

        return playerBegins;
    }
}
