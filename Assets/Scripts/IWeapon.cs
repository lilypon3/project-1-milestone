using UnityEngine;
public interface IWeapon
{
    public void Attack();
    public bool IsUnlocked { get; set; }
}