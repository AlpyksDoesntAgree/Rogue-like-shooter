using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : IWeapon
{
    private int _ammo = 0;
    private int _maxAmmo = 0;
    private float _damage = 75f;
    private float _fireRate = 0.25f;
    private float _attackRange = 2f;
    private float _spread = 0f;
    public float Damage => _damage;
    public float FireRate => _fireRate;

    public int Ammo
    {
        get => _ammo;
        set => _ammo = 0;
    }
    public int MaxAmmo => _maxAmmo;
    public float AttackRange => _attackRange;
    public float Spread => _spread;

    public event Action onAttack;
    public event Action onAmmoChange = null;
    public event Action onReload = null;
    public event Action onLowAmmoAmount = null;

    public void Attack() => onAttack?.Invoke();
    public void Reload()
    {
        return;
    }
}
