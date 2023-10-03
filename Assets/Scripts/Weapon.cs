using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponData _weaponData;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private ParticleSystem _shootEffect;
    public WeaponData GetWeaponData() => _weaponData;

    private float timeBtwShots;
    private float startTimeBtwShots => _weaponData.ReloadDuration;

    private void Update()
    {
        timeBtwShots -= Time.deltaTime;
    }

    public void Shoot()
    {
        if (timeBtwShots <= 0)
        {
            if (BagController.Instance.CheckHasAmmo(_weaponData))
            {
                if (_shootEffect != null)
                {
                    _shootEffect.Play();
                }
                Instantiate(_weaponData.ProjectilePrefab, _shootPoint.position,
                    Quaternion.identity).transform.localScale = new Vector3(PlayerInteraction.Instance.GetPlayerDirection().x, 1, 1);
                timeBtwShots = startTimeBtwShots;

                EventManager.Instance.InvokeWeaponShooted(_weaponData);
            }
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}