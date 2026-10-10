using UnityEngine;

public class Ladder : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        GridMovement g = other.GetComponentInParent<GridMovement>();
        if (g != null) g.EnterLadder();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        GridMovement g = other.GetComponentInParent<GridMovement>();
        if (g != null) g.ExitLadder();
    }
}