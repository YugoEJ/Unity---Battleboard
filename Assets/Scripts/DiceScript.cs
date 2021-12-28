using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour {

	Rigidbody rb;
	public static Vector3 diceVelocity;

	void Start () {

		rb = GetComponent<Rigidbody> ();
	}

	void Update()
	{

		diceVelocity = rb.velocity;

		if (Input.GetKeyDown(KeyCode.D))
		{
			Roll();
		}
	}

	public void Roll()
    {

		//DiceNumberTextScript.diceNumber = 0;
		float dirX = Random.Range(100, 500);
		float dirY = Random.Range(100, 500);
		float dirZ = Random.Range(100, 500);
		transform.position = new Vector3(-60.9f, 41.1f, 160.5f);
		//transform.rotation = Quaternion.identity;
		rb.AddForce(transform.up * (-200));
		rb.AddTorque(dirX, dirY, dirZ);
	}
}
