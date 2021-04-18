using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IAttackable
{
    public delegate void HealthChangedDelegate(int health);
    public event HealthChangedDelegate OnHealthChanged;

    [SerializeField] protected int health;
    [SerializeField] protected GameObject projectile;

    protected virtual void FixedUpdate()
    {
        if (health < 1)
            Die();
    }

    public void Attack(int damage)
    {
        health -= damage;
        OnHealthChanged?.Invoke(health);
    }

    protected void Die()
    {
        Destroy(gameObject);
    }
}