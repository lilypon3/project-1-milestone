using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 1;
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

    private void OnTriggerEnter2D(Collider2D colldier)
    {
        if (colldier.GetComponent<Health>() != null)
        {
            Health health = colldier.GetComponent<Health>();
            health.TakeDamage(damage, gridMovement.LastMoveDir);
        }
    }

    private void checkPosition()
    {
        if (gridMovement.LastMoveDir == Vector2.right) // right
        {
            transform.localPosition = new Vector3(0.851f, 0, 0);
        }
        else if (gridMovement.LastMoveDir == Vector2.left) // left
        {
            transform.localPosition = new Vector3(-0.851f, 0, 0);
        }
        else if (gridMovement.LastMoveDir == Vector2.up) // up
        {
            transform.localPosition = new Vector3(0, 0.851f, 0);
        }
        else if (gridMovement.LastMoveDir == Vector2.down) // down
        {
            transform.localPosition = new Vector3(0, -0.851f, 0);
        }
    }
}
