using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [System.Serializable]
    public class EnemySpawn { public GameObject prefab; public Transform point; }

    public Vector2Int grid_coord;
    public List<EnemySpawn> enemy_spawns;
    public List<EnemySpawner> spawners;      
    public List<Door> doors;
    public bool shut_doors_until_cleared;
    public GameObject clear_reward_prefab;
    public Transform reward_point;
    public bool IsCleared => cleared;
    public System.Action onCleared;

    List<GameObject> enemies = new List<GameObject>();
    int alive;
    bool spawned, cleared;

    public void Enter()
    {

        Debug.Log($"[Room {grid_coord}] Enter. spawned={spawned}, spawners={spawners.Count}, enemy_spawns={enemy_spawns.Count}");   
        if (!spawned)
        {
            spawned = true;

            foreach (var s in enemy_spawns)
                Register(Instantiate(s.prefab, s.point.position, Quaternion.identity, transform));

            foreach (var sp in spawners)
                foreach (var e in sp.Spawn(transform))
                    Register(e);
        }
        else
        {
            foreach (var e in enemies) if (e != null) e.SetActive(true);
        }

        if (alive > 0 && shut_doors_until_cleared) SetDoorsShut(true);
    }

    void Register(GameObject e)
    {
        Health h = e.GetComponent<Health>();
        if (h == null)
        {
            Debug.LogWarning(e.name + " has no Health, so the room can't count it.");
            return;
        }
        enemies.Add(e);
        alive++;
        h.onDeath += OnEnemyDied;
    }

    public void Exit()
    {
        foreach (var e in enemies) if (e != null) e.SetActive(false);  // frozen until return
    }

    void OnEnemyDied()
    {
        alive--;
        if (alive > 0 || cleared) return;
        cleared = true;
        SetDoorsShut(false);
        if (clear_reward_prefab != null)
        {
            Instantiate(clear_reward_prefab, reward_point.position, Quaternion.identity, transform);
        }
        onCleared?.Invoke();
    }

    void SetDoorsShut(bool shut) { foreach (var d in doors) d.SetShut(shut); }
}