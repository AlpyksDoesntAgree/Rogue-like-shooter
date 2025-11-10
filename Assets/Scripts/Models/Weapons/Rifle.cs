using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : IWeapon
{
    private int _ammo = 20;
    private int _maxAmmo = 20;
    private float _damage = 25f;
    private float _fireRate = 0.05f;
    private float _attackRange = Mathf.Infinity;
    private float _spread = 7.5f;
    public float Damage => _damage;
    public float FireRate => _fireRate;

    public int Ammo
    {
        get => _ammo;
        set
        {
            _ammo = Mathf.Clamp(value, 0, _maxAmmo);
            onAmmoChange?.Invoke();

            if (_ammo <= 3)
            {
                onLowAmmoAmount?.Invoke();
            }
        }
    }
    public int MaxAmmo => _maxAmmo;
    public float AttackRange => _attackRange;
    public float Spread => _spread;

    public event Action onAmmoChange;
    public event Action onAttack;
    public event Action onReload;
    public event Action onLowAmmoAmount;

    public void Attack()
    {
        if (_ammo > 0)
        {
            Ammo--;
            onAttack?.Invoke();
        }
    }
    public void Reload()
    {
        if (Ammo < _maxAmmo)
        {
            Ammo = _maxAmmo;
            onReload?.Invoke();
        }
    }
}
