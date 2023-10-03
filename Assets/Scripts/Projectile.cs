using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _lifetime = 3;
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _mask;
    private void Update()
    {
        transform.Translate(Vector2.right * transform.localScale.x * _speed * Time.deltaTime);

        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, transform.right * transform.localScale.x, _distance, _mask);
        if (raycastHit.collider != null)
        {
            if (raycastHit.collider.CompareTag("Enemy"))
            {
                raycastHit.collider.GetComponent<Enemy>().TakeDamage(_damage);
            }
            Destroy(gameObject);
        }


        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}