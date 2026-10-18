using UnityEngine;

public enum DoorDirection { Right, Left, Up, Down }
public enum DoorType { Open, Locked, ShutUntilCleared, Sealed }

public class Door : MonoBehaviour
{
    public DoorDirection direction;
    public DoorType type = DoorType.Open;
    public RoomTransition room_transition;
    public Collider2D solid_blocker;     // physically blocks when closed
    [Header("Portal (optional)")]
    public bool is_portal; // portal for shield room transition instead of regular door transition
    public Vector2Int destination_room;   //(5,1)
    public Transform player_spawn;        // where Link appears
    public Transform camera_point;        // where the camera should sit in the destination room
    public bool use_fade = false;  

    
    void Start() { Refresh(); }

    public void Refresh()
    {
        bool closed = type != DoorType.Open;
        if (solid_blocker != null) solid_blocker.enabled = closed;
    }
       

    public void SetShut(bool shut)
    {
        if (type == DoorType.Locked || type == DoorType.Sealed) return;
        type = shut ? DoorType.ShutUntilCleared : DoorType.Open;
        Refresh();
    }

    void OnTriggerEnter2D(Collider2D other) { HandleContact(other); }
    void OnTriggerStay2D(Collider2D other)  { HandleContact(other); }

    void HandleContact(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (type == DoorType.Open)
        {
            if (is_portal && player_spawn != null && camera_point != null)
                room_transition.TeleportToRoom(destination_room, player_spawn.position, camera_point.position, use_fade);
            else
                room_transition.TriggerTransition(direction, other.gameObject);
        }
        else if (type == DoorType.Locked)
        {
            Inventory inv = other.GetComponent<Inventory>();
            if (inv != null && inv.GetKeys() > 0)
            {
                inv.UseKey();
                Debug.Log("Door unlocked with a key!");
                Unlock();
            }
            else
            {
                Debug.Log("Door is locked. You need a key.");
            }
        }
    }

    public void Unlock() { type = DoorType.Open; Refresh(); }
}