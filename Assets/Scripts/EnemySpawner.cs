using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int numberToSpawn = 5;
    [SerializeField] private Vector2 areaSize = new Vector2(10f, 6f); // spawn region centered on this object

    // No Start(): the RoomController calls this when Link enters the room
    void Awake()
    {
        Debug.Log($"[Spawner] {name} Awake. numberToSpawn={numberToSpawn}, prefab={(enemyPrefab ? enemyPrefab.name : "NONE")}");
    }

    public List<GameObject> Spawn(Transform parent)
    {
        Debug.Log($"[Spawner] Spawn() called on {name}, spawning {numberToSpawn}");
        List<GameObject> spawned = new List<GameObject>();
        for (int i = 0; i < numberToSpawn; i++)
        {
            Vector3 offset = new Vector3(
                Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
                Random.Range(-areaSize.y / 2f, areaSize.y / 2f), 0f);
            GameObject e = Instantiate(enemyPrefab, transform.position + offset, Quaternion.identity, parent);
            Debug.Log($"[Spawner] #{i} spawned at {e.transform.position}");
            spawned.Add(e);
        }
        return spawned;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(areaSize.x, areaSize.y, 0f));
    }
}