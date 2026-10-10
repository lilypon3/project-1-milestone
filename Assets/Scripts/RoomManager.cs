using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    Dictionary<Vector2Int, RoomController> rooms = new Dictionary<Vector2Int, RoomController>();
    public Vector2Int starting_room;

    void Awake()
    {
        foreach (var r in GetComponentsInChildren<RoomController>(true))
            rooms[r.grid_coord] = r;
        Debug.Log("[RoomManager] Registered rooms: " + string.Join(", ", rooms.Keys));
    }

    void Start()
    {
        Enter(starting_room);
    }

    public void Enter(Vector2Int c) { 
        Debug.Log("[RoomManager] Enter " + c + " found=" + rooms.ContainsKey(c));
        if (rooms.TryGetValue(c, out var r)) r.Enter(); 
        }
    public void Exit(Vector2Int c)  { if (rooms.TryGetValue(c, out var r)) r.Exit(); }
}