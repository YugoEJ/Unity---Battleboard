using UnityEngine;

public class Player : MonoBehaviour
{
    public Route currentRoute;

    private int routePos;
    private bool isMoving;

    private int extraLife;
    private bool superSpeed;

    public bool CanMove(int stepsToTake)
    {
        if ((routePos + stepsToTake) < (currentRoute.tileList.Count - 1))
        {
            return true;
        }
        else
        {
            return false;
            // winner
        }
    }

    // IsEdgePos() checks (for moving animation) whether to calculate for X or Z axies
    public bool IsEdgePos(int currNode)
    {
        bool isEdgePos = false;

        while (currNode > 0)
        {
            if (currNode - 12 == 11 || currNode == 11)
            {
                isEdgePos = true;
                break;
            }

            currNode -= 12;
        }

        return isEdgePos;
    }

    public bool IsMoving()
    {
        return this.isMoving;
    }

    public void SetMoving(bool moving)
    {
        this.isMoving = moving;
    }

    public int RoutePos()
    {
        return this.routePos;
    }

    public void SetRoutePos(int newPos)
    {
        this.routePos = newPos;
    } 

    /*public bool IsOnSpecialTile()
    {
        return (this.currentRoute.GetSpecialTiles()[this.routePos].name == "Special Tile");
    }*/

    public void AddExtraLife()
    {
        if (this.extraLife < 3)
        {
            this.extraLife++;
        }
    }

    public void RemoveExtraLife()
    {
        if (this.extraLife < 0)
        {
            this.extraLife--;
        }
    }

    public void GiveSuperSpeed()
    {
        this.superSpeed = true;
    }

    public void RemoveSuperSpeed()
    {
        this.superSpeed = false;
    }
}