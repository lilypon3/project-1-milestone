using UnityEngine;

public class DropTable : MonoBehaviour
{
    public GameObject rupee_prefab;
    public GameObject heart_prefab;
     public GameObject bomb_prefab;

    [Range(0, 1)] public float drop_chance = 0.5f; // chance ANYTHING drops

    public Health health;

    void Start()
    {
        health = GetComponent<Health>();
        if (health != null)
        {
            health.onDeath += HandleDeath;
        }
    }

    void HandleDeath()
    {
        Debug.Log("Enemy has died!");
        SpawnDrop();
        Destroy(gameObject); // destroy the enemy after it dies
        
    }

    void SpawnDrop()
    {
        if (Random.value > drop_chance) return; // nothing drops this time

        int roll = Random.Range(0, 3); // 0, 1, or 2 - weight these however you like
        GameObject drop = null;

        switch (roll)
        {
            case 0: drop = rupee_prefab; break;
            case 1: drop = heart_prefab; break;
             case 2: drop = bomb_prefab; break;
        }

        if (drop != null)
        {
            Instantiate(drop, transform.position, Quaternion.identity);
        }
    }
}