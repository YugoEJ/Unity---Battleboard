using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchPlayer : MonoBehaviour
{
    public GameManagerScript board;
    public Player player;
    public Player computer;
    public static int playerHealthPoints;
    public bool appliedEffects;

    public GameObject WinText;
    public GameObject LoseText;
    public GameObject Draw;

    public static bool isGameOver = false;

    void Update()
    {
        if (board.duringMinigame && !appliedEffects)
        {
            Timer.timeValue = 10f;

            playerHealthPoints = 1 + player.GetExtraLife();
            OnTouchComputer.computerHealthPoints = 1 + computer.GetExtraLife();

            appliedEffects = true;
            isGameOver = false;
        }

        if (playerHealthPoints != 0 && OnTouchComputer.computerHealthPoints != 0 && Timer.timeValue <= 0 && isGameOver == false)
        {
            Draw.gameObject.SetActive(true);
            WinText.gameObject.SetActive(false);
            LoseText.gameObject.SetActive(false);
            isGameOver = true;
            player.RemoveMinigameWinner();
            computer.RemoveMinigameWinner();
            StartCoroutine(DelayGoToBoard());
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "obstacle" && isGameOver == false)
        {
            Destroy(col.gameObject);
            player.RemoveExtraLife();
            board.boardUI.PlayerExtraLifeText.text = "Extra Life: " + player.GetExtraLife();
            playerHealthPoints--;
            //board.boardSFX.hurtMinigameSFX.Play();
        }

        if (playerHealthPoints != 0 && OnTouchComputer.computerHealthPoints == 0 && isGameOver == false)
        {
            isGameOver = true;
            WinText.gameObject.SetActive(true);
            player.SetMinigameWinner();
            computer.RemoveMinigameWinner();
            //board.boardSFX.winMinigameSFX.Play();
            StartCoroutine(DelayGoToBoard());
        }

        if (playerHealthPoints == 0 && OnTouchComputer.computerHealthPoints != 0 && isGameOver == false)
        {
            isGameOver = true;
            LoseText.gameObject.SetActive(true);
            player.RemoveMinigameWinner();
            computer.SetMinigameWinner();
            //board.boardSFX.loseMinigameSFX.Play();
            StartCoroutine(DelayGoToBoard());
        }
    }

    public IEnumerator DelayGoToBoard()
    {
        appliedEffects = false;
        isGameOver = true;
        board.duringMinigame = false;

        Timer.timeValue = 0f;

        yield return new WaitForSeconds(1f);

        board.minigameCam.enabled = false;
        board.boardCam.enabled = true;
        board.boardSFX.boardBGM.Play();
        board.boardSFX.minigameBGM.Stop();
        board.boardUI.ShowAllTexts();
        HideTexts();

    }

    public void HideTexts()
    {
        WinText.gameObject.SetActive(false);
        LoseText.gameObject.SetActive(false);
        Draw.gameObject.SetActive(false);
    }
}
