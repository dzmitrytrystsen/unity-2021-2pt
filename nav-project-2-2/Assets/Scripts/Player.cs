using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private Camera _camera;
    private NavMeshAgent _playerAgent;

    private void Start()
    {
        _camera = Camera.main;

        _playerAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _playerAgent.SetDestination(hit.point);
            }
        }
    }
}
