using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{

    public float speed = 5.5f;
    private Vector2 direction = Vector2.up;
    public int damage = 1;
    private Rigidbody2D rb;
    private Collider2D ignoreCollider;


    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
        //checkPosition();
    }


    public void Init(Vector2 travelDirection, Collider2D shooterCollider = null)
    {
        if (travelDirection != Vector2.zero)
        {
            direction = travelDirection.normalized;
            transform.rotation = Quaternion.FromToRotation(Vector3.down, direction);
        }

        ignoreCollider = shooterCollider;
        Collider2D myCollider = GetComponent<Collider2D>();
        // if (ignoreCollider != null && myCollider != null)
        // {
        //     Physics2D.IgnoreCollision(myCollider, ignoreCollider);
        //     // Debug.Log("Collisions being ignored: " + myCollider + " ---- " + ignoreCollider);
        // }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleHit(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleHit(collision.collider);
    }

    private void HandleHit(Collider2D collision)
    {
        if (ignoreCollider != null && collision == ignoreCollider)
        {
            return;
        }
        Health health = collision.GetComponentInParent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage, direction);
        }
        
        Debug.Log("Projectile collided with: " + collision);
        Destroy(gameObject);
    }
    
}
