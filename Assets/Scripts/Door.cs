using UnityEngine;

public enum DoorDirection { Right, Left, Up, Down }
public enum DoorType { Open, Locked, ShutUntilCleared, Sealed }

[System.Serializable]
public class DoorTileSprites
{
    public Sprite open;
    public Sprite locked;
    public Sprite shut;
    public Sprite sealed_door;   // optional, falls back to shut
}

public class Door : MonoBehaviour
{
    public DoorDirection direction;
    public DoorType type = DoorType.Open;
    public RoomTransition room_transition;
    public Collider2D solid_blocker;     // physically blocks when closed

    [Header("Visuals (one entry per tile of the doorway)")]
    public SpriteRenderer[] door_renderers;   // 2 renderers for a 2-tile door
    public DoorTileSprites[] tile_sprites;    // same order and length as door_renderers
    public AudioClip open_sound;
    public AudioClip close_sound;

    [Header("Portal (optional)")]
    public bool is_portal;                // teleports instead of panning to the adjacent room
    public Vector2Int destination_room;   //(5,1)
    public Transform player_spawn;        // where Link appears
    public Transform camera_point;        // where the camera sits in the destination room
    public bool use_fade = false;


    DoorType last_type;
    bool visuals_initialized;

    void Start() { Refresh(); }

    public void Refresh()
    {
        bool closed = type != DoorType.Open;
        if (solid_blocker != null) solid_blocker.enabled = closed;

        UpdateSprites();

        // play a sound only on a real change, not on the first Refresh at Start()
        if (visuals_initialized && last_type != type)
        {
            AudioClip clip = closed ? close_sound : open_sound;
            if (clip != null && Camera.main != null)
                AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
        last_type = type;
        visuals_initialized = true;
    }

    void UpdateSprites()
    {
        if (door_renderers == null || tile_sprites == null) return;

        for (int i = 0; i < door_renderers.Length && i < tile_sprites.Length; i++)
        {
            if (door_renderers[i] == null || tile_sprites[i] == null) continue;

            Sprite s = null;
            switch (type)
            {
                case DoorType.Open:
                    s = tile_sprites[i].open;
                    break;
                case DoorType.Locked:
                    s = tile_sprites[i].locked;
                    break;
                case DoorType.ShutUntilCleared:
                    s = tile_sprites[i].shut;
                    break;
                case DoorType.Sealed:
                    s = tile_sprites[i].sealed_door != null ? tile_sprites[i].sealed_door : tile_sprites[i].shut;
                    break;
            }
            if (s != null) door_renderers[i].sprite = s;
        }
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
        if (room_transition == null) return;

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

    public void Unlock()
    {
        type = DoorType.Open;
        Refresh();
    }
       
}