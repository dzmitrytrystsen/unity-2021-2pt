using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector3 _endPosition;
    private Vector3 _startPosition;

    private NavMeshAgent _enemyAgent;
    private bool _isEndReached;

    private void Start()
    {
        _enemyAgent = GetComponent<NavMeshAgent>();

        _startPosition = transform.position;
    }

    private void Update()
    {
        if (!_isEndReached)
        {
            _enemyAgent.SetDestination(_endPosition);

            if (Vector3.Distance(transform.position, _endPosition) < 0.5f)
                _isEndReached = true;
        }
        else
        {
            _enemyAgent.SetDestination(_startPosition);

            if (Vector3.Distance(transform.position, _startPosition) < 0.5f)
                _isEndReached = false;
        }
    }
}
