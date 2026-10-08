using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    public int damage = 1;

    //don't know which collider to use since stalfos has both attached (julia help)
    void OnTriggerEnter2D(Collider2D collider)
    {
        TryDamage(collider);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        TryDamage(collision.collider);
    }

    void TryDamage(Collider2D other)
    { 

        Health health = other.GetComponent<Health>();
        if (health == null) health = other.GetComponentInParent<Health>();

        if (health != null)
        {
            Vector2 knockback_direction = (other.transform.position - transform.position).normalized;
            health.TakeDamage(damage, knockback_direction);
        }
    }
}
