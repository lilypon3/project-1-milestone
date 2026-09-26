using UnityEngine;
using UnityEngine.InputSystem;

public class CheatManager : MonoBehaviour
{
    public static bool god_mode = false;

    public Inventory inventory; 
    public Health health;

    public WeaponController weaponController;

    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
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
            // AddBomb once bombs exist
        }
        
        if (weaponController != null)
        {
            IWeapon alt = weaponController.GetCurrentAltWeapon();
            if (alt != null) alt.IsUnlocked = true; // unlocks bow for p1_milestone
        }
    }
}