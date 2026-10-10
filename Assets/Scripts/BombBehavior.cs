using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BombBehavior : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 2.5f;
    [SerializeField] private int damage = 3;

    void Start()
    {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(1.5f);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        HashSet<Health> damagedHealth = new HashSet<Health>();
        foreach (Collider2D hit in hits)
        {
            Health health = hit.GetComponentInParent<Health>();
            if (health == null || health.CompareTag("Player") || !damagedHealth.Add(health))
            {
                continue;
            }

            health.TakeDamage(damage, Vector2.zero);
            Debug.Log("Bomb hit " + health.name + ".");
        }

        Destroy(gameObject);
    }
}
