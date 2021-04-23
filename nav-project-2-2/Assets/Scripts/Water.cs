using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Water : MonoBehaviour
{
    private NavMeshAgent _playerAgent;
    private float _playerStartSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            _playerAgent = other.gameObject.GetComponent<NavMeshAgent>();
            _playerStartSpeed = _playerAgent.speed;
            _playerAgent.speed = 1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
            _playerAgent.speed = _playerStartSpeed;
    }
}