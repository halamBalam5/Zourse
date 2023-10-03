using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BagItemController : MonoBehaviour
{
    private WeaponData weaponData;

    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _countText;

    public WeaponData GetWeaponData() => weaponData;

    public int Count { get; private set; } = 0;

    private void Start()
    {
        EventManager.Instance.onWeaponShooted += OnWeaponShooted;
    }

    private void OnDestroy()
    {
        EventManager.Instance.onWeaponShooted -= OnWeaponShooted;
    }

    public void SetWeaponData(WeaponData weaponData)
    {
        this.weaponData = weaponData;

        _iconImage.sprite = weaponData.IconSprite;

        Count = weaponData.Count;

        UpdateCountText();
    }

    public void Increase(int count)
    {
        Count += count;
        _countText.text = Count.ToString();
        UpdateCountText();
    }

    public void Decrease(int count = 1)
    {
        Count -= count;
        if (Count < 0)
        {
            Count = 0;
        }
        UpdateCountText();
    }

    private void OnWeaponShooted(WeaponData weaponData)
    {
        if (weaponData == this.weaponData)
        {
            Decrease();
        }
    }

    private void UpdateCountText()
    {
        _countText.gameObject.SetActive(Count > 1);
        _countText.text = Count.ToString();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void OnItemClicked()
    {
        EventManager.Instance.InvokeWeaponSelected(weaponData);
    }

    public void OnItemRemoveClicked()
    {
        EventManager.Instance.InvokeWeaponRemoved(weaponData);
        Destroy(gameObject);
    }
}
