using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZoneScript : MonoBehaviour {

	private Vector3 diceVelocity;
	private static int stepsToTake;
	private static bool diceLanded;

	public static int StepsToTake()
    {
		return stepsToTake;
    }

	void FixedUpdate () {
		diceVelocity = DiceScript.diceVelocity;
	}

	void OnTriggerStay(Collider col)
	{
		if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
		{
			diceLanded = true;

			switch (col.gameObject.name) {
			case "Side1":
				//DiceNumberTextScript.diceNumber = 6;
					stepsToTake = 1;
				break;
			case "Side2":
				//DiceNumberTextScript.diceNumber = 5;
					stepsToTake = 2;
				break;
			case "Side3":
				//DiceNumberTextScript.diceNumber = 4;
					stepsToTake = 3;
				break;
			case "Side4":
				//DiceNumberTextScript.diceNumber = 3;
					stepsToTake = 4;
				break;
			case "Side5":
				//DiceNumberTextScript.diceNumber = 2;
					stepsToTake = 5;
				break;
			case "Side6":
				//DiceNumberTextScript.diceNumber = 1;
					stepsToTake = 6;
				break;
			}
		}
        else
        {
			diceLanded = false;
        }

		Debug.Log(stepsToTake);
	}

	public bool DiceLanded()
    {
		return diceLanded;
    }
}
