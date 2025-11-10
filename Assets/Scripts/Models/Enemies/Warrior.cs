using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : IEnemy
{
    private float _health = 150f;
    private float _damage = 30f;
    private float _speed = 1f;
    private float _attackRange = 1.5f;
    private float _attackSpeed = 1.25f;
    public float Health 
    { 
        get => _health;
        set
        {
            _health -= value;
            if( _health <= 0 )
                onDeath.Invoke();
        }
    }
    public float Damage => _damage;
    public float Speed => _speed;
    public float AttackRange => _attackRange;
    public float AttackSpeed => _attackSpeed;

    public event Action onDeath;

    public void DealDamage(float damage)
    {
        Health--;
    }
}
