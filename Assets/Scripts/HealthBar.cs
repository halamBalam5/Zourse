using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform _follow;
    [SerializeField] private float _speed = 1.5f;
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _follow.position, _speed * Time.deltaTime);
    }
}