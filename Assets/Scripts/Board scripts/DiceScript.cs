using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour {

	Rigidbody rb;
	public static Vector3 diceVelocity;
	private bool diceLanded;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	void Update()
	{
		diceVelocity = rb.velocity;
	}

	public void Roll()
    {
		float dirX = Random.Range(100, 500);
		float dirY = Random.Range(100, 500);
		float dirZ = Random.Range(100, 500);

		transform.position = new Vector3(2.78f, Random.Range(81f, 83f), 166.98f);
		transform.rotation = Quaternion.identity;
		rb.AddTorque(dirX, dirY, dirZ);
		rb.velocity = (transform.up * Random.Range(3, 6));
	}
}
