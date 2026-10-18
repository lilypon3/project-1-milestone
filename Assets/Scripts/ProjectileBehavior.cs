using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float speed = 5.5f;
    public int damage = 1;

    public bool hostile = false;          // true = hurts the player, ignores enemies
    //dont know if i like the color tint or not thats why it's commented out for now
    //public Color hostile_tint = new Color(1f, 0.35f, 0.35f);
    public float lifetime = 4f;

    private Vector2 direction = Vector2.up;
    private Collider2D ignoreCollider;
    private bool consumed;

    void Start()
    {
        if (hostile)
        {
            SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
            //if (sr != null) sr.color = hostile_tint;
        }
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    public void Init(Vector2 travelDirection, Collider2D shooterCollider = null)
    {
        if (travelDirection != Vector2.zero)
        {
            direction = travelDirection.normalized;
            transform.rotation = Quaternion.FromToRotation(Vector3.down, direction);
        }
        ignoreCollider = shooterCollider;
    }

    private void OnTriggerEnter2D(Collider2D other)  { HandleHit(other); }
    private void OnCollisionEnter2D(Collision2D c)   { HandleHit(c.collider); }

    private void HandleHit(Collider2D other)
    {
        if (consumed) return;
        if (ignoreCollider != null && other == ignoreCollider) return;
        if (other.gameObject.layer == LayerMask.NameToLayer("Water")) return;   // arrows fly over water

        Health health = other.GetComponentInParent<Health>();

        if (hostile)
        {
            if (other.CompareTag("Player") || (health != null && health.CompareTag("Player")))
            {

                //uncomment when shield implenented
                // Shield hook (add ShieldBlocker to Link, or remove these 4 lines for now)
                //ShieldBlocker shield = other.GetComponentInParent<ShieldBlocker>();
                //if (shield != null && shield.TryBlock(direction)) { Consume(); return; }

                if (health != null) health.TakeDamage(damage, direction);
                Consume();
            }
            else if (!other.isTrigger && health == null)
            {
                Consume();   // solid wall or block
            }
            return;          // passes through enemies, pickups, and triggers
        }

        // Link's arrows / sword beams: ignore pickups and other non-solid triggers
        if (other.isTrigger && health == null) return;

        if (health != null) health.TakeDamage(damage, direction);
        Consume();
    }

    void Consume()
    {
        consumed = true;
        Destroy(gameObject);
    }
}