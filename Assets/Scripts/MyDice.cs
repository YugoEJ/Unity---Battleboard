using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDice : MonoBehaviour
{
    public GameObject dice;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            dice.transform.position = new Vector3(-60.9f, 41.1f, 170f);
        }
    }

    void Roll()
    {
        
    }
}
