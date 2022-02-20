using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public Scene boardScene;
    public Scene mainMenuScene;

    public Camera boardCam;
    public Camera minigameCam;

    public Player player;
    public Player computer;
    private Player currentPlayer;

    public DiceScript dice;

    public UIManagerScript boardUI;
    public SFXManagerScript boardSFX;

    public bool duringMinigame;
    private bool gamePaused;
    private bool gameOver;
    private int currentTotalDiceRolls;
    private int requiredDiceRollsForMinigame = 10;
    private int requiredDiceRollsIncrement = 10;
    private float speed = 40f;

    private void Start()
    {
        boardScene = SceneManager.GetActiveScene();

        boardUI.ShowAllTexts();

        currentTotalDiceRolls = 0;

        currentPlayer = player;

        boardUI.TotalDiceRollsText.text += " " + 0;
        boardUI.DiceResultText.text = "" + 0;
        boardUI.SpecialEffectText.text += " None";
        boardUI.PlayerExtraLifeText.text += " 0";
        boardUI.PCExtraLifeText.text += " 0";
        boardUI.NextMinigameText.text += " " + requiredDiceRollsForMinigame + " Rolls";
        boardUI.ComputerWinsText.gameObject.SetActive(false);
        boardUI.PlayerWinsText.gameObject.SetActive(false);


        boardSFX.diceSFX.Play();
        boardSFX.boardBGM.Play();

        minigameCam.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(boardScene.name);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (!gamePaused && !gameOver && !duringMinigame)
        {
            if ((player.IsMinigameWinner() || computer.IsMinigameWinner()) && !duringMinigame)
            {
                if (player.IsMinigameWinner())
                {
                    player.RemoveMinigameWinner();
                    StartCoroutine(MoveWinningPlayer(player, 3));
                }

                if (computer.IsMinigameWinner())
                {
                    computer.RemoveMinigameWinner();
                    StartCoroutine(MoveWinningPlayer(computer, 3));
                }

                currentPlayer = player;
            } 
            else
            {
                if (currentPlayer == player)
                {
                    if (Input.GetKeyDown(KeyCode.Space) && !player.IsMoving() && !computer.IsMoving())
                    {
                        Move(player, computer);
                    }
                }
                else if (currentPlayer == computer)
                {
                    if (!computer.IsMoving() && !player.IsMoving())
                    {
                        Move(computer, player);
                    }
                }
            }
        }
    }

    private void Move(Player currentPlayer, Player nextPlayer)
    {
        boardSFX.diceSFX.Play();
        dice.Roll();

        StartCoroutine(MovePlayer(currentPlayer));

        // if current player CAN move, this means he hasn't reached the end, and the game will continue.
        if (currentPlayer.CanMove(DiceCheckZoneScript.StepsToTake()) && !this.duringMinigame) 
        {
            if (nextPlayer.IsSkippingTurn())
            {
                this.currentPlayer = currentPlayer;
                nextPlayer.RemoveSkipTurn();
            }
            else
            {
                this.currentPlayer = nextPlayer;
            }

            StartCoroutine(DiceRollDelay());
        }
        else if (!currentPlayer.CanMove(DiceCheckZoneScript.StepsToTake()))
        {
            // if this statement is true, currentPlayer is the winner. this statement should be considered in other Move methods as well (MoveWinningPlayer()).
            this.gameOver = true;

            if (currentPlayer == player)
            {
                boardUI.PlayerWinsText.gameObject.SetActive(true);
                boardUI.ComputerWinsText.gameObject.SetActive(false);
            }
            else
            {
                boardUI.ComputerWinsText.gameObject.SetActive(true);
                boardUI.PlayerWinsText.gameObject.SetActive(false);
            }

            return;
        }
        else
        {
            this.currentPlayer = nextPlayer;
        }
    }

    public IEnumerator MovePlayer(Player currentPlayer)
    {
        yield return new WaitForSeconds(3f);

        int stepsToTake = DiceCheckZoneScript.StepsToTake();
        this.currentTotalDiceRolls += stepsToTake;

        this.boardUI.DiceResultText.text = "" + stepsToTake;

        if (currentPlayer.IsMoving())
        {
            yield break;
        }
        currentPlayer.SetMoving(true);

        // loop for the moving animation
        while (stepsToTake > 0 && (currentPlayer.RoutePos() != (currentPlayer.currentRoute.tileList.Count - 1)))
        {
            PlayStepSound();

            Vector3 initialPos = currentPlayer.currentRoute.tileList[currentPlayer.RoutePos()].position;

            currentPlayer.SetRoutePos(currentPlayer.RoutePos() + 1);

            Vector3 finalPos = currentPlayer.currentRoute.tileList[currentPlayer.RoutePos()].position;
            Vector3 nextPos = currentPlayer.transform.position;

            //--------first-------step--------------//

            // first step coordinates
            if (currentPlayer.IsEdgePos(currentPlayer.RoutePos() - 1))
            {
                nextPos.z = finalPos.z;
            }
            else
            {
                nextPos.x = finalPos.x;
            }
            nextPos.y = 8f;


            // first step (only Y changes)
            while (currentPlayer.transform.position != nextPos)
            {
                MoveToNextNode(nextPos, currentPlayer);
                yield return null;
            }

            //---------second-------step-------------//

            // second step coordinates
            if (currentPlayer.IsEdgePos(currentPlayer.RoutePos() - 1))
            {
                nextPos.z = finalPos.z;
                nextPos.x = (finalPos.x + initialPos.x) / 2;
            }
            else
            {
                nextPos.x = finalPos.x;
                nextPos.z = (finalPos.z + initialPos.z) / 2;
            }
            nextPos.y = 12f;

            // second step (Y changes to slightly higher, and X/Z changes to ((initialPos.X/Z + finalPos.X/Z) / 2)
            while (currentPlayer.transform.position != nextPos)
            {
                MoveToNextNode(nextPos, currentPlayer);
                yield return null;
            }

            //---------third-----step----------------//

            // third step coordinates
            nextPos.x = finalPos.x;
            nextPos.y = 8f;
            nextPos.z = finalPos.z;

            // third step (Y changes to slightly lower, and X/Z changes to (finalPos.X/Z)
            while (currentPlayer.transform.position != nextPos)
            {
                MoveToNextNode(nextPos, currentPlayer);
                yield return null;
            }

            //---------final------step---------------//

            // final step coordinates
            nextPos.x = finalPos.x;
            nextPos.y = 5.5f;
            nextPos.z = finalPos.z;

            // final step (Y changes to original height (5.5f))
            while (currentPlayer.transform.position != nextPos)
            {
                MoveToNextNode(nextPos, currentPlayer);
                yield return null;
            }


            yield return new WaitForSeconds(0.1f);

            stepsToTake--;
        }

        currentPlayer.SetMoving(false);

        ApplySpecialTileEffect(currentPlayer, false);

        // THIS IS WHERE WE MAKE A STATEMENT THAT PAUSES THE GAME ONCE THE MINIGAME SHOULD BEGIN, AS DEMONSTRATED BELOW THIS COMMENT
        if (currentPlayer == this.computer)
        {
            boardUI.TotalDiceRollsText.text = "Total Dice Rolls: " + this.currentTotalDiceRolls;
        }

        // if the minigame threshold (this.stepsForMinigame) is met, pause the game and switch to the minigame
        if (currentPlayer == this.computer && this.currentTotalDiceRolls >= this.requiredDiceRollsForMinigame && !this.player.IsSkippingTurn())
        {
            this.requiredDiceRollsForMinigame += this.requiredDiceRollsIncrement;
            this.duringMinigame = true;
            StartCoroutine(StartMinigameDelay());
        }

        if (this.currentPlayer == player)
        {
            boardUI.SetPlayerTurn();
        }
        else
        {
            boardUI.SetPCTurn();
        }
    }

    private void MoveToNextNode(Vector3 goalNode, Player currentPlayer)
    {
        currentPlayer.transform.position = Vector3.MoveTowards(currentPlayer.transform.position, goalNode, speed * Time.deltaTime);
    }

    private IEnumerator MoveWinningPlayer(Player winningPlayer, int stepsToTake)
    {
        yield return new WaitForSeconds(4f);

        if (winningPlayer.IsMoving())
        {
            yield break;
        }
        winningPlayer.SetMoving(true);

        // loop for the moving animation
        while (stepsToTake > 0 && (winningPlayer.RoutePos() != (winningPlayer.currentRoute.tileList.Count - 1)))
        {
            PlayStepSound();

            Vector3 initialPos = winningPlayer.currentRoute.tileList[winningPlayer.RoutePos()].position;

            winningPlayer.SetRoutePos(winningPlayer.RoutePos() + 1);

            Vector3 finalPos = winningPlayer.currentRoute.tileList[winningPlayer.RoutePos()].position;
            Vector3 nextPos = winningPlayer.transform.position;

            //--------first-------step--------------//

            // first step coordinates
            if (winningPlayer.IsEdgePos(winningPlayer.RoutePos() - 1))
            {
                nextPos.z = finalPos.z;
            }
            else
            {
                nextPos.x = finalPos.x;
            }
            nextPos.y = 8f;


            // first step (only Y changes)
            while (winningPlayer.transform.position != nextPos)
            {
                MoveToNextNode(nextPos, winningPlayer);
                yield return null;
            }

            //---------second-------step-------------//

            // second step coordinates
            if (winningPlayer.IsEdgePos(winningPlayer.RoutePos() - 1))
            {
                nextPos.z = finalPos.z;
                nextPos.x = (finalPos.x + initialPos.x) / 2;
            }
            else
            {
                nextPos.x = finalPos.x;
                nextPos.z = (finalPos.z + initialPos.z) / 2;
            }
            nextPos.y = 12f;

            // second step (Y changes to slightly higher, and X/Z changes to ((initialPos.X/Z + finalPos.X/Z) / 2)
            while (winningPlayer.transform.position != nextPos)
            {
                MoveToNextNode(nextPos, winningPlayer);
                yield return null;
            }

            //---------third-----step----------------//

            // third step coordinates
            nextPos.x = finalPos.x;
            nextPos.y = 8f;
            nextPos.z = finalPos.z;

            // third step (Y changes to slightly lower, and X/Z changes to (finalPos.X/Z)
            while (winningPlayer.transform.position != nextPos)
            {
                MoveToNextNode(nextPos, winningPlayer);
                yield return null;
            }

            //---------final------step---------------//

            // final step coordinates
            nextPos.x = finalPos.x;
            nextPos.y = 5.5f;
            nextPos.z = finalPos.z;

            // final step (Y changes to original height (5.5f))
            while (winningPlayer.transform.position != nextPos)
            {
                MoveToNextNode(nextPos, winningPlayer);
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);

            stepsToTake--;
        }

        winningPlayer.SetMoving(false);
        ApplySpecialTileEffect(winningPlayer, true);
    }

    // the bool in this method checks if "skip-turn" should be applied (if the winner of the Minigame lands on a skip-turn tile, it will be nullified).
    private void ApplySpecialTileEffect(Player currentPlayer, bool afterMinigame)
    {
        string specialEffect = currentPlayer.currentRoute.GetSpecialTiles()[currentPlayer.RoutePos()].GetTileEffect();
        
        this.boardUI.SpecialEffectText.text = "Effect: " + specialEffect;

        switch (specialEffect)
        {
            case "None":
                break;

            case "Extra-Life":
                currentPlayer.AddExtraLife();
                this.boardSFX.extraLifeSFX.Play();

                if (currentPlayer == this.player)
                {
                    this.boardUI.PlayerExtraLifeText.text = "Extra-Life: " + currentPlayer.GetExtraLife();
                }
                else
                {
                    this.boardUI.PCExtraLifeText.text = "Extra-Life: " + currentPlayer.GetExtraLife();
                }
                break;

            /*case "Super-Speed":
                currentPlayer.GiveSuperSpeed();
                this.boardSFX.superSpeedSFX.Play();

                if (currentPlayer == this.player)
                {
                    this.boardUI.PlayerSuperSpeedText.text = "Super-Speed: " + currentPlayer.GetSuperSpeedForText();
                }
                else
                {
                    this.boardUI.PCSuperSpeedText.text = "Super-Speed: " + currentPlayer.GetSuperSpeedForText();
                }
                break;*/

            // if landed on skip-turn tile, the opponent may roll the dice twice. if skip-turn was already true, the opponent's skip-turn gets nullified.
            case "Skip-Turn":
                if (!afterMinigame)
                {
                    if (this.player.IsSkippingTurn() && this.currentPlayer == this.computer)
                    {
                        this.computer.RemoveSkipTurn();
                    }
                    else
                    {
                        this.boardSFX.skipTurnSFX.Play();
                        currentPlayer.SetSkipTurn();
                    }
                }
                break;
        }
    }

    // most of the actions taken in this method should be COUNTERED by the MINIGAME SCRIPTS when the minigame is over (e.g. Game.boardUI.ShowAllTexts() | Game.boardCam.enabled = true).
    private IEnumerator StartMinigameDelay()
    {
        yield return new WaitForSeconds(1.5f);
        this.boardUI.HideAllTexts();

        this.boardCam.enabled = false;
        this.minigameCam.enabled = true;

        this.boardSFX.boardBGM.Stop();
        this.boardSFX.minigameBGM.Play();

        this.boardUI.NextMinigameText.text = "Next Minigame: " + requiredDiceRollsForMinigame + " Rolls";
    }

    private IEnumerator DiceRollDelay()
    {
        PauseGame();
        yield return new WaitForSeconds(4f);
        PauseGame();
    }

    public void PauseGame()
    {
        gamePaused = !gamePaused;
    }

    public void PlayStepSound()
    {
        int sound = Random.Range(1, 5);

        switch (sound)
        {
            case 1: 
                boardSFX.stepOneSFX.Play();
                break;

            case 2:
                boardSFX.stepTwoSFX.Play();
                break;

            case 3:
                boardSFX.stepThreeSFX.Play();
                break;

            case 4:
                boardSFX.stepFourSFX.Play();
                break;
        }
    }
}