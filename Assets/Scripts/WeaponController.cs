using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    public MonoBehaviour standard_weapon;      // SwordAttack
    public List<MonoBehaviour> alt_weapons;    // Bow, Bomb, Boomerang, Shield...

    int current_alt_index = 0;

    // usable if truly unlocked (picked up) OR god mode is on
    static bool Available(IWeapon w)
    {
        return w != null && (w.IsUnlocked || CheatManager.god_mode);
    }

    void Update()
    {
        EnsureValidSelection();

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
            IWeapon current = GetCurrentAltWeapon();
            if (Available(current)) Debug.Log(alt_weapons[current_alt_index] + " equipped");
        }
    }

    // if god mode turns off while a cheat-only weapon is equipped, move off it
    void EnsureValidSelection()
    {
        if (alt_weapons.Count <= 1) return;
        if (!Available(GetCurrentAltWeapon())) CycleAltWeapon();
    }

    void AttackCurrentAlt()
    {
        IWeapon weapon = GetCurrentAltWeapon();
        if (Available(weapon)) weapon.Attack();
    }

    void CycleAltWeapon()
    {
        if (alt_weapons.Count <= 1) return;

        GetComponent<Health>()?.SetShielding(false);   // never stay invulnerable after switching

        int start_index = current_alt_index;
        do
        {
            current_alt_index = (current_alt_index + 1) % alt_weapons.Count;
            if (Available(alt_weapons[current_alt_index] as IWeapon)) break;
        }
        while (current_alt_index != start_index);
    }

    public IWeapon GetCurrentAltWeapon()
    {
        if (alt_weapons.Count == 0) return null;
        return alt_weapons[current_alt_index] as IWeapon;
    }
}