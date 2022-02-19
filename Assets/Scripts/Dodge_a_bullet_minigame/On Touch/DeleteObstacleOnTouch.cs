using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObstacleOnTouch : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "obstacle")
        {
            Destroy(col.gameObject);
        }
    }
}