using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public event Action<WeaponData> onWeaponAdded;
    public event Action<WeaponData> onWeaponRemoved;
    public event Action<WeaponData> onWeaponSelected;

    public event Action<AmmoData> onAmmoTaked;

    public event Action<WeaponData> onWeaponShooted;

    public void InvokeWeaponAdded(WeaponData weapon)
    {
        onWeaponAdded?.Invoke(weapon);
    }

    public void InvokeWeaponRemoved(WeaponData weapon)
    {
        onWeaponRemoved?.Invoke(weapon);
    }

    public void InvokeWeaponSelected(WeaponData weapon)
    {
        onWeaponSelected?.Invoke(weapon);
    }

    public void InvokeWeaponShooted(WeaponData weapon)
    {
        onWeaponShooted?.Invoke(weapon);
    }

    public void InvokeAmmoTaked(AmmoData ammoData)
    {
        onAmmoTaked?.Invoke(ammoData);
    }
}
