using UnityEngine;

public enum DoorDirection { Right, Left, Up, Down }
public enum DoorType { Open, Locked, ShutUntilCleared, Sealed }

public class Door : MonoBehaviour
{
    public DoorDirection direction;
    public DoorType type = DoorType.Open;
    public RoomTransition room_transition;
    public Collider2D solid_blocker;     // physically blocks when closed

    
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (type == DoorType.Open)
        {
            room_transition.TriggerTransition(direction, other.gameObject);
        }
        else if (type == DoorType.Locked)
        {
            Inventory inv = other.GetComponent<Inventory>();
            if (inv != null && inv.GetKeys() > 0)
            {
                inv.UseKey();
                Unlock();
                Debug.Log("Door unlocked with a key!");
               
            }
            Debug.Log("Door is locked. You need a key to open it.");
        }
    }

    public void Unlock() { type = DoorType.Open; Refresh(); }
}