using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public Player player;
    public Player computer;
    private Player currentPlayer;

    private bool gamePaused;
    private bool gameOver;

    private int dice;
    private int stepsToTake;

    private void Start()
    {
        currentPlayer = FlipCoin();
    }

    private void Update()
    {
        if (!gamePaused && !gameOver)
        {
            if (currentPlayer == player)
            {
                if (Input.GetKeyDown(KeyCode.Space) && !player.isMoving && !computer.isMoving)
                {
                    Move(player, computer);
                }
            }
            else
            {
                if (!computer.isMoving && !player.isMoving)
                {
                    Move(computer, player);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P)) // game should be paused when a special effect is applied (item equipped, game initiated, etc.), for now, P will pause the game.
        {
            gamePaused = !gamePaused;
            Debug.Log("PAUSED");
        }
    }

    private void Move(Player currentPlayer, Player nextPlayer)
    {
        RollDice();

        if (currentPlayer.CanMove(stepsToTake))
        {
            StartCoroutine(currentPlayer.Move(stepsToTake));
            this.currentPlayer = nextPlayer;
            Debug.Log(this.currentPlayer.name + " 's turn.");
        }
        else
        {
            StartCoroutine(currentPlayer.Move(stepsToTake));
            gameOver = true;
            Debug.Log(this.currentPlayer.name + " wins!");
        }
    }

    private void RollDice()
    {
        this.stepsToTake = Random.Range(1, 7);

        Debug.Log(currentPlayer.name + " is rolling the dice...\n" + "Dice rolled: " + this.stepsToTake);
    }

    private Player FlipCoin()
    {
        Player[] players = new Player[2];
        players[0] = player;
        players[1] = computer;

        Debug.Log("Flipping coin to decide who goes first!");
        int coin = Random.Range(0, 2);

        currentPlayer = players[coin];
        Debug.Log(currentPlayer.name + " goes first!");

        return currentPlayer;
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(3.0f);
    }
}
