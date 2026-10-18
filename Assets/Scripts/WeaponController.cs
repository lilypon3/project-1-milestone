using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    public MonoBehaviour standard_weapon;      // SwordAttack
    public List<MonoBehaviour> alt_weapons;    // BowAttack and future alt weapons

    int current_alt_index = 0;

    void Update()
    {
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            (standard_weapon as IWeapon)?.Attack();
        }

        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            AttackCurrentAlt();
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            CycleAltWeapon();
            Debug.Log(alt_weapons[current_alt_index] + "equipped");
        }
    }

    void AttackCurrentAlt()
    {
        if (alt_weapons.Count == 0) return;

        IWeapon weapon = alt_weapons[current_alt_index] as IWeapon;
        if (weapon != null && weapon.IsUnlocked)
        {
            weapon.Attack();
        }
    }

    void CycleAltWeapon()
    {
        if (alt_weapons.Count <= 1) return;

        int start_index = current_alt_index;
        do
        {
            current_alt_index = (current_alt_index + 1) % alt_weapons.Count;
            IWeapon weapon = alt_weapons[current_alt_index] as IWeapon;
            if (weapon != null && weapon.IsUnlocked) break;
        }
        while (current_alt_index != start_index); // stop if loops through everything with nothing unlocked
    }

    public IWeapon GetCurrentAltWeapon()
    {
        if (alt_weapons.Count == 0) return null;
        return alt_weapons[current_alt_index] as IWeapon;
    }
}