using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _iconSprite;
    [SerializeField] private GameObject _weaponPrefab;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private int _count;
    [SerializeField] private float _reloadDuration;

    public string Name => _name;
    public Sprite IconSprite => _iconSprite;
    public GameObject WeaponPrefab => _weaponPrefab;
    public GameObject ProjectilePrefab => _projectilePrefab;
    public int Count => _count;
    public float ReloadDuration => _reloadDuration;
}