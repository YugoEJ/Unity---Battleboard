using UnityEngine;

public class Player : MonoBehaviour
{
    public Route currentRoute;

    private int routePos;
    private bool isMoving;

    private int extraLife;
    private bool superSpeed;
    private bool wonMinigame;
    private bool skipTurn;

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

    public int GetExtraLife()
    {
        return this.extraLife;
    }

    public void AddExtraLife()
    {
        if (this.extraLife < 3)
        {
            this.extraLife++;
        }
    }

    public void RemoveExtraLife()
    {
        if (this.extraLife > 0)
        {
            this.extraLife--;
        }
    }

    public string GetSuperSpeed()
    {
        if (this.superSpeed)
        {
            return "On";
        }
        else
        {
            return "Off";
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

    public bool IsMinigameWinner()
    {
        return this.wonMinigame;
    }

    public void SetMinigameWinner()
    {
        this.wonMinigame = true;
    }

    public void RemoveMinigameWinner()
    {
        this.wonMinigame = false;
    }

    public bool IsSkippingTurn()
    {
        return this.skipTurn;
    }

    public void SetSkipTurn()
    {
        this.skipTurn = true;
    }

    public void RemoveSkipTurn()
    {
        this.skipTurn = false;
    }
}