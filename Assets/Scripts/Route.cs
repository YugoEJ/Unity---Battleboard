using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    Transform[] childObjects;
    public List<Transform> childObjectList = new List<Transform>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        FillNodes();

        for (int i = 0; i < childObjectList.Count; i++)
        {
            Vector3 currentPos = childObjectList[i].position;

            if (i > 0)
            {
                Vector3 prevPos = childObjectList[i - 1].position;
                Gizmos.DrawLine(prevPos, currentPos);
            }
        }
    }

    void FillNodes()
    {
        childObjectList.Clear();

        childObjects = GetComponentsInChildren<Transform>();

        foreach (Transform child in childObjects)
        {
            if (child != this.transform)
            {
                childObjectList.Add(child);
            }
        }
    }

}
