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
    public Text SpecialEffectText;
    public Text PCExtraLifeText;
    public Text PCSuperSpeedText;
    public Text PlayerExtraLifeText;
    public Text PlayerSuperSpeedText;
    public GameObject PCTurnIndicator;
    public GameObject PlayerTurnIndicator;

    public void ShowAllTexts()
    {
        this.TurnText.enabled = true;
        this.DiceResultText.enabled = true;
        this.TotalDiceRollsText.enabled = true;
        this.SpecialEffectText.enabled = true;
        this.PCExtraLifeText.enabled = true;
        this.PCSuperSpeedText.enabled = true;
        this.PlayerExtraLifeText.enabled = true;
        this.PlayerSuperSpeedText.enabled = true;
    }

    public void HideAllTexts()
    {
        this.TurnText.enabled = false;
        this.TurnDividerText.enabled = false;
        this.DiceResultText.enabled = false;
        this.TotalDiceRollsText.enabled = false;
        this.SpecialEffectText.enabled = false;
        this.PCExtraLifeText.enabled = false;
        this.PCSuperSpeedText.enabled = false;
        this.PlayerExtraLifeText.enabled = false;
        this.PlayerSuperSpeedText.enabled = false;
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
