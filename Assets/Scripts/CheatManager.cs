using UnityEngine;
using UnityEngine.InputSystem;

public class CheatManager : MonoBehaviour
{
    public static bool god_mode = false;

    static int last_frame = -1;

    public Inventory inventory; 
    public Health health;
    public Collector collector;

    public WeaponController weaponController;

    public RoomTransition roomTransition;
    public Vector2Int shieldRoomStart = new Vector2Int(5, 1);
    public Transform ShieldRoomPlayerSpawn;
    public Transform ShieldRoomCameraStart;

    public Vector2Int[] customLevelRooms = {
    new Vector2Int(5, 1),
    new Vector2Int(5, 2),
    new Vector2Int(6, 2)};

    int saved_rupees;
    int saved_keys;
    int saved_bombs;

    void Awake() { god_mode = false; }

    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            if (last_frame == Time.frameCount) return; // prevent double toggle in one frame
            last_frame = Time.frameCount;

            god_mode = !god_mode;
            Debug.Log("God mode: " + god_mode);

            if (god_mode)
            {
                ApplyGodModeBenefits();
            }
            else
            {
                if (inventory != null && collector != null)
                {
                    inventory.RestoreInventory(
                        saved_rupees,
                        saved_keys,
                        saved_bombs,
                        collector.rupeesCollectedDuringGodMode,
                        collector.keysCollectedDuringGodMode,
                        collector.bombsCollectedDuringGodMode
                    );

                    collector.rupeesCollectedDuringGodMode = 0;
                    collector.keysCollectedDuringGodMode = 0;
                    collector.bombsCollectedDuringGodMode = 0;
                }

                if (health != null)
                {
                    health.SetHealth(health.max_health);
                }

                Debug.Log("God mode disabled. Inventory and health restored.");
            }
        }

        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            if (last_frame == Time.frameCount) return; // prevent double toggle in one frame
            last_frame = Time.frameCount;

            StartShieldRoom();

        }
    }

    void ApplyGodModeBenefits()
    {
        if (inventory != null)
        {
            // save the player's actual inventory before adding cheats
            saved_rupees = inventory.GetRupees();
            saved_keys = inventory.GetKeys();
            saved_bombs = inventory.GetBombs();

            // give the player cheat benefits
            inventory.AddRupee(999);
            inventory.AddKey(999);
            inventory.AddBomb(999);
        }

        if (health != null)
        {
            health.addHealth(999);
        }
    }

    void StartShieldRoom()
    {
        if (roomTransition == null || ShieldRoomPlayerSpawn == null || ShieldRoomCameraStart == null)
        {
            Debug.LogError("CheatManager: RoomTransition or ShieldRoom spawn points are not set.");
            return;
        }

        if (InShieldLevel())
        {
            Debug.Log("Already in the shield level.");
            return;
        }

        roomTransition.TeleportToRoom(shieldRoomStart, ShieldRoomPlayerSpawn.position, ShieldRoomCameraStart.position);
        Debug.Log("Teleported to Shield Room.");
    }

    bool InShieldLevel()
    {
        foreach (var r in customLevelRooms)
            if (roomTransition.current_room == r) return true;
        return false;
    }
}