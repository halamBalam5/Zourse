using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField]
    public AmmoData _ammoData;

    public AmmoData GetAmmoData() => _ammoData;

    public void DestroySelf() => Destroy(gameObject);
}
