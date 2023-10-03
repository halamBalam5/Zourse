using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ammo", menuName = "ScriptableObjects/Ammo Data")]
public class AmmoData : ScriptableObject
{
    [SerializeField] private GameObject _ammoPrefab;
    [SerializeField] private WeaponData _weapon;
    [SerializeField] private int _count;

    public GameObject ammoPrefab => _ammoPrefab;
    public WeaponData Weapon => _weapon;
    public int Count => _count;
}
