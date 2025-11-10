using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public float Health { get; set; }
    public float Damage { get; }
    public float Speed { get; }
    public float AttackRange { get; }
    public float AttackSpeed { get; }

    public event Action onDeath;
    void DealDamage(float damage);
}
