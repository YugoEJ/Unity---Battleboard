using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchComputer : MonoBehaviour
{
    public GameManagerScript board;
    public Player player;
    public Player computer;
    public static int computerHealthPoints;
    public bool appliedEffects;

    public GameObject WinText;
    public GameObject Draw;

    void Update()
    {
        /*if (board.duringMinigame && !appliedEffects)
        {
            healthPoints = 1 + computer.GetExtraLife();
            appliedEffects = true;
        }*/

        /*if (computerHealthPoints != 0 && OnTouchPlayer.playerHealthPoints != 0 && Timer.timeValue <= 0)
        {
            Draw.gameObject.SetActive(true);
            WinText.gameObject.SetActive(false);
            OnTouchPlayer.isGameOver = true;
            computer.RemoveMinigameWinner();
            StartCoroutine(DelayGoToBoard());
        }*/
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "obstacle" && OnTouchPlayer.isGameOver == false)
        {
            Destroy(col.gameObject);
            computer.RemoveExtraLife();
            board.boardUI.PCExtraLifeText.text = "Extra Life: " + computer.GetExtraLife();
            computerHealthPoints--;
        }

        if (computerHealthPoints == 0 && OnTouchPlayer.playerHealthPoints != 0 && OnTouchPlayer.isGameOver == false) 
        {
            OnTouchPlayer.isGameOver = true;
            WinText.gameObject.SetActive(true);
            player.SetMinigameWinner();
            computer.RemoveMinigameWinner();
            //board.boardSFX.winMinigameSFX.Play();
            StartCoroutine(DelayGoToBoard());
        }
    }

    public IEnumerator DelayGoToBoard()
    {
        //board.duringMinigame = false;
        appliedEffects = false;
        Timer.timeValue = 0f;

        yield return new WaitForSeconds(3f);

        board.duringMinigame = false;

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
        Draw.gameObject.SetActive(false);
    }
}
