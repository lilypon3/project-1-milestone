using UnityEngine;
using System.Collections;

public class FireballAttack : MonoBehaviour
{
    [SerializeField] private float attackInterval = 5f;
    [SerializeField] private float targetSpread = 5.5f;
    [SerializeField] private float spawnSpacing = 0.5f;

    private GridMovement gridMovement;
    private AquamentusMovement aquamentusMovement;
    public FireballBehavior fireballPrefab;
    public Transform launchOffset;

    void Start()
    {
        gridMovement = GetComponent<GridMovement>();
        aquamentusMovement = GetComponent<AquamentusMovement>();

        if (gridMovement == null && aquamentusMovement != null)
        {
            StartCoroutine(AttackCoroutine());
        }
        else if (gridMovement == null)
        {
            Debug.LogError("FireballAttack requires GridMovement or AquamentusMovement.", this);
            enabled = false;
        }
    }

    public void Attack()
    {
        if (fireballPrefab == null)
        {
            Debug.LogError("FireballAttack requires a FireballBehavior prefab.", this);
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("FireballAttack could not find an active object tagged Player.", this);
            return;
        }

        Transform spawnPoint = launchOffset != null ? launchOffset : transform;
        Vector2 center = spawnPoint.position;
        Vector2 playerPosition = player.transform.position;
        Vector2 targetOffset = Vector2.up * targetSpread;
        Vector2 spawnOffset = Vector2.up * spawnSpacing;
        Collider2D shooterCollider = GetComponent<Collider2D>();

        SpawnFireball(center, playerPosition, shooterCollider);
        SpawnFireball(center + spawnOffset, playerPosition + targetOffset, shooterCollider);
        SpawnFireball(center - spawnOffset, playerPosition - targetOffset, shooterCollider);
    }

    private void SpawnFireball(Vector2 spawnPosition, Vector2 targetPosition, Collider2D shooterCollider)
    {
        FireballBehavior fireball = Instantiate(fireballPrefab, spawnPosition, transform.rotation);
        fireball.Init(targetPosition - spawnPosition, shooterCollider);
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);
            Attack();
        }
    }
}
