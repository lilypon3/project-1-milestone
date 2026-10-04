using UnityEngine;

public class BoomerangBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 5.5f;
    private Vector3 direction = Vector3.up;
    public int damage = 1;
    [HideInInspector] public Transform player;
    private Collider2D ignoreCollider;
    private bool comeBack = false;
    


    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        if (Vector3.Distance(transform.position, player.position) <= 5f && comeBack == false)
        {
            //move away
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
        //transform.position += (Vector3)direction * speed * Time.deltaTime;
        if (Vector3.Distance(player.position, transform.position) > 5f)
        {
            comeBack = true;
        }
        if (comeBack == true)
        {
            // If the boomerang is too far from the player, make it return to the player
            transform.position += (player.position - transform.position).normalized * speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, player.position) < 0.5f)
            {
                Destroy(gameObject);
                comeBack = false;
            }
        }
    }

    public void Init(Vector3 travelDirection, Collider2D shooterCollider = null)
    {
        if (travelDirection != Vector3.zero)
        {
            direction = travelDirection.normalized;
            transform.rotation = Quaternion.FromToRotation(Vector3.down, direction);
        }

        ignoreCollider = shooterCollider;
        if (shooterCollider != null) player = shooterCollider.transform;
        Collider2D myCollider = GetComponent<Collider2D>();
        if (ignoreCollider != null && myCollider != null)
        {
            Physics2D.IgnoreCollision(myCollider, ignoreCollider);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
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

        Destroy(gameObject);
    }
}
