using UnityEngine;

public class KeeseSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // Drag your prefab here in the Inspector
    [SerializeField] private int numberToSpawn = 5;  // How many enemies you want
    [SerializeField] private Vector3 spawnOffset = new Vector3(0f, 1f, 0f); // Distance between each spawn

    void Start()
    {
        SpawnMultipleEnemies();
    }

    void SpawnMultipleEnemies()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            // Calculate a unique position for each spawned enemy
            Vector3 spawnPosition = transform.position + spawnOffset;

            // Instantiate (spawn) the prefab into the scene
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}