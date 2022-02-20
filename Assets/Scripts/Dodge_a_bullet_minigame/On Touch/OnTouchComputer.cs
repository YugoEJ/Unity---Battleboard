using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchComputer : MonoBehaviour
{
    public GameManagerScript board;
    public Player player;
    public Player computer;
    public static int computerHealthPoints;

    public GameObject WinText;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "obstacle")
        {
            Destroy(col.gameObject);

            if (OnTouchPlayer.isGameOver == false)
            {
                computer.RemoveExtraLife();
                board.boardUI.PCExtraLifeText.text = "Extra Life: " + computer.GetExtraLife();
                computerHealthPoints--;

                if ((computerHealthPoints > 0 && OnTouchPlayer.playerHealthPoints <= 0) || (computerHealthPoints <= 0 && OnTouchPlayer.playerHealthPoints <= 0) || (computerHealthPoints <= 0 && OnTouchPlayer.playerHealthPoints >= 0)) 
                {
                    OnTouchPlayer.isGameOver = true;
                    WinText.gameObject.SetActive(true);
                    player.SetMinigameWinner();
                    computer.RemoveMinigameWinner();
                    board.boardSFX.winMinigameSFX.Play();
                    StartCoroutine(DelayGoToBoard());
                }
            }
        }
    }

    public IEnumerator DelayGoToBoard()
    {
        board.duringMinigame = false;
        OnTouchPlayer.isGameOver = true;

        yield return new WaitForSeconds(3f);

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
    }
}
