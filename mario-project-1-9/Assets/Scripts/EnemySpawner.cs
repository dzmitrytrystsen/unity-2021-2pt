using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject _enemyPrefab;

    private Transform[] _spawnPoints;

    private void Start()
    {
        _spawnPoints = GetComponentsInChildren<Transform>();

        for (int i = 1; i < _spawnPoints.Length; i++)
        {
            Instantiate(_enemyPrefab, _spawnPoints[i].position, Quaternion.identity);
        }
    }
}
