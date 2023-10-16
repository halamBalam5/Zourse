using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceReaction = 4;
    [SerializeField] private float _timeBtwDamage = 1.5f;
    [SerializeField] private float _damage = 10;
    [SerializeField] private Transform _transformBody;
    [SerializeField] Image _healthbar;
    [SerializeField] ParticleSystem _deathEffect;
    [SerializeField] List<AmmoData> _ammoData;
    private float currentTimeDamage;
    private float health = 10;
    const float MAX_HEALTH = 10;
    private bool lookRight = true;
    private Animator anim;
    private bool isWalk;
    private bool isStanding;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        currentTimeDamage -= Time.deltaTime;
        if (Vector2.Distance(transform.position, PlayerInteraction.Instance.transform.position) <= _distanceReaction)
        {
            isWalk = true;
            if (isStanding == true)
            {
                return;
            }
            _transformBody.position = Vector2.MoveTowards(_transformBody.position, PlayerInteraction.Instance.transform.position + Vector3.up * 0.3f, _speed * Time.deltaTime);
            anim.SetBool("isWalk", isWalk);
        }
        else
        {
            isWalk = false;
            anim.SetBool("isWalk", isWalk);
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        _healthbar.fillAmount = health / MAX_HEALTH;
        _deathEffect.Play();
        if (health <= 0)
        {
            if (_transformBody != null)
            {
                Instantiate(_ammoData[Random.Range(0, _ammoData.Count)].ammoPrefab, transform.position, Quaternion.identity);
                StopWalking();
                anim.SetTrigger("Death");
            }
        }
    }
    public void DestroyItself()
    {
        Destroy(_transformBody.gameObject);
    }
    private void FixedUpdate()
    {
        if (transform.position.x > PlayerInteraction.Instance.transform.position.x && lookRight)
        {
            Flip();
        }
        else if (transform.position.x < PlayerInteraction.Instance.transform.position.x && !lookRight)
        {
            Flip();
        }
    }
    private void Flip()
    {
        lookRight = !lookRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    private void StopWalking()
    {
        isStanding = true;
    }
    private void StartWalking()
    {
        isStanding = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (currentTimeDamage <= 0)
            {
                anim.SetTrigger("Damage");
                PlayerInteraction.Instance.TakeDamage(_damage);
                currentTimeDamage = _timeBtwDamage;
            }
        }
    }
}