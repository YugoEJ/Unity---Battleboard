using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    public Text TurnText;
    public Text TurnDividerText;
    public Text DiceResultText;
    public Text TotalDiceRollsText;
    public Text NextMinigameText;
    public Text SpecialEffectText;
    public Text PCExtraLifeText;
    public Text PlayerExtraLifeText;
    public GameObject PCTurnIndicator;
    public GameObject PlayerTurnIndicator;

    public void ShowAllTexts()
    {
        this.TurnText.enabled = true;
        this.TurnDividerText.enabled = true;
        this.DiceResultText.enabled = true;
        this.TotalDiceRollsText.enabled = true;
        this.NextMinigameText.enabled = true;
        this.SpecialEffectText.enabled = true;
        this.PCExtraLifeText.enabled = true;
        this.PlayerExtraLifeText.enabled = true;
    }

    public void HideAllTexts()
    {
        this.TurnText.enabled = false;
        this.TurnDividerText.enabled = false;
        this.DiceResultText.enabled = false;
        this.TotalDiceRollsText.enabled = false;
        this.NextMinigameText.enabled = false;
        this.SpecialEffectText.enabled = false;
        this.PCExtraLifeText.enabled = false;
        this.PlayerExtraLifeText.enabled = false;
    }

    public void SetPlayerTurn()
    {
        this.PlayerTurnIndicator.SetActive(true);
        this.PCTurnIndicator.SetActive(false);
    }

    public void SetPCTurn()
    {
        this.PlayerTurnIndicator.SetActive(false);
        this.PCTurnIndicator.SetActive(true);
    }
}
