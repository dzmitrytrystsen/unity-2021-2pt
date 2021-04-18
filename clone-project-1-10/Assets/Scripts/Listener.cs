using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{
    [SerializeField] private Unit _player;

    private void Start()
    {
        _player.OnHealthChanged += DebugHealth;
        _player.Attack(30);
        _player.OnHealthChanged -= DebugHealth;
        Destroy(gameObject);
    }

    private void DebugHealth(int health)
    {
        Debug.Log("Current health: " + health);
    }
}
