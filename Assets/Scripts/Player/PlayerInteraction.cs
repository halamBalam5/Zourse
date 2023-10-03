using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private WeaponData _weaponData;
    [SerializeField] private Transform _gunPoint;
    [SerializeField] private int _inventorySize = 6;
    [SerializeField] private List<WeaponData> inventory;
    [SerializeField] private Image _healthbar;
    [SerializeField] private Transform playerVisual;

    private float health = 100;
    private Weapon armedWeapon;
    public static PlayerInteraction Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EventManager.Instance.onWeaponSelected += OnWeaponSelected;
        EventManager.Instance.onWeaponRemoved += OnWeaponRemoved;
    }

    private void OnDestroy()
    {
        EventManager.Instance.onWeaponSelected -= OnWeaponSelected;
        EventManager.Instance.onWeaponRemoved -= OnWeaponRemoved;
    }

    public Vector3 GetPlayerDirection()
    {
        return playerVisual.localScale;
    }

    public void OnShootButtonDown()
    {
        if (armedWeapon == null)
        {
            return;
        }
        armedWeapon.Shoot();
    }

    private void AddWeapon(WeaponData weaponData)
    {
        EventManager.Instance.InvokeWeaponAdded(weaponData);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            Weapon _currentWeapon = collision.gameObject.GetComponent<Weapon>();
            AddWeapon(_currentWeapon.GetWeaponData());
            _currentWeapon.DestroySelf();
        }

        else if (collision.CompareTag("Ammo"))
        {
            Ammo ammo = collision.gameObject.GetComponent<Ammo>();
            if (ammo != null)
            {
                EventManager.Instance.InvokeAmmoTaked(ammo.GetAmmoData());
                ammo.DestroySelf();
            }
        }
    }

    private void OnWeaponSelected(WeaponData weaponData)
    {
        if (armedWeapon != null)
        {
            armedWeapon.DestroySelf();
        }

        armedWeapon = Instantiate(weaponData.WeaponPrefab, _gunPoint).GetComponent<Weapon>();
        ResetArmedWeaponTransform(armedWeapon);
        BagController.Instance.Hide();
    }

    private void OnWeaponRemoved(WeaponData weaponData)
    {
        if (armedWeapon?.GetWeaponData() == weaponData)
        {
            armedWeapon.DestroySelf();
            armedWeapon = null;
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        _healthbar.fillAmount = health / 100f;
        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
    private void ResetArmedWeaponTransform(Weapon armed)
    {
        armed.transform.localPosition = Vector3.zero;
        armed.transform.localRotation = Quaternion.identity;
        armed.transform.localScale = Vector3.one;
    }
}