using UnityEngine;
using UnityEngine.InputSystem;

public class CheatManager : MonoBehaviour
{
    public static bool god_mode = false;

    static int last_frame = -1;

    public Inventory inventory; 
    public Health health;

    public WeaponController weaponController;

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
        }
    }

    void ApplyGodModeBenefits()
    {
        if (inventory != null)
        {
            inventory.AddRupee(999);
            inventory.AddKey(999);
            health.addHealth(999);
            // AddBomb once bombs exist
        }
        
        if (weaponController != null)
        {
            IWeapon alt = weaponController.GetCurrentAltWeapon();
            if (alt != null) alt.IsUnlocked = true; // unlocks bow for p1_milestone
        }
    }
}