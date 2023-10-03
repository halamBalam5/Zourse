using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _emenies;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private int _countEnemy;
    void Start()
    {
        for (int i = 0; i < _countEnemy; i++)
        {
            int spawnPointIndex = Random.Range(0, _spawnPoints.Count);
            Instantiate(_emenies[Random.Range(0, _emenies.Count)], _spawnPoints[spawnPointIndex].position, Quaternion.identity);
            _spawnPoints.RemoveAt(spawnPointIndex);
        }
    }
}