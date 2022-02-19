using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchPlayer : MonoBehaviour
{
    public GameManagerScript board;
    public Player player;
    public Player computer;
    public static int healthPoints;
    public bool appliedEffects;

    public GameObject WinText;
    public GameObject LoseText;
    public GameObject Draw;

    public static bool isGameOver = false;

    void Update()
    {
        if (board.duringMinigame && !appliedEffects)
        {
            healthPoints = 1 + player.GetExtraLife();
            appliedEffects = true;
            isGameOver = false;
        }

        if (healthPoints != 0 && OnTouchComputer.healthPoints != 0 && Timer.timeValue <= 0)
        {
            Draw.gameObject.SetActive(true);
            WinText.gameObject.SetActive(false);
            LoseText.gameObject.SetActive(false);
            isGameOver = true;
            player.RemoveMinigameWinner();
            computer.RemoveMinigameWinner();
            StartCoroutine(DelayGoToBoard());
        }
        else
        {
            isGameOver = false;
        }
    }

    private void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "obstacle" && isGameOver == false)
        {
            Destroy(col.gameObject);
            player.RemoveExtraLife();
            board.boardUI.PlayerExtraLifeText.text = "Extra Life: " + player.GetExtraLife();
            healthPoints--;
        }

        if (healthPoints != 0 && OnTouchComputer.healthPoints == 0 && isGameOver == false)
        {
            WinText.gameObject.SetActive(true);
            isGameOver = true;
            player.SetMinigameWinner();
            computer.RemoveMinigameWinner();
            StartCoroutine(DelayGoToBoard());

        }

        if (healthPoints == 0 && OnTouchComputer.healthPoints != 0 && isGameOver == false)
        {
            LoseText.gameObject.SetActive(true);
            isGameOver = true;
            player.RemoveMinigameWinner();
            computer.SetMinigameWinner();
            StartCoroutine(DelayGoToBoard());
        }
    }

    public IEnumerator DelayGoToBoard()
    {
        yield return new WaitForSeconds(3f);

        player.RemoveSuperSpeed();
        computer.RemoveSuperSpeed();
        board.minigameCam.enabled = false;
        board.boardCam.enabled = true;
        board.boardSFX.boardBGM.Play();
        board.boardSFX.minigameBGM.Stop();
        board.boardUI.ShowAllTexts();
        appliedEffects = false;
        HideTexts();
        board.duringMinigame = false;
    }

    public void HideTexts()
    {
        WinText.gameObject.SetActive(false);
        LoseText.gameObject.SetActive(false);
        Draw.gameObject.SetActive(false);
    }
}
