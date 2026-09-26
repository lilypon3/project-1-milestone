using UnityEngine;

public class LaunchOffset : MonoBehaviour
{

    private GridMovement gridMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gridMovement = GetComponentInParent<GridMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        checkPosition();
    }

    private void checkPosition()
    {
        if (gridMovement.LastMoveDir == Vector2.right) // right
        {
            transform.localPosition = new Vector3(1.08f, 0, 0);
        }
        else if (gridMovement.LastMoveDir == Vector2.left) // left
        {
            transform.localPosition = new Vector3(-1.08f, 0, 0);
        }
        else if (gridMovement.LastMoveDir == Vector2.up) // up
        {
            transform.localPosition = new Vector3(0, 1.08f, 0);
        }
        else if (gridMovement.LastMoveDir == Vector2.down) // down
        {
            transform.localPosition = new Vector3(0, -1.08f, 0);
        }
    }
}
