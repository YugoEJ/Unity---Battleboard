using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public DiceScript dice;
    //public DiceCheckZoneScript diceResult;

    public Player player;
    public Player computer;
    private Player currentPlayer;

    private bool gamePaused;
    private bool gameOver;

    //private int stepsToTake;

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
                if (Input.GetKeyDown(KeyCode.Space) && !player.IsMoving() && !computer.IsMoving())
                {
                    Move(player, computer);
                }
            }
            else
            {
                if (!computer.IsMoving() && !player.IsMoving())
                {
                    Move(computer, player);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P)) // game should be paused when a special effect is applied (item equipped, game initiated, etc.), for now, P will pause the game.
        {
            PauseGame();
        }
    }

    private void Move(Player currentPlayer, Player nextPlayer)
    {
        //RollDice();

        dice.Roll();

        StartCoroutine(currentPlayer.Move());

        // HERE we will check if the player landed on a special tile (with a method from within currentPlayer itself). if so, Pause the game and apply effect (buff/minigame/etc.), then Unpause.

        if (currentPlayer.CanMove()) // if current player CAN move, this means he hasn't reached the end, and the game can continue.
        {
            this.currentPlayer = nextPlayer;

            StartCoroutine(NextTurnDelay());
        }
        else
        {
            gameOver = true;
            Debug.Log(this.currentPlayer.name + " wins!");
            return;
        }
    }

    /*private void RollDice()
    {
        stepsToTake = Random.Range(1, 7);
        Debug.Log(currentPlayer.name + " rolled the dice on: " + stepsToTake);
    }*/



    private Player FlipCoin()
    {
        Player[] players = new Player[2];
        players[0] = player;
        players[1] = computer;

        Debug.Log("Flipping coin to decide who goes first!");
        int coin = Random.Range(0, 2);

        currentPlayer = players[coin];

        StartCoroutine(NextTurnDelay());

        return currentPlayer;
    }

    private IEnumerator NextTurnDelay()
    {
        PauseGame();
        yield return new WaitForSeconds(3 + ((currentPlayer.StepsToTake() / 2) + 2.5f));

        // CHECK IF CURRENT PLAYER LANDED ON SPECIAL TILE - IF SO, APPLY EFFECT.

        if (currentPlayer == player)
        {
            Debug.Log(currentPlayer.name + "'s turn.");
        }
        else
        {
            Debug.Log(currentPlayer.name + "'s turn.");
            yield return new WaitForSeconds(1);
        }

        PauseGame();
        yield return new WaitForSeconds(2.5f);
    }

    private void PauseGame()
    {
        gamePaused = !gamePaused;
    }
}