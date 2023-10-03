using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagController : MonoBehaviour
{
    [SerializeField] private GameObject bagItemPrefab;

    private List<BagItemController> bagItems = new List<BagItemController>();

    public static BagController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        EventManager.Instance.onWeaponAdded += OnWeaponAdded;
        EventManager.Instance.onWeaponRemoved += OnWeaponRemoved;
        EventManager.Instance.onAmmoTaked += OnAmmoTaked;

        Hide();
    }

    private void OnDestroy()
    {
        EventManager.Instance.onWeaponAdded -= OnWeaponAdded;
        EventManager.Instance.onWeaponRemoved -= OnWeaponRemoved;
        EventManager.Instance.onAmmoTaked -= OnAmmoTaked;
    }

    public int GetItemsCount() => bagItems.Count;

    public BagItemController GetBagItem(WeaponData weaponData)
    {
        return bagItems.Find(bagItem => bagItem.GetWeaponData() == weaponData);
    }

    public bool CheckHasAmmo(WeaponData weaponData)
    {
        BagItemController bagItem = GetBagItem(weaponData);

        if (bagItem == null) return false;

        return bagItem.Count > 0;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void OnWeaponAdded(WeaponData weaponData)
    {
        BagItemController bagItem = GetBagItem(weaponData);
        if (bagItem == null)
        {
            GameObject bagItemObject = Instantiate(bagItemPrefab, transform);
            bagItem = bagItemObject.GetComponent<BagItemController>();
            bagItems.Add(bagItem);

            bagItem.SetWeaponData(weaponData);
        }
        else
        {
            bagItem.Increase(weaponData.Count);
        }
    }

    void OnWeaponRemoved(WeaponData weaponData)
    {
        BagItemController bagItem = GetBagItem(weaponData);
        if (bagItem != null)
        {
            bagItem.DestroySelf();
            bagItems.Remove(bagItem);
        }
        if (bagItems.Count == 0)
        {
            Hide();
        }
    }

    void OnAmmoTaked(AmmoData ammoData)
    {
        BagItemController bagItem = GetBagItem(ammoData.Weapon);
        bagItem?.Increase(ammoData.Count);
    }
}
